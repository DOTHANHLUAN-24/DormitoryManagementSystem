using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý chỉ số tiêu thụ điện/nước (UtilityUsageService).
    /// </summary>
    public class UtilityUsageService(
        IUtilityUsageRepository utilityUsageRepository,
        IUtilityRepository utilityRepository,
        IUnitOfWork unitOfWork
    ) : IUtilityUsageService
    {
        private readonly IUtilityUsageRepository _utilityUsageRepository = utilityUsageRepository;
        private readonly IUtilityRepository _utilityRepository = utilityRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        /// <summary>
        /// Lấy danh sách ghi nhận chỉ số tiêu thụ phân trang kèm theo bộ lọc nâng cao.
        /// </summary>
        public async Task<PagedResult<UtilityUsage>> GetPagedUtilityUsagesAsync(
            int pageIndex, 
            int pageSize, 
            string? searchString, 
            Guid? blockId = null, 
            Guid? roomId = null, 
            int? month = null, 
            int? year = null, 
            Guid? utilityId = null, 
            bool? isActive = null, 
            bool? isDeleted = false)
        {
            var query = _utilityUsageRepository.GetQuery()
                .Include(u => u.Room)
                    .ThenInclude(r => r.Block)
                .Include(u => u.Utility)
                .Include(u => u.Invoice)
                .AsQueryable();

            if (isDeleted.HasValue)
            {
                query = query.Where(u => u.IsDeleted == isDeleted.Value);
            }
            if (isActive.HasValue)
            {
                query = query.Where(u => u.IsActive == isActive.Value);
            }
            if (blockId.HasValue)
            {
                query = query.Where(u => u.Room.BlockId == blockId.Value);
            }
            if (roomId.HasValue)
            {
                query = query.Where(u => u.RoomId == roomId.Value);
            }
            if (month.HasValue)
            {
                query = query.Where(u => u.Month == month.Value);
            }
            if (year.HasValue)
            {
                query = query.Where(u => u.Year == year.Value);
            }
            if (utilityId.HasValue)
            {
                query = query.Where(u => u.UtilityId == utilityId.Value);
            }
            
            if (!string.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().Trim();
                query = query.Where(u => u.Room.RoomNumber.ToLower().Contains(search) || 
                                         u.Utility.UtilityName.ToLower().Contains(search) ||
                                         u.Room.Block.BlockName.ToLower().Contains(search));
            }

            query = query.OrderByDescending(u => u.Year)
                         .ThenByDescending(u => u.Month)
                         .ThenBy(u => u.Room.RoomNumber);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedResult<UtilityUsage>(items, totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// Lấy danh sách ghi nhận chỉ số tiêu thụ của một phòng cụ thể (phân trang).
        /// </summary>
        public async Task<PagedResult<UtilityUsage>> GetPagedUtilityUsagesByRoomIdAsync(
            Guid roomId, 
            int pageIndex, 
            int pageSize, 
            string? searchString, 
            bool? isActive = null)
        {
            var query = _utilityUsageRepository.GetQuery()
                .Include(u => u.Room)
                    .ThenInclude(r => r.Block)
                .Include(u => u.Utility)
                .Include(u => u.Invoice)
                .Where(u => u.RoomId == roomId && !u.IsDeleted)
                .AsQueryable();

            if (isActive.HasValue)
            {
                query = query.Where(u => u.IsActive == isActive.Value);
            }
            
            if (!string.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().Trim();
                query = query.Where(u => u.Utility.UtilityName.ToLower().Contains(search));
            }

            query = query.OrderByDescending(u => u.Year)
                         .ThenByDescending(u => u.Month);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedResult<UtilityUsage>(items, totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// Lấy chi tiết bản ghi tiêu thụ theo Id.
        /// </summary>
        public async Task<UtilityUsage?> GetByIdAsync(Guid id)
        {
            return await _utilityUsageRepository.GetQuery()
                .Include(u => u.Room)
                    .ThenInclude(r => r.Block)
                .Include(u => u.Utility)
                .Include(u => u.Invoice)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Lấy chỉ số cuối kỳ gần nhất của phòng và loại dịch vụ để tự động làm chỉ số đầu kỳ tiếp theo.
        /// </summary>
        public async Task<double> GetLatestIndexAsync(Guid roomId, Guid utilityId)
        {
            var latestRecord = await _utilityUsageRepository.GetQuery()
                .Where(u => u.RoomId == roomId && u.UtilityId == utilityId && !u.IsDeleted)
                .OrderByDescending(u => u.Year)
                .ThenByDescending(u => u.Month)
                .ThenByDescending(u => u.CreatedDate)
                .FirstOrDefaultAsync();

            return latestRecord?.CurrentIndex ?? 0.0;
        }

        /// <summary>
        /// Tạo mới một bản ghi tiêu thụ điện nước. Tự động tính lượng tiêu thụ và tổng tiền.
        /// </summary>
        public async Task<bool> CreateUtilityUsageAsync(
            Guid roomId, 
            Guid utilityId, 
            int month, 
            int year, 
            double previousIndex, 
            double currentIndex, 
            bool isActive = true)
        {
            var utility = await _utilityRepository.GetByIdAsync(utilityId);
            if (utility == null)
            {
                throw new ArgumentException("Dịch vụ tiện ích không tồn tại trong hệ thống.");
            }

            if (currentIndex < previousIndex)
            {
                throw new ArgumentException("Chỉ số mới (cuối kỳ) không được phép nhỏ hơn chỉ số cũ (đầu kỳ).");
            }

            double usageQuantity = currentIndex - previousIndex;
            decimal totalAmount = (decimal)usageQuantity * utility.UnitPrice;

            // Kiểm tra trùng lặp bản ghi cùng phòng, dịch vụ, tháng, năm để tránh nhập lặp
            var duplicate = await _utilityUsageRepository.GetQuery()
                .AnyAsync(u => u.RoomId == roomId && u.UtilityId == utilityId && u.Month == month && u.Year == year && !u.IsDeleted);
            if (duplicate)
            {
                throw new ArgumentException($"Đã tồn tại chỉ số dịch vụ {utility.UtilityName} được ghi nhận cho phòng này vào tháng {month}/{year}.");
            }

            var usage = new UtilityUsage
            {
                RoomId = roomId,
                UtilityId = utilityId,
                Month = month,
                Year = year,
                PreviousIndex = previousIndex,
                CurrentIndex = currentIndex,
                UsageQuantity = usageQuantity,
                TotalAmount = totalAmount,
                IsActive = isActive,
                IsDeleted = false
            };

            await _utilityUsageRepository.AddAsync(usage);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Cập nhật chỉ số tiêu thụ điện nước. Tự động tính toán lại lượng tiêu thụ và tổng tiền.
        /// </summary>
        public async Task<bool> UpdateUtilityUsageAsync(
            Guid id, 
            double previousIndex, 
            double currentIndex, 
            bool isActive)
        {
            var usage = await _utilityUsageRepository.GetByIdAsync(id);
            if (usage == null) return false;

            var utility = await _utilityRepository.GetByIdAsync(usage.UtilityId);
            if (utility == null)
            {
                throw new ArgumentException("Dịch vụ tiện ích liên kết không tồn tại.");
            }

            if (currentIndex < previousIndex)
            {
                throw new ArgumentException("Chỉ số mới (cuối kỳ) không được phép nhỏ hơn chỉ số cũ (đầu kỳ).");
            }

            double usageQuantity = currentIndex - previousIndex;
            decimal totalAmount = (decimal)usageQuantity * utility.UnitPrice;

            usage.PreviousIndex = previousIndex;
            usage.CurrentIndex = currentIndex;
            usage.UsageQuantity = usageQuantity;
            usage.TotalAmount = totalAmount;
            usage.IsActive = isActive;
            usage.LastModified = DateTime.Now;

            await _utilityUsageRepository.UpdateAsync(usage);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa mềm một bản ghi chỉ số tiêu thụ.
        /// </summary>
        public async Task<bool> SoftDeleteUtilityUsageAsync(Guid id)
        {
            var usage = await _utilityUsageRepository.GetByIdAsync(id);
            if (usage == null) return false;

            await _utilityUsageRepository.DeleteAsync(usage, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Khôi phục một bản ghi chỉ số tiêu thụ đã bị xóa mềm.
        /// </summary>
        public async Task<bool> RestoreUtilityUsageAsync(Guid id)
        {
            var usage = await _utilityUsageRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (usage == null || !usage.IsDeleted) return false;

            await _utilityUsageRepository.RestoreAsync(usage);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
