using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Interfaces
{
    public interface IBedService
    {
        // Các thao tác truy vấn (Read)
        Task<Bed?> GetByIdAsync(Guid id);
        Task<IEnumerable<Bed>> GetAllAsync(bool includeDeleted = false);
        Task<Bed?> GetByBedNumberAsync(string bedNumber);
        Task<PagedResult<Bed>> GetPagedAsync(int pageIndex, int pageSize, string? searchString = null);
        Task<IEnumerable<Bed>> GetAvailableBedsByRoomIdAsync(Guid roomId);
        Task<bool> IsBedAvailableAsync(Guid bedId);

        // Các thao tác thay đổi dữ liệu (Create, Update, Delete) sử dụng UnitOfWork
        Task<bool> CreateBedAsync(Bed bed);
        Task<bool> UpdateBedAsync(Bed bed);

        /// <summary>
        /// Xóa một giường ra khỏi hệ thống
        /// </summary>
        /// <param name="id">Mã định danh của giường</param>
        /// <param name="isSoftDelete">Mặc định true (xóa mềm), false (xóa cứng)</param>
        /// <returns></returns>
        Task<bool> DeleteBedAsync(Guid id, bool isSoftDelete = true);
    }
}