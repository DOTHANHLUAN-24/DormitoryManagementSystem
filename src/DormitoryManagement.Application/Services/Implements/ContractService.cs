using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý hợp đồng thuê phòng (ContractService).
    /// </summary>
    public class ContractService(
        IContractRepository contractRepository,
        IBedRepository bedRepository,
        IUnitOfWork unitOfWork
    ) : IContractService
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IBedRepository _bedRepository = bedRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

        /// <summary>
        /// Lấy hợp đồng theo Id (eager loading các liên kết cần thiết).
        /// </summary>
        public async Task<Contract?> GetByIdAsync(Guid id)
        {
            return await _contractRepository.GetQuery()
                .Include(c => c.User)
                .Include(c => c.Bed)
                .ThenInclude(b => b.Room)
                .ThenInclude(r => r.Block)
                .Include(c => c.Bed)
                .ThenInclude(b => b.Room)
                .ThenInclude(r => r.RoomType)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Lấy danh sách hợp đồng phân trang, hỗ trợ tìm kiếm và lọc trạng thái.
        /// </summary>
        public async Task<PagedResult<Contract>> GetPagedContractsAsync(int pageIndex, int pageSize, string? searchString = null, ContractStatus? status = null)
        {
            var query = _contractRepository.GetPagingQuery(searchString ?? "")
                .Include(c => c.User)
                .Include(c => c.Bed)
                .ThenInclude(b => b.Room)
                .ThenInclude(r => r.Block)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status.Value);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Contract>(items, totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// Tạo mới một hợp đồng.
        /// </summary>
        public async Task<bool> CreateContractAsync(Contract contract)
        {
            await _contractRepository.AddAsync(contract);

            if (contract.Status == ContractStatus.Active)
            {
                var bed = await _bedRepository.GetByIdAsync(contract.BedId);
                if (bed != null)
                {
                    bed.Status = BedStatus.Occupied;
                    await _bedRepository.UpdateAsync(bed);
                }
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Cập nhật thông tin hợp đồng, tự động cập nhật trạng thái giường tương ứng.
        /// </summary>
        public async Task<bool> UpdateContractAsync(Contract contract)
        {
            await _contractRepository.UpdateAsync(contract);

            // Tự động cập nhật trạng thái giường tương ứng dựa vào trạng thái hợp đồng
            var bed = await _bedRepository.GetByIdAsync(contract.BedId);
            if (bed != null)
            {
                if (contract.Status == ContractStatus.Active)
                {
                    bed.Status = BedStatus.Occupied;
                    await _bedRepository.UpdateAsync(bed);
                }
                else if (contract.Status == ContractStatus.Expired || contract.Status == ContractStatus.Terminated)
                {
                    // Trả giường về trạng thái trống nếu kết thúc hợp đồng
                    bed.Status = BedStatus.Available;
                    await _bedRepository.UpdateAsync(bed);
                }
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa hợp đồng theo Id.
        /// </summary>
        public async Task<bool> DeleteContractAsync(Guid id)
        {
            var contract = await _contractRepository.GetByIdAsync(id);
            if (contract == null) return false;

            // Xóa hợp đồng và trả giường về trống nếu hợp đồng đang hoạt động
            if (contract.Status == ContractStatus.Active || contract.Status == ContractStatus.Pending)
            {
                var bed = await _bedRepository.GetByIdAsync(contract.BedId);
                if (bed != null)
                {
                    bed.Status = BedStatus.Available;
                    await _bedRepository.UpdateAsync(bed);
                }
            }

            await _contractRepository.DeleteAsync(contract, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Lấy số lượng hợp đồng ở trạng thái chờ duyệt (Pending).
        /// </summary>
        public async Task<int> GetPendingCountAsync()
        {
            return await _contractRepository.GetQuery()
                .CountAsync(c => c.Status == ContractStatus.Pending);
        }
    }
}
