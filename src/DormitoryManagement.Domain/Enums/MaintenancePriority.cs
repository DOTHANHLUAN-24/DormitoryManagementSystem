namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Mức độ ưu tiên của yêu cầu bảo trì/sửa chữa.
    /// </summary>
    public enum MaintenancePriority 
    { 
        /// <summary>Ưu tiên thấp.</summary>
        Low, 
        
        /// <summary>Ưu tiên trung bình.</summary>
        Medium, 
        
        /// <summary>Ưu tiên cao.</summary>
        High, 
        
        /// <summary>Khẩn cấp, cần xử lý ngay.</summary>
        Urgent 
    }
}
