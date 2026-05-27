namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Trạng thái của giường trong phòng.
    /// </summary>
    public enum BedStatus 
    { 
        /// <summary>Giường trống, có thể sử dụng.</summary>
        Available, 
        
        /// <summary>Giường đã có người ở.</summary>
        Occupied, 
        
        /// <summary>Giường đang được bảo trì/sửa chữa.</summary>
        Maintenance 
    }
}
