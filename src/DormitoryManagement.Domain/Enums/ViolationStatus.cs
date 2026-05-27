namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Trạng thái của vi phạm nội quy.
    /// </summary>
    public enum ViolationStatus 
    { 
        /// <summary>Vi phạm đang chờ xử lý.</summary>
        Pending, 
        
        /// <summary>Vi phạm đã được giải quyết/xử phạt xong.</summary>
        Resolved, 
        
        /// <summary>Vi phạm đang bị khiếu nại.</summary>
        Appealed 
    }
}
