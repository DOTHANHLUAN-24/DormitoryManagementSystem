using DormitoryManagement.Application.Common.Configurations;
using DormitoryManagement.Application.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DormitoryManagement.Infrastructure.ExternalServices
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        // Đọc cấu hình trực tiếp từ IConfiguration (Cách này đảm bảo 100% đọc được JSON mà không cần cấu hình Program.cs)
        public EmailService(IConfiguration config)
        {
            _mailSettings = new MailSettings
            {
                Mail = config["MailSettings:Mail"] ?? string.Empty,
                DisplayName = config["MailSettings:DisplayName"] ?? string.Empty,
                Password = config["MailSettings:Password"] ?? string.Empty,
                Host = config["MailSettings:Host"] ?? string.Empty,
                Port = int.TryParse(config["MailSettings:Port"], out int port) ? port : 587
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            // Kiểm tra nếu Mail hoặc Host vẫn trống thì báo lỗi đích danh
            if (string.IsNullOrEmpty(_mailSettings.Mail) || string.IsNullOrEmpty(_mailSettings.Host))
            {
                throw new Exception($"LỖI ĐỌC CẤU HÌNH: Dữ liệu bị rỗng. Vui lòng check lại file appsettings.json. [Mail: '{_mailSettings.Mail}', Host: '{_mailSettings.Host}']");
            }

            var email = new MimeMessage();
            email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = htmlMessage };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
