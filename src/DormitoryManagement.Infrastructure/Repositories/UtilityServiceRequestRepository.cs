using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý yêu cầu đăng ký dịch vụ của sinh viên (UtilityServiceRequest).
    /// </summary>
    public class UtilityServiceRequestRepository(ApplicationDbContext db) : BaseRepository<UtilityServiceRequest>(db), IUtilityServiceRequestRepository
    {
    }
}
