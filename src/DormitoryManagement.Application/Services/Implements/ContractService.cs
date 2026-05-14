using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services.Implements
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _db;

        public ContractService(IContractRepository db)
        {
            _db = db;
        }

        public Task<Contract?> GetByContractCodeAsync(string contractCode)
        {
            return _db.GetByContractCodeAsync(contractCode);
        }

        public Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId)
        {
            return _db.GetByUserIdAsync(userId);
        }

        public Task<Contract> GetByBedIdAsync(Guid bedId)
        {
            return _db.GetByBedIdAsync(bedId);
        }
    }
}
