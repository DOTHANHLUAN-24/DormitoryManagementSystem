using System;

namespace DormitoryManagement.Models
{
    /// <summary>
    /// Model hiển thị thông tin khách đến thăm trong các View và API.
    /// </summary>
    public class VisitorViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string VisitorName { get; set; } = string.Empty;
        public string IdentityCard { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public Guid HostId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool IsCheckedOut { get; set; }
        public string Status { get; set; } = "Chờ duyệt"; // "Chờ duyệt", "Đang ở trong", "Đã rời đi", "Từ chối", "Quá giờ"
        public DateTime CreatedDate { get; set; }
    }
}
