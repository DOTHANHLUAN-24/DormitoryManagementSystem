using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Services.Implements
{
    public class UtilityServiceRequestService(
        IUtilityServiceRequestRepository utilityServiceRequestRepository,
        IContractRepository contractRepository,
        IUtilityRepository utilityRepository,
        IUtilityUsageRepository utilityUsageRepository,
        IUnitOfWork unitOfWork
    ) : IUtilityServiceRequestService
    {
        private readonly IUtilityServiceRequestRepository _utilityServiceRequestRepository = utilityServiceRequestRepository;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IUtilityRepository _utilityRepository = utilityRepository;
        private readonly IUtilityUsageRepository _utilityUsageRepository = utilityUsageRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> RegisterServiceRequestAsync(Guid userId, Guid utilityId, int quantity, string? notes)
        {
            var contract = await _contractRepository.GetQuery()
                .Include(c => c.Bed)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == DormitoryManagement.Domain.Enums.ContractStatus.Active);
            
            if (contract == null || contract.Bed?.RoomId == null)
            {
                throw new Exception("Bạn không có hợp đồng thuê phòng hoạt động để đăng ký dịch vụ.");
            }

            var utility = await _utilityRepository.GetByIdAsync(utilityId);
            if (utility == null || !utility.IsActive)
            {
                throw new Exception("Dịch vụ không tồn tại hoặc đã ngừng hoạt động.");
            }

            var existing = await _utilityServiceRequestRepository.GetQuery()
                .AnyAsync(r => r.RoomId == contract.Bed.RoomId && r.UtilityId == utilityId && r.Status == "Pending");

            if (existing)
            {
                throw new Exception("Yêu cầu đăng ký dịch vụ này cho phòng của bạn đang được chờ xử lý.");
            }

            var request = new UtilityServiceRequest
            {
                RoomId = contract.Bed.RoomId,
                RequesterId = userId,
                UtilityId = utilityId,
                Status = "Pending",
                Quantity = quantity,
                Notes = notes,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };

            await _utilityServiceRequestRepository.AddAsync(request);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<UtilityServiceRequest>> GetPagedServiceRequestsAsync(int pageIndex, int pageSize, string? searchString, string? status = null)
        {
            var query = _utilityServiceRequestRepository.GetQuery()
                .Include(r => r.Room)
                    .ThenInclude(room => room.Block)
                .Include(r => r.Requester)
                .Include(r => r.Utility)
                .Where(r => !r.IsDeleted);

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(r => r.Status == status);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(r => r.Room.RoomNumber.Contains(searchString) || 
                                         r.Requester.FullName.Contains(searchString) || 
                                         r.Utility.UtilityName.Contains(searchString));
            }

            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(r => r.CreatedDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<UtilityServiceRequest>(items, totalCount, pageIndex, pageSize);
        }

        public async Task<bool> ApproveServiceRequestAsync(Guid requestId, Guid processedById)
        {
            var request = await _utilityServiceRequestRepository.GetQuery()
                .Include(r => r.Utility)
                .FirstOrDefaultAsync(r => r.Id == requestId && !r.IsDeleted);

            if (request == null)
            {
                return false;
            }

            request.Status = "Approved";
            request.LastModified = DateTime.Now;
            
            await _utilityServiceRequestRepository.UpdateAsync(request);

            // Tạo bản ghi UtilityUsage để theo dõi lượng sử dụng dịch vụ và phục vụ tạo hóa đơn
            var usage = new UtilityUsage
            {
                RoomId = request.RoomId,
                UtilityId = request.UtilityId,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                PreviousIndex = 0,
                CurrentIndex = request.Quantity,
                UsageQuantity = request.Quantity,
                TotalAmount = request.Utility.UnitPrice * request.Quantity,
                InvoiceId = null,
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            await _utilityUsageRepository.AddAsync(usage);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RejectServiceRequestAsync(Guid requestId, Guid processedById)
        {
            var request = await _utilityServiceRequestRepository.GetQuery()
                .FirstOrDefaultAsync(r => r.Id == requestId && !r.IsDeleted);

            if (request == null)
            {
                return false;
            }

            request.Status = "Rejected";
            request.LastModified = DateTime.Now;

            await _utilityServiceRequestRepository.UpdateAsync(request);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<UtilityServiceRequest>> GetServiceRequestsByUserIdAsync(Guid userId)
        {
            return await _utilityServiceRequestRepository.GetQuery()
                .Include(r => r.Utility)
                .Where(r => r.RequesterId == userId && !r.IsDeleted)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }
    }
}
