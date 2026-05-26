using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;

namespace DormitoryManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp triển khai Repository quản lý thông tin khách đến thăm (VisitorLog).
    /// </summary>
    public class VisitorLogRepository(ApplicationDbContext db) : BaseRepository<VisitorLog>(db), IVisitorLogRepository
    {
    }
}
