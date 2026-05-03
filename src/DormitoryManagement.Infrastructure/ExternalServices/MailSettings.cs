namespace DormitoryManagement.Infrastructure.ExternalServices
{
    public class MailSettings
    {
        public string FromEmail { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Host { get; set; } = string.Empty;

        public int Port { get; set; }

        public bool EnableSsl { get; set; } = true;
    }
}
