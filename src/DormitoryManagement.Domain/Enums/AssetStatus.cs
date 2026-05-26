namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Trạng thái của tài sản trong ký túc xá.
    /// </summary>
    public enum AssetStatus 
    { 
        /// <summary>Tài sản đang trong tình trạng tốt.</summary>
        Good, 
        
        /// <summary>Tài sản bị hỏng.</summary>
        Broken, 
        
        /// <summary>Tài sản đang được sửa chữa.</summary>
        UnderRepair, 
        
        /// <summary>Tài sản đã bị mất.</summary>
        Lost 
    }
}
