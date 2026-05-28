using System;

namespace DormitoryManagement.Application.Dtos.Requests
{
    public class ViolationResponseDto
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;
        public string Status { get; set; } = "Chưa xử lý";
        public decimal FineAmount { get; set; }
    }
}
