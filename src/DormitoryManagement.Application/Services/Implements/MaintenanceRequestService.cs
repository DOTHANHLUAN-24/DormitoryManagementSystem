using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.MaintenanceRequests;
using DormitoryManagement.Application.Dtos.Responses.MaintenanceRequests;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Application.Mappings;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý yêu cầu bảo trì (MaintenanceRequestService).
    /// </summary>
    public class MaintenanceRequestService(IMaintenanceRequestRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IMaintenanceRequestService
    {
        private readonly IMaintenanceRequestRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<MaintenanceRequestResponseDto> CreateAsync(CreateMaintenanceRequestDto dto, Guid requesterId)
        {
            var entity = _mapper.Map<MaintenanceRequest>(dto);
            entity.RequesterId = requesterId;
            entity.CreatedDate = DateTime.Now;
            entity.IsActive = true;
            entity.IsDeleted = false;

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // Lấy lại từ DB để map đầy đủ thông tin (Room, Requester...)
            var createdEntity = await _repository.GetByIdAsync(entity.Id);
            return _mapper.Map<MaintenanceRequestResponseDto>(createdEntity ?? entity);
        }

        public async Task<MaintenanceRequestResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<MaintenanceRequestResponseDto>(entity);
        }

        public async Task<PagedResult<MaintenanceRequestResponseDto>> GetAllPagedAsync(int pageIndex, int pageSize, string? searchTerm = null, MaintenanceStatus? status = null, MaintenancePriority? priority = null)
        {
            System.Linq.Expressions.Expression<Func<MaintenanceRequest, bool>> predicate = x =>
                !x.IsDeleted
                && (string.IsNullOrEmpty(searchTerm) || x.Title.Contains(searchTerm) || x.Description.Contains(searchTerm))
                && (!status.HasValue || x.Status == status.Value)
                && (!priority.HasValue || x.Priority == priority.Value);

            var pagedData = await _repository.GetByStatusPagedAsync(
                pageIndex, pageSize, true, false, predicate,
                x => x.Room, x => x.Room.Block, x => x.Requester, x => x.Handler!);

            return pagedData.MapToPagedResult<MaintenanceRequest, MaintenanceRequestResponseDto>(_mapper);
        }

        public async Task<IEnumerable<MaintenanceRequestResponseDto>> GetByRequesterIdAsync(Guid requesterId)
        {
            var entities = await _repository.GetRequestsByRequesterIdAsync(requesterId);
            return _mapper.Map<IEnumerable<MaintenanceRequestResponseDto>>(entities);
        }

        public async Task<bool> UpdateStatusAsync(Guid id, UpdateMaintenanceStatusDto dto, Guid? handlerId = null)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Status = dto.Status;

            if (handlerId.HasValue)
            {
                entity.HandlerId = handlerId.Value;
            }

            if (dto.Status == MaintenanceStatus.Resolved || dto.Status == MaintenanceStatus.Closed)
            {
                entity.ResolvedAt = DateTime.Now;
            }

            entity.LastModified = DateTime.Now;

            await _repository.UpdateAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity, true); // Soft delete
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
