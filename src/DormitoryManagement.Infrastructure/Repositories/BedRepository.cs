using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace DormitoryManagement.Infrastructure.Repositories
{
    public class BedRepository : IBedRepository
    {

       private readonly ApplicationDbContext _context;
      
        public BedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bed?> GetByBedNumberAsync(string bedNumber)
        {
            if(string.IsNullOrEmpty(bedNumber))
            {
                return null;
            }
            return await _context.Beds
                .FirstOrDefaultAsync(b => !b.IsDeleted && b.BedNumber == bedNumber);
        }

        public IEnumerable<Bed> GetPagingQuery(string searchString)
        {
            var query = _context.Beds
                .Include(b => b.Room)
                .Where(b => !b.IsDeleted)
                .AsQueryable();


            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.BedNumber.Contains(searchString));
            }
            else
            {
                query = query.Where(b => false);
            }

            return query
                .OrderByDescending(b => b.CreatedDate)
                .ToList();
        }
        public async Task<IEnumerable<Bed>> GetAllAsync()
        {
            return await _context.Beds
            .Include(b => b.Room)
            .Where(b => !b.IsDeleted)
            .OrderByDescending(b => b.CreatedDate)
            .ToListAsync();
        }   

        public async Task<Bed?> GetByIdAsync(Guid id)
        {
            return await _context.Beds
            .Include(b => b.Room)
            .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Bed entity)
        {
            if (entity == null) return;

            entity.CreatedDate = DateTime.Now;
            entity.LastModified = null;
            entity.IsDeleted = false;

            await _context.Beds.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task AddRangeAsync(IEnumerable<Bed> entities)
        {
            var now = DateTime.Now;

            if (entities == null) return;

            foreach (var e in entities)
            {
                if (e == null) continue;
                e.CreatedDate = now;
                e.LastModified = null;
                e.IsDeleted = false;
            }

            await _context.Beds.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public void Update(Bed entity)
        {
            if (entity == null) return;

            var bed = _context.Beds.FirstOrDefault(b => b.Id == entity.Id && !b.IsDeleted);
            if (bed == null) return;

            bed.BedNumber = entity.BedNumber;
            bed.Status = entity.Status;
            bed.RoomId = entity.RoomId;
            bed.IsActive = entity.IsActive;
            bed.LastModified = DateTime.Now;

            _context.SaveChanges();
        }

        public void Delete(Bed entity)
        {
            if (entity == null) return;

            var bed = _context.Beds.FirstOrDefault(b => b.Id == entity.Id && !b.IsDeleted);
            if (bed == null) return;

            bed.IsDeleted = true;
            bed.LastModified = DateTime.Now;

            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<Bed> entities)
        {
            if (entities == null) return;

            var ids = entities.Select(e => e.Id).ToList();
            if (!ids.Any()) return;

            var beds = _context.Beds.Where(b => ids.Contains(b.Id) && !b.IsDeleted).ToList();
            if (!beds.Any()) return;

            var now = DateTime.Now;
            foreach (var bed in beds)
            {
                bed.IsDeleted = true;
                bed.LastModified = now;
            }

            _context.SaveChanges();
        }
    }
}
