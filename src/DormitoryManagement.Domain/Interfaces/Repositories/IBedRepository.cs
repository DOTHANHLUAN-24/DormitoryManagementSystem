using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin giường (Bed).
    /// </summary>
    public interface IBedRepository : IBaseRepository<Bed>
    {
        /// <summary>
        /// Lấy thông tin giường theo số giường (BedNumber).
        /// </summary>
        /// <param name="bedNumber">Số giường cần tìm</param>
        /// <returns>Giường nếu tìm thấy, ngược lại là null</returns>
        Task<Bed?> GetByBedNumberAsync(string bedNumber);

        /// <summary>
        /// Lấy danh sách các giường còn trống trong một phòng cụ thể.
        /// </summary>
        /// <param name="roomId">Id của phòng cần kiểm tra</param>
        /// <returns>Danh sách giường trống</returns>
        Task<IEnumerable<Bed>> GetAvailableBedsByRoomIdAsync(Guid roomId);

        /// <summary>
        /// Kiểm tra xem một giường có còn trống (không có sinh viên đăng ký) hay không.
        /// </summary>
        /// <param name="bedId">Id của giường cần kiểm tra</param>
        /// <returns>True nếu giường còn trống, ngược lại là False</returns>
        Task<bool> IsBedAvailableAsync(Guid bedId);
    }
}