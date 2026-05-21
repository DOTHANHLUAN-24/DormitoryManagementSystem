using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Utilities;
using DormitoryManagement.Application.Dtos.Responses.Utilities;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Application.Mappings;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Triển khai dịch vụ quản lý dịch vụ / tiện ích (UtilityService).
    /// </summary>
    public class UtilityService(IUtilityRepository utilityRepository, IUnitOfWork unitOfWork, IMapper mapper) : IUtilityService
    {
        private readonly IUtilityRepository _utilityRepository = utilityRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Lấy toàn bộ danh sách dịch vụ đang hoạt động.
        /// </summary>
        public async Task<IEnumerable<UtilityResponseDto>> GetAllActiveUtilitiesAsync()
        {
            var utilities = await _utilityRepository.GetActiveUtilitiesAsync();
            return _mapper.Map<IEnumerable<UtilityResponseDto>>(utilities);
        }

        /// <summary>
        /// Lấy danh sách dịch vụ bị xóa mềm (nằm trong thùng rác - IsActive = false).
        /// </summary>
        public async Task<IEnumerable<UtilityResponseDto>> GetAllDeletedUtilitiesAsync()
        {
            var all = await _utilityRepository.GetAllAsync();
            var trashed = all.Where(u => !u.IsActive && !u.IsDeleted).OrderByDescending(u => u.CreatedDate);
            return _mapper.Map<IEnumerable<UtilityResponseDto>>(trashed);
        }

        /// <summary>
        /// Lấy danh sách dịch vụ tiện ích phân trang kèm theo bộ lọc tìm kiếm.
        /// </summary>
        public async Task<PagedResult<UtilityResponseDto>> GetPagedUtilitiesAsync(int pageIndex, int pageSize, string? searchTerm, bool? isActive = null, bool? isDeleted = false)
        {
            var result = await _utilityRepository.GetByStatusPagedAsync(
                pageIndex,
                pageSize,
                isActive: isActive,
                isDeleted: isDeleted,
                predicate: u => string.IsNullOrEmpty(searchTerm) || u.UtilityName.Contains(searchTerm)
            );

            return result.MapToPagedResult<Utility, UtilityResponseDto>(_mapper);
        }

        /// <summary>
        /// Lấy chi tiết thông tin dịch vụ theo Id.
        /// </summary>
        public async Task<UtilityResponseDto?> GetUtilityByIdAsync(Guid id)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            return _mapper.Map<UtilityResponseDto>(utility);
        }

        /// <summary>
        /// Tạo mới một dịch vụ tiện ích.
        /// </summary>
        public async Task<bool> CreateUtilityAsync(UtilityRequestDto request)
        {
            var existing = await _utilityRepository.GetByUtilityNameAsync(request.UtilityName);
            if (existing != null)
            {
                throw new Exception("Tên dịch vụ tiện ích này đã tồn tại trong hệ thống.");
            }

            var utility = _mapper.Map<Utility>(request);
            utility.IsActive = true;
            utility.IsDeleted = false;

            await _utilityRepository.AddAsync(utility);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Cập nhật thông tin dịch vụ.
        /// </summary>
        public async Task<bool> UpdateUtilityAsync(Guid id, UtilityRequestDto request)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return false;
            }

            var existingWithName = await _utilityRepository.GetByUtilityNameAsync(request.UtilityName);
            if (existingWithName != null && existingWithName.Id != id)
            {
                throw new Exception("Tên dịch vụ mới đã tồn tại trên một dịch vụ khác.");
            }

            _mapper.Map(request, utility);
            utility.LastModified = DateTime.Now;

            await _utilityRepository.UpdateAsync(utility);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Đưa dịch vụ vào thùng rác (IsActive = false).
        /// </summary>
        public async Task<bool> SoftDeleteUtilityAsync(Guid id)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return false;
            }

            utility.IsActive = false;
            utility.LastModified = DateTime.Now;

            await _utilityRepository.UpdateAsync(utility);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Khôi phục dịch vụ từ thùng rác (IsActive = true).
        /// </summary>
        public async Task<bool> RestoreUtilityAsync(Guid id)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return false;
            }

            utility.IsActive = true;
            utility.LastModified = DateTime.Now;

            await _utilityRepository.UpdateAsync(utility);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa vĩnh viễn dịch vụ tiện ích ra khỏi database (xóa cứng).
        /// </summary>
        public async Task<bool> HardDeleteUtilityAsync(Guid id)
        {
            var utility = await _utilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return false;
            }

            await _utilityRepository.DeleteAsync(utility, isSoftDelete: false);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
