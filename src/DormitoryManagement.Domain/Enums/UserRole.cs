namespace DormitoryManagement.Domain.Enums
{
    /// <summary>
    /// Vai trò của người dùng trong hệ thống.
    /// </summary>
    public enum UserRole 
    { 
        /// <summary>Quản trị viên hệ thống.</summary>
        Admin, 
        
        /// <summary>Nhân viên quản lý ký túc xá.</summary>
        ManagementStaff, 
        
        /// <summary>Sinh viên.</summary>
        Student, 
        
        /// <summary>Nhân viên kỹ thuật/bảo trì.</summary>
        TechnicalStaff 
    }
}
