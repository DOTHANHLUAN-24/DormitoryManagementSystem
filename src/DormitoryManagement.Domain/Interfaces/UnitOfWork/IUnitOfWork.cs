namespace DormitoryManagement.Domain.Interfaces.UnitOfWork
{
    /// <summary>
    /// Giao diện Unit of Work, quản lý các thay đổi trong một transaction (giao dịch).
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Lưu tất cả các thay đổi của các repository vào database một cách đồng bộ.
        /// </summary>
        /// <returns>Số lượng bản ghi bị ảnh hưởng.</returns>
        Task<int> SaveChangesAsync();
    }
}
