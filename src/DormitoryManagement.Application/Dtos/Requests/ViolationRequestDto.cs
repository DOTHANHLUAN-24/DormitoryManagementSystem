using System;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManagement.Application.Dtos.Requests
{
    public class ViolationRequestDto
    {
        [Required(ErrorMessage = "Mã số sinh viên không được để trống")]
        public string StudentId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên phòng ở")]
        public string Room { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn mức độ vi phạm")]
        public string Severity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn ngày lập biên bản")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Nội dung vi phạm không được để trống")]
        public string Content { get; set; } = string.Empty;

        public string Status { get; set; } = "Chưa xử lý";
    }
}