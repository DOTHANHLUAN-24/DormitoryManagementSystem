namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string userName, string role);
    }
}
