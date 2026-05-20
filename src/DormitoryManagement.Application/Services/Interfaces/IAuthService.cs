using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Dtos.Responses;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ xử lý xác thực (Authentication).
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Thực hiện đăng nhập tài khoản và sinh mã JWT token.
        /// </summary>
        /// <param name="loginRequest">Thông tin yêu cầu đăng nhập (tên tài khoản và mật khẩu)</param>
        /// <returns>Thông tin phản hồi đăng nhập chứa mã token và thông tin người dùng nếu thành công, ngược lại là null</returns>
        Task<LoginResponse?> LoginAsync(LoginRequest loginRequest);
    }
}
