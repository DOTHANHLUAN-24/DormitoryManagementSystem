using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IUtilityRepository : IBaseRepository<Utility>
    {
        Task<Utility?> GetByUtilityNameAsync(string utilityName);

        Task<IEnumerable<Utility>> GetActiveUtilitiesAsync();

        Task<bool> IsUtilityActiveAsync(Guid utilityId);
    }
}
