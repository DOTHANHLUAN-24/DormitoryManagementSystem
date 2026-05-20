namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ tạo mã JWT Token phục vụ xác thực người dùng.
    /// </summary>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Tạo mã JWT Token dựa trên thông tin người dùng.
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <param name="userName">Tên tài khoản người dùng</param>
        /// <param name="role">Vai trò (Role) của người dùng trong hệ thống</param>
        /// <returns>Chuỗi JWT Token mã hóa</returns>
        string GenerateToken(Guid userId, string userName, string role);
    }
}
