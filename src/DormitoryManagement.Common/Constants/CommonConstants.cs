namespace Common.Constants
{
    public static class CommonConstants
    {
        public static readonly string AdminRole = "Admin";

        public static readonly string ManagerRole = "ManagementStaff";
        
        public static readonly string StudentRole = "Student";
        
        public static readonly string TechnicalRole = "TechnicalStaff";
    }

    public static class Roles
    {
        public const string Admin = "Admin";
        public const string ManagementStaff = "ManagementStaff";
        public const string TechnicalStaff = "TechnicalStaff";
        public const string Student = "Student";
    }

    public static class DefaultPasswords
    {
        public const string Admin = "Admin@123";
        public const string ManagementStaff = "Manager@123";
        public const string TechnicalStaff = "Tech@123";
        public const string Student = "Student@123";
    }

    public static class ConfigKeys
    {
        public const string DefaultConnection = "DefaultConnection";
        public const string JwtKey = "JwtSettings:Key";
        public const string JwtIssuer = "JwtSettings:Issuer";
        public const string JwtAudience = "JwtSettings:Audience";
        public const string PaginationPageSize = "Pagination:PageSize";
    }

    public static class CookieNames
    {
        public const string JwtToken = "JWTToken";
    }

    public static class AntiForgery
    {
        public const string HeaderName = "RequestVerificationToken";
    }

    public static class Routes
    {
        public const string AccountLogin = "/Account/Login";
    }

    public static class Pagination
    {
        public const int DefaultPageSize = 5;
    }
}
