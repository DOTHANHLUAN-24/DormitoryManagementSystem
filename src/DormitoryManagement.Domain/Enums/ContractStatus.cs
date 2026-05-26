namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Trạng thái của hợp đồng thuê phòng.
    /// </summary>
    public enum ContractStatus 
    { 
        /// <summary>Hợp đồng đang có hiệu lực.</summary>
        Active, 
        
        /// <summary>Hợp đồng đã hết hạn.</summary>
        Expired, 
        
        /// <summary>Hợp đồng đã bị chấm dứt trước hạn.</summary>
        Terminated, 
        
        /// <summary>Hợp đồng đang chờ xử lý/duyệt.</summary>
        Pending 
    }
}
