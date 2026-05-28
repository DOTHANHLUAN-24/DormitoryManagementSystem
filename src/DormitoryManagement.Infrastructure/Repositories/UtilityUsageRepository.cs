using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin ghi nhận tiêu thụ dịch vụ (UtilityUsage).
    /// </summary>
    public class UtilityUsageRepository(ApplicationDbContext db) : BaseRepository<UtilityUsage>(db), IUtilityUsageRepository
    {
    }
}
