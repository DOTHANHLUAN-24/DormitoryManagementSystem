using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Utilities;
using DormitoryManagement.Application.Dtos.Responses.Utilities;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Application.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Triển khai dịch vụ quản lý dịch vụ / tiện ích (UtilityService).
    /// </summary>
    public class UtilityService(
        IUtilityRepository utilityRepository,
        IUtilityServiceRequestRepository utilityServiceRequestRepository,
        IContractRepository contractRepository,
        IUtilityUsageRepository utilityUsageRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IUtilityService
    {
        private readonly IUtilityRepository _utilityRepository = utilityRepository;
        private readonly IUtilityServiceRequestRepository _utilityServiceRequestRepository = utilityServiceRequestRepository;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IUtilityUsageRepository _utilityUsageRepository = utilityUsageRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Lấy toàn bộ danh sách dịch vụ đang hoạt động.
        /// </summary>
        public async Task<IEnumerable<UtilityResponseDto>> GetAllActiveUtilitiesAsync()
        {
            var utilities = await _utilityRepository.GetActiveUtilitiesAsync();
            return _mapper.Map<IEnumerable<UtilityResponseDto>>(utilities);
        }

        /// <summary>
        /// Lấy danh sách dịch vụ bị xóa mềm (nằm trong thùng rác - IsActive = false).
        /// </summary>
        public async Task<IEnumerable<UtilityResponseDto>> GetAllDeletedUtilitiesAsync()
        {
            var all = await _utilityRepository.GetAllAsync();
            var trashed = all.Where(u => !u.IsActive && !u.IsDeleted).OrderByDescending(u => u.CreatedDate);
            return _mapper.Map<IEnumerable<UtilityResponseDto>>(trashed);
        }

        /// <summary>
        /// Lấy danh sách dịch vụ tiện ích phân trang kèm theo bộ lọc tìm kiếm.
        /// </summary>
        public async Task<PagedResult<UtilityResponseDto>> GetPagedUtilitiesAsync(int pageIndex, int pageSize, string? searchTerm, bool? isActive = null, bool? isDeleted = false)
        {
            var result = await _utilityRepository.GetByStatusPagedAsync(
                pageIndex,
                pageSize,
                isActive: isActive,
                isDeleted: isDeleted,
                predicate: u => string.IsNullOrEmpty(searchTerm) || u.UtilityName.Contains(searchTerm)
            );

            return result.MapToPagedResult<Utility, UtilityResponseDto>(_mapper);
        }

        /// <summary>
        /// Lấy chi tiết thông tin dịch vụ theo Id.
        /// </summary>
        public async Task<UtilityResponseDto?> GetUtilityByIdAsync(Guid id)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            return _mapper.Map<UtilityResponseDto>(utility);
        }

        /// <summary>
        /// Tạo mới một dịch vụ tiện ích.
        /// </summary>
        public async Task<bool> CreateUtilityAsync(UtilityRequestDto request)
        {
            var existing = await _utilityRepository.GetByUtilityNameAsync(request.UtilityName);
            if (existing != null)
            {
                throw new Exception("Tên dịch vụ tiện ích này đã tồn tại trong hệ thống.");
            }

            var utility = _mapper.Map<Utility>(request);
            utility.IsActive = true;
            utility.IsDeleted = false;

            await _utilityRepository.AddAsync(utility);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Cập nhật thông tin dịch vụ.
        /// </summary>
        public async Task<bool> UpdateUtilityAsync(Guid id, UtilityRequestDto request)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return false;
            }

            var existingWithName = await _utilityRepository.GetByUtilityNameAsync(request.UtilityName);
            if (existingWithName != null && existingWithName.Id != id)
            {
                throw new Exception("Tên dịch vụ mới đã tồn tại trên một dịch vụ khác.");
            }

            _mapper.Map(request, utility);
            utility.LastModified = DateTime.Now;

            await _utilityRepository.UpdateAsync(utility);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Đưa dịch vụ vào thùng rác (IsActive = false).
        /// </summary>
        public async Task<bool> SoftDeleteUtilityAsync(Guid id)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return false;
            }

            utility.IsActive = false;
            utility.LastModified = DateTime.Now;

            await _utilityRepository.UpdateAsync(utility);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Khôi phục dịch vụ từ thùng rác (IsActive = true).
        /// </summary>
        public async Task<bool> RestoreUtilityAsync(Guid id)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return false;
            }

            utility.IsActive = true;
            utility.LastModified = DateTime.Now;

            await _utilityRepository.UpdateAsync(utility);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa vĩnh viễn dịch vụ tiện ích ra khỏi database (xóa cứng).
        /// </summary>
        public async Task<bool> HardDeleteUtilityAsync(Guid id)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return false;
            }

            await _utilityRepository.DeleteAsync(utility, isSoftDelete: false);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RegisterServiceRequestAsync(Guid userId, Guid utilityId, int quantity, string? notes)
        {
            var contract = await _contractRepository.GetQuery()
                .Include(c => c.Bed)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == DormitoryManagement.Domain.Enums.ContractStatus.Active);
            
            if (contract == null || contract.Bed?.RoomId == null)
            {
                throw new Exception("Bạn không có phòng hoạt động để đăng ký dịch vụ.");
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
                InvoiceId = null, // Chưa gắn hóa đơn, sẽ được load lên khi tạo hóa đơn tháng
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

        public async Task<IEnumerable<UtilityUsage>> GetUtilityUsagesByRoomIdAsync(Guid roomId)
        {
            return await _utilityUsageRepository.GetQuery()
                .Include(u => u.Utility)
                .Where(u => u.RoomId == roomId && !u.IsDeleted)
                .OrderByDescending(u => u.Year)
                .ThenByDescending(u => u.Month)
                .ToListAsync();
        }

        public async Task<IEnumerable<UtilityServiceRequest>> GetServiceRequestsByUserIdAsync(Guid userId)
        {
            return await _utilityServiceRequestRepository.GetQuery()
                .Include(r => r.Utility)
                .Where(r => r.RequesterId == userId && !r.IsDeleted)
                .ToListAsync();
        }
    }
}
