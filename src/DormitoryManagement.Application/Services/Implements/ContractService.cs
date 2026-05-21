using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý hợp đồng thuê phòng (ContractService).
    /// </summary>
    public class ContractService(IContractRepository contractRepository) : IContractService
    {
        private readonly IContractRepository _contractRepository = contractRepository;

        /// <summary>
        /// Lấy hợp đồng theo mã hợp đồng (ContractCode).
        /// </summary>
        public Task<Contract?> GetByContractCodeAsync(string contractCode)
        {
            return _contractRepository.GetByContractCodeAsync(contractCode);
        }

        /// <summary>
        /// Lấy toàn bộ danh sách hợp đồng liên kết với người dùng theo UserId.
        /// </summary>
        public Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId)
        {
            return _contractRepository.GetByUserIdAsync(userId);
        }

        /// <summary>
        /// Lấy hợp đồng đang hoạt động liên kết với giường theo BedId.
        /// </summary>
        public Task<Contract> GetByBedIdAsync(Guid bedId)
        {
            return _contractRepository.GetByBedIdAsync(bedId);
        }
    }
}
