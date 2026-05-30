using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý ghi nhận chỉ số tiêu thụ điện/nước (UtilityUsage).
    /// </summary>
    public interface IUtilityUsageService
    {
        /// <summary>
        /// Lấy danh sách ghi nhận chỉ số tiêu thụ phân trang kèm theo bộ lọc nâng cao.
        /// </summary>
        Task<PagedResult<UtilityUsage>> GetPagedUtilityUsagesAsync(
            int pageIndex, 
            int pageSize, 
            string? searchString, 
            Guid? blockId = null, 
            Guid? roomId = null, 
            int? month = null, 
            int? year = null, 
            Guid? utilityId = null, 
            bool? isActive = null, 
            bool? isDeleted = false);

        /// <summary>
        /// Lấy danh sách ghi nhận chỉ số tiêu thụ của một phòng cụ thể (phân trang).
        /// </summary>
        Task<PagedResult<UtilityUsage>> GetPagedUtilityUsagesByRoomIdAsync(
            Guid roomId, 
            int pageIndex, 
            int pageSize, 
            string? searchString, 
            bool? isActive = null);

        /// <summary>
        /// Lấy chi tiết bản ghi tiêu thụ theo Id.
        /// </summary>
        Task<UtilityUsage?> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy chỉ số cuối kỳ gần nhất của phòng và loại dịch vụ để tự động làm chỉ số đầu kỳ tiếp theo.
        /// </summary>
        Task<double> GetLatestIndexAsync(Guid roomId, Guid utilityId);

        /// <summary>
        /// Tạo mới một bản ghi tiêu thụ điện nước. Tự động tính lượng tiêu thụ và tổng tiền.
        /// </summary>
        Task<bool> CreateUtilityUsageAsync(
            Guid roomId, 
            Guid utilityId, 
            int month, 
            int year, 
            double previousIndex, 
            double currentIndex, 
            bool isActive = true);

        /// <summary>
        /// Cập nhật chỉ số tiêu thụ điện nước. Tự động tính toán lại lượng tiêu thụ và tổng tiền.
        /// </summary>
        Task<bool> UpdateUtilityUsageAsync(
            Guid id, 
            double previousIndex, 
            double currentIndex, 
            bool isActive);

        /// <summary>
        /// Xóa mềm một bản ghi chỉ số tiêu thụ.
        /// </summary>
        Task<bool> SoftDeleteUtilityUsageAsync(Guid id);

        /// <summary>
        /// Khôi phục một bản ghi chỉ số tiêu thụ đã bị xóa mềm.
        /// </summary>
        Task<bool> RestoreUtilityUsageAsync(Guid id);
    }
}
