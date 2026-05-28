using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Infrastructure.Data;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext db) : IUnitOfWork
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
