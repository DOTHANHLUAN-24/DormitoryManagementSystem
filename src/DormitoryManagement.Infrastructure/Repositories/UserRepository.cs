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

        public IQueryable<User> GetPagingQuery(string searchString)
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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _db.Users.FindAsync(id.ToString());
            
            return user ?? new User();
        }

        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<User>.Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
