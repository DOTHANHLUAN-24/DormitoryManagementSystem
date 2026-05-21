namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ gửi email tự động (Email Service).
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Gửi một email không đồng bộ (Asynchronous Email Send).
        /// </summary>
        /// <param name="toEmail">Địa chỉ email người nhận</param>
        /// <param name="subject">Tiêu đề email</param>
        /// <param name="htmlMessage">Nội dung email định dạng HTML</param>
        /// <returns>Task đại diện cho hoạt động gửi email không đồng bộ</returns>
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}
