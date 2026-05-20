using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý hợp đồng thuê phòng (ContractService).
    /// </summary>
    public class ContractService : IContractService
    {
        private readonly IContractRepository _db;

        /// <summary>
        /// Khởi tạo ContractService.
        /// </summary>
        /// <param name="db">Repository hợp đồng</param>
        public ContractService(IContractRepository db)
        {
            _db = db;
        }

        /// <summary>
        /// Lấy hợp đồng theo mã hợp đồng (ContractCode).
        /// </summary>
        public Task<Contract?> GetByContractCodeAsync(string contractCode)
        {
            return _db.GetByContractCodeAsync(contractCode);
        }

        /// <summary>
        /// Lấy toàn bộ danh sách hợp đồng liên kết với người dùng theo UserId.
        /// </summary>
        public Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId)
        {
            return _db.GetByUserIdAsync(userId);
        }

        /// <summary>
        /// Lấy hợp đồng đang hoạt động liên kết với giường theo BedId.
        /// </summary>
        public Task<Contract> GetByBedIdAsync(Guid bedId)
        {
            return _db.GetByBedIdAsync(bedId);
        }
    }
}
