using System.Threading.Tasks;
using DormitoryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PagedList;
namespace DormitoryManagement.Data.Dao
{
    public class UserDao
    {
        ApplicationDbContext db = null!;

        public UserDao()
        {
            db = new ApplicationDbContext();
        }

        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> listUsers = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                listUsers = listUsers.Where(x => x.UserName!.Contains(searchString) || x.PhoneNumber!.Contains(searchString));
            }

            return listUsers.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.UserName == username) ?? new User();
        }

        public async Task<User> ViewDetail(string userId)
        {
            return await db.Users.FindAsync(userId) ?? new User();
        }

        public async Task<bool> Insert(User entity)
        {
            db.Users.Add(entity);
            
            entity.CreatedDate = DateTime.Now;


            var result = await db.SaveChangesAsync();
            if(result > 0 )
            {
                return true;
            }

            return false;
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.Id.ToString());

                if(user == null)
                {
                    return false;
                }

                user.PhoneNumber = entity.PhoneNumber;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
