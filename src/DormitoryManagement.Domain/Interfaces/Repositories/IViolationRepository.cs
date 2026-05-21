using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin vi phạm kỷ luật (Violation).
    /// </summary>
    public interface IViolationRepository : IBaseRepository<Violation>
    {
        /// <summary>
        /// Lấy thông tin bản ghi vi phạm dựa trên đường dẫn ảnh minh chứng.
        /// </summary>
        /// <param name="evidenceImage">Đường dẫn ảnh minh chứng của vi phạm</param>
        /// <returns>Bản ghi vi phạm tương ứng nếu tìm thấy, ngược lại là null</returns>
        Task<Violation?> GetByEvidenceImageAsync(string evidenceImage);

        /// <summary>
        /// Lấy danh sách tất cả các vi phạm thuộc về một hợp đồng thuê cụ thể.
        /// </summary>
        /// <param name="contractId">Id của hợp đồng thuê phòng cần lấy lịch sử vi phạm</param>
        /// <returns>Danh sách các vi phạm liên quan</returns>
        Task<IEnumerable<Violation>> GetViolationsByContractIdAsync(Guid contractId);

        /// <summary>
        /// Kiểm tra xem một vi phạm đã được giải quyết / xử lý xong hay chưa.
        /// </summary>
        /// <param name="violationId">Id của vi phạm</param>
        /// <returns>True nếu vi phạm đã được xử lý (Resolved), ngược lại là False</returns>
        Task<bool> IsViolationResolvedAsync(Guid violationId);

        /// <summary>
        /// Lọc danh sách vi phạm theo trạng thái xử lý (Đang xử lý, Đã xử lý, Đã hủy).
        /// </summary>
        /// <param name="status">Trạng thái xử lý vi phạm</param>
        /// <returns>Danh sách các vi phạm thỏa mãn trạng thái</returns>
        Task<IEnumerable<Violation>> GetViolationsByStatusAsync(ViolationStatus status);
    }
}
