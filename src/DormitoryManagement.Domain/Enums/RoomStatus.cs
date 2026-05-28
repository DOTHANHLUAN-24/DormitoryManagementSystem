namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Trạng thái của phòng trong ký túc xá.
    /// </summary>
    public enum RoomStatus 
    { 
        /// <summary>Phòng còn giường trống, có thể ở.</summary>
        Available, 
        
        /// <summary>Phòng đã đầy, không nhận thêm người.</summary>
        Full, 
        
        /// <summary>Phòng đang được bảo trì/sửa chữa.</summary>
        Maintenance, 
        
        /// <summary>Phòng đã được đặt trước.</summary>
        Reserved 
    }
}
