using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IViolationRepository : IBaseRepository<Violation>
    {
        /// <summary>
        /// Lấy ảnh bằng chứng
        /// </summary>
        /// <param name="evidenceImage">link ảnh</param>
        /// <returns>một cái</returns>
        Task<Violation?> GetByEvidenceImageAsync(string evidenceImage);

        Task<IEnumerable<Violation>> GetViolationsByContractIdAsync(Guid contractId);

        Task<bool> IsViolationResolvedAsync(Guid violationId);

        Task<IEnumerable<Violation>> GetViolationsByStatusAsync(ViolationStatus status);
    }
}
