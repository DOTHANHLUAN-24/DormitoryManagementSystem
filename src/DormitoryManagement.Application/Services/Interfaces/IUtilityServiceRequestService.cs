using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý yêu cầu đăng ký tiện ích / dịch vụ (UtilityServiceRequest).
    /// </summary>
    public interface IUtilityServiceRequestService
    {
        /// <summary>
        /// Đăng ký dịch vụ cho sinh viên với số lượng cụ thể.
        /// </summary>
        Task<bool> RegisterServiceRequestAsync(Guid userId, Guid utilityId, int quantity, string? notes);

        /// <summary>
        /// Lấy danh sách yêu cầu đăng ký dịch vụ phân trang.
        /// </summary>
        Task<PagedResult<UtilityServiceRequest>> GetPagedServiceRequestsAsync(int pageIndex, int pageSize, string? searchString, string? status = null);

        /// <summary>
        /// Phê duyệt yêu cầu đăng ký dịch vụ.
        /// </summary>
        Task<bool> ApproveServiceRequestAsync(Guid requestId, Guid processedById);

        /// <summary>
        /// Từ chối yêu cầu đăng ký dịch vụ.
        /// </summary>
        Task<bool> RejectServiceRequestAsync(Guid requestId, Guid processedById);

        /// <summary>
        /// Lấy danh sách tất cả yêu cầu đăng ký dịch vụ của sinh viên.
        /// </summary>
        Task<IEnumerable<UtilityServiceRequest>> GetServiceRequestsByUserIdAsync(Guid userId);
    }
}
