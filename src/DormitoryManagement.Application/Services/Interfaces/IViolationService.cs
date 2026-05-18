using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// CHÚ Ý DÒNG NÀY: Thay bằng đường dẫn thư mục chứa file ViolationRequestDto của bạn
using DormitoryManagement.Application.Dtos.Requests; 

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IViolationService
    {
        // Hàm lấy danh sách hiển thị ở trang Index
        Task<IEnumerable<object>> GetActiveViolationsPagedAsync(int page, int pageSize, string search);

        // Hàm lấy chi tiết phục vụ trang Edit
        Task<object> GetViolationByIdAsync(Guid id);

        // Hàm xử lý khi bấm "Tạo mới"
        Task<bool> CreateViolationAsync(ViolationRequestDto violationDto);

        // Hàm xử lý khi bấm "Cập nhật"
        Task<bool> UpdateViolationAsync(Guid id, ViolationRequestDto violationDto);
    }
}
