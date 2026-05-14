using DormitoryManagement.Application.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

namespace DormitoryManagement.Application.Services
{
    public class BedService : IBedService
    {
        private readonly IBedRepository _bedRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BedService(IBedRepository bedRepository, IUnitOfWork unitOfWork)
        {
            _bedRepository = bedRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Bed?> GetByIdAsync(Guid id)
        {
            return await _bedRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Bed>> GetAllAsync(bool includeDeleted = false)
        {
            return await _bedRepository.GetAllAsync(includeDeleted);
        }

        public async Task<Bed?> GetByBedNumberAsync(string bedNumber)
        {
            return await _bedRepository.GetByBedNumberAsync(bedNumber);
        }

        public async Task<PagedResult<Bed>> GetPagedAsync(int pageIndex, int pageSize, string? searchString = null)
        {
            // Tận dụng hàm GetPagedAsync có sẵn của IBaseRepository
            return await _bedRepository.GetPagedAsync(
                pageIndex,
                pageSize,
                predicate: string.IsNullOrWhiteSpace(searchString)
                    ? null
                    : b => b.BedNumber.Contains(searchString),
                orderBy: q => q.OrderByDescending(b => b.CreatedDate)
            );
        }

        public async Task<IEnumerable<Bed>> GetAvailableBedsByRoomIdAsync(Guid roomId)
        {
            return await _bedRepository.GetAvailableBedsByRoomIdAsync(roomId);
        }

        public async Task<bool> IsBedAvailableAsync(Guid bedId)
        {
            return await _bedRepository.IsBedAvailableAsync(bedId);
        }

        public async Task<bool> CreateBedAsync(Bed bed)
        {
            await _bedRepository.AddAsync(bed);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateBedAsync(Bed bed)
        {
            await _bedRepository.UpdateAsync(bed);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteBedAsync(Guid id, bool isSoftDelete = true)
        {
            var bed = await _bedRepository.GetByIdAsync(id);
            if (bed == null) return false;

            await _bedRepository.DeleteAsync(bed, isSoftDelete);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}