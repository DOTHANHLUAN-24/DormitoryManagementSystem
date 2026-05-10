using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class BedRepository : IBedRepository
    {
        private readonly ApplicationDbContext _db;

        public BedRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Bed entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            await _db.Beds.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Bed> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var e in entities)
            {
                e.CreatedDate = now;
            }

            await _db.Beds.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
        }

        public void Delete(Bed entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<Bed> entities)
        {
            throw new NotImplementedException();
        }

        public Task<(List<Bed> Beds, int TotalCount)> GetActiveBedsPagedAsync(int page, int pageSize, string? search)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bed>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Bed?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Bed entity)
        {
            throw new NotImplementedException();
        }
    }
}
