using DormitoryManagement.Application.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

namespace DormitoryManagement.Application.Services
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý giường (BedService).
    /// </summary>
    public class BedService(IBedRepository bedRepository, IUnitOfWork unitOfWork) : IBedService
    {
        private readonly IBedRepository _bedRepository = bedRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        /// <summary>
        /// Lấy thông tin giường theo Id.
        /// </summary>
        public async Task<Bed?> GetByIdAsync(Guid id)
        {
            return await _bedRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Lấy toàn bộ danh sách giường.
        /// </summary>
        public async Task<IEnumerable<Bed>> GetAllAsync(bool includeDeleted = false)
        {
            return await _bedRepository.GetAllAsync(includeDeleted);
        }

        /// <summary>
        /// Lấy thông tin giường theo số giường (BedNumber).
        /// </summary>
        public async Task<Bed?> GetByBedNumberAsync(string bedNumber)
        {
            return await _bedRepository.GetByBedNumberAsync(bedNumber);
        }

        /// <summary>
        /// Lấy danh sách giường phân trang kèm tìm kiếm.
        /// </summary>
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

        /// <summary>
        /// Lấy danh sách các giường trống thuộc một phòng cụ thể.
        /// </summary>
        public async Task<IEnumerable<Bed>> GetAvailableBedsByRoomIdAsync(Guid roomId)
        {
            return await _bedRepository.GetAvailableBedsByRoomIdAsync(roomId);
        }

        /// <summary>
        /// Kiểm tra xem giường có trống để sử dụng không.
        /// </summary>
        public async Task<bool> IsBedAvailableAsync(Guid bedId)
        {
            return await _bedRepository.IsBedAvailableAsync(bedId);
        }

        /// <summary>
        /// Tạo mới một giường vào hệ thống.
        /// </summary>
        public async Task<bool> CreateBedAsync(Bed bed)
        {
            await _bedRepository.AddAsync(bed);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Cập nhật thông tin giường.
        /// </summary>
        public async Task<bool> UpdateBedAsync(Bed bed)
        {
            await _bedRepository.UpdateAsync(bed);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// Xóa một giường ra khỏi hệ thống (hỗ trợ xóa mềm/xóa cứng).
        /// </summary>
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