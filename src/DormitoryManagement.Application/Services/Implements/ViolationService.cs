using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Services.Interfaces;

namespace DormitoryManagement.Application.Services.Implements
{
    public class ViolationService : IViolationService
    {
        // Sử dụng Dictionary để lưu Mock Data theo Id của DTO cho dễ tìm kiếm
        private static readonly Dictionary<Guid, ViolationRequestDto> MockData = new Dictionary<Guid, ViolationRequestDto>();

        static ViolationService()
        {
            // Khởi tạo 2 bản ghi dữ liệu mẫu chuẩn Dto
            var id1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var id2 = Guid.Parse("22222222-2222-2222-2222-222222222222");

            MockData[id1] = new ViolationRequestDto 
            { 
                StudentId = "SV202601", 
                Room = "P.302-A1", 
                Severity = "Cảnh cáo", 
                Date = DateTime.Now.AddDays(-2), 
                Content = "Sử dụng thiết bị công suất lớn (Bếp từ) trong phòng.", 
                Status = "Chưa xử lý" 
            };

            MockData[id2] = new ViolationRequestDto 
            { 
                StudentId = "SV202605", 
                Room = "P.105-B2", 
                Severity = "Nhẹ", 
                Date = DateTime.Now.AddDays(-5), 
                Content = "Về muộn sau 23h00 không có lý do chính đáng.", 
                Status = "Đã xử lý" 
            };
        }

        public async Task<IEnumerable<object>> GetActiveViolationsPagedAsync(int page, int pageSize, string search)
        {
            // Chuyển danh sách kèm theo Id ra để hiển thị ngoài bảng Index
            var list = new List<object>();
            foreach (var item in MockData)
            {
                list.Add(new { 
                    Id = item.Key, 
                    StudentId = item.Value.StudentId, 
                    Room = item.Value.Room, 
                    Severity = item.Value.Severity, 
                    Date = item.Value.Date, 
                    Content = item.Value.Content, 
                    Status = item.Value.Status 
                });
            }
            return await Task.FromResult(list);
        }

        public async Task<object> GetViolationByIdAsync(Guid id)
        {
            // Nếu tìm thấy Id trong danh sách Mock, trả về đúng đối tượng Dto đó
            if (MockData.ContainsKey(id))
            {
                return await Task.FromResult(MockData[id]);
            }
            
            // Trường hợp test từ link trực tiếp không có id, trả về bản ghi đầu tiên tránh lỗi trống
            foreach (var item in MockData.Values)
            {
                return await Task.FromResult(item);
            }
            
            return await Task.FromResult<object>(null!);
        }

        public async Task<bool> CreateViolationAsync(ViolationRequestDto violationDto)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateViolationAsync(Guid id, ViolationRequestDto violationDto)
        {
            if (MockData.ContainsKey(id))
            {
                MockData[id] = violationDto;
            }
            return await Task.FromResult(true);
        }
    }
}