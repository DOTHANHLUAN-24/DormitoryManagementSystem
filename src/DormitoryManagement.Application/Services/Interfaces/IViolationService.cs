using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DormitoryManagement.Application.Dtos.Requests; 

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý nghiệp vụ vi phạm kỷ luật (Violation).
    /// </summary>
    public interface IViolationService
    {
        /// <summary>
        /// Lấy danh sách vi phạm kỷ luật chưa bị xóa kèm phân trang và tìm kiếm (phục vụ hiển thị trang danh sách).
        /// </summary>
        /// <param name="page">Chỉ số trang hiện tại</param>
        /// <param name="pageSize">Số lượng phần tử trên trang</param>
        /// <param name="search">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách các đối tượng nặc danh hoặc DTO chứa thông tin vi phạm</returns>
        Task<IEnumerable<object>> GetActiveViolationsPagedAsync(int page, int pageSize, string search);

        /// <summary>
        /// Lấy thông tin chi tiết một bản ghi vi phạm kỷ luật theo Id.
        /// </summary>
        /// <param name="id">Id bản ghi vi phạm</param>
        /// <returns>Thông tin vi phạm dạng đối tượng DTO hoặc object</returns>
        Task<object> GetViolationByIdAsync(Guid id);

        /// <summary>
        /// Tạo mới một bản ghi vi phạm kỷ luật của sinh viên.
        /// </summary>
        /// <param name="violationDto">Thông tin yêu cầu tạo vi phạm</param>
        /// <returns>True nếu tạo thành công, ngược lại là False</returns>
        Task<bool> CreateViolationAsync(ViolationRequestDto violationDto);

        /// <summary>
        /// Cập nhật thông tin bản ghi vi phạm kỷ luật hiện có.
        /// </summary>
        /// <param name="id">Id bản ghi vi phạm cần sửa</param>
        /// <param name="violationDto">Thông tin cập nhật mới</param>
        /// <returns>True nếu cập nhật thành công, ngược lại là False</returns>
        Task<bool> UpdateViolationAsync(Guid id, ViolationRequestDto violationDto);
    }
}
