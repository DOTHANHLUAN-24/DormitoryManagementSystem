using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) // Inject qua Constructor
        {
            _db = db;
        }

        public IQueryable<User> GetQuery()
        {
            return _db.Users.AsQueryable();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<(List<User> Users, int TotalCount)> GetActiveUsersPagedAsync(
            int page,
            int pageSize,
            string? search)
        {
            var query = _db.Users
                .Where(x => !x.IsDeleted && x.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.UserName!.Contains(search) ||
                    x.PhoneNumber!.Contains(search) ||
                    x.Email!.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var users = await query
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        public IQueryable<User> GetPagingQuery(string searchString, int pageIndex, int pageSize)
        {
            var query = _db.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.UserName!.Contains(searchString) || x.PhoneNumber!.Contains(searchString));
            }
            return query.OrderByDescending(x => x.CreatedDate);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> Insert(User entity)
        {
            _db.Users.Add(entity);

            entity.CreatedDate = DateTime.Now;


            var result = await _db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public bool Update(User entity)
        {
            try
            {
                var user = _db.Users.Find(entity.Id.ToString());

                if (user == null)
                {
                    return false;
                }

                user.PhoneNumber = entity.PhoneNumber;

                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _db.Users.FindAsync(id.ToString());

            return user ?? new User();
        }

        public async Task AddAsync(User entity)
        {
            entity.CreatedDate = DateTime.Now;
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<User> entities)
        {
            var now = DateTime.Now;
            foreach (var e in entities)
            {
                e.CreatedDate = now;
            }

            await _db.Users.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
        }

        void IBaseRepository<User>.Update(User entity)
        {
            var user = _db.Users.Find(entity.Id);
            if (user == null)
            {
                return;
            }

            user.FullName = entity.FullName;
            user.PhoneNumber = entity.PhoneNumber;
            user.Email = entity.Email;
            user.IsActive = entity.IsActive;
            user.IdentityCardNumber = entity.IdentityCardNumber;
            user.Role = entity.Role;
            user.Code = entity.Code;
            user.LastModified = DateTime.Now;

            _db.SaveChanges();
        }

        public void Delete(User entity)
        {
            var user = _db.Users.Find(entity.Id);
            if (user == null)
            {
                return;
            }

            // Soft delete
            user.IsDeleted = true;
            user.LastModified = DateTime.Now;
            _db.SaveChanges();
        }

        public void DeleteRange(IEnumerable<User> entities)
        {
            var ids = entities.Select(e => e.Id).ToList();
            var users = _db.Users.Where(u => ids.Contains(u.Id)).ToList();
            if (!users.Any())
            {
                return;
            }

            var now = DateTime.Now;
            foreach (var u in users)
            {
                u.IsDeleted = true;
                u.LastModified = now;
            }

            _db.SaveChanges();
        }
    }
}
