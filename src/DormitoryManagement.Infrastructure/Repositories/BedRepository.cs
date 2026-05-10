using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;

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

        public async void DeleteAsync(Bed entity)
        {
            var bed = await _db.Beds.FindAsync(entity.Id);
            if (bed == null)
                return;

            bed.IsDeleted = true;
            bed.LastModified = DateTime.UtcNow;

            await _db.SaveChangesAsync();

        }

        public void DeleteRangeAsync(IEnumerable<Bed> entities)
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

        public Task<List<Bed>> GetAllBedByStatusAsync(BedStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<Bed?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Bed entity)
        {
            throw new NotImplementedException();
        }
    }
}
