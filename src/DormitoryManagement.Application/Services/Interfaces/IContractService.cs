using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    public interface IContractService
    {
        Task<Contract?> GetByContractCodeAsync(string contractCode);

        Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId);

        Task<Contract> GetByBedIdAsync(Guid bedId);
    }
}
