namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Trạng thái của yêu cầu bảo trì/sửa chữa.
    /// </summary>
    public enum MaintenanceStatus 
    { 
        /// <summary>Yêu cầu mới được tạo, chưa xử lý.</summary>
        Open, 
        
        /// <summary>Đang trong quá trình xử lý/sửa chữa.</summary>
        InProgress, 
        
        /// <summary>Đã sửa chữa/giải quyết xong.</summary>
        Resolved, 
        
        /// <summary>Yêu cầu đã được đóng lại.</summary>
        Closed 
    }
}
