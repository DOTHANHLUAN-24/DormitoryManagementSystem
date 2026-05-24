using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Vehicles;
using DormitoryManagement.Application.Dtos.Responses.Vehicles;
using DormitoryManagement.Application.Mappings;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Application.Mappings;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Triển khai dịch vụ quản lý phương tiện (Vehicle).
    /// </summary>
    public class VehicleService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork, IMapper mapper) : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Lấy danh sách phương tiện phân trang kèm theo lọc tìm kiếm/owner/trạng thái hoạt động và trạng thái xóa.
        /// </summary>
        public async Task<PagedResult<VehicleResponseDto>> GetPagedVehiclesAsync(
            int pageIndex,
            int pageSize,
            string? searchTerm,
            bool? isActive = null,
            bool? isDeleted = false,
            Guid? ownerId = null)
        {
            // Note: BaseRepository.GetByStatusPagedAsync mặc định order by CreatedDate desc.
            // Để search theo LicensePlate/VehicleType, ta đưa predicate vào.
            var predicate = ownerId.HasValue
                ? (System.Linq.Expressions.Expression<Func<Vehicle, bool>>)(v =>
                    (string.IsNullOrEmpty(searchTerm) || v.LicensePlate.Contains(searchTerm) || v.VehicleType.Contains(searchTerm)) &&
                    v.OwnerId == ownerId.Value)
                : (System.Linq.Expressions.Expression<Func<Vehicle, bool>>)(v =>
                    string.IsNullOrEmpty(searchTerm) ||
                    v.LicensePlate.Contains(searchTerm) ||
                    v.VehicleType.Contains(searchTerm));

            var result = await _vehicleRepository.GetByStatusPagedAsync(
                pageIndex,
                pageSize,
                isActive: isActive,
                isDeleted: isDeleted,
                predicate: predicate);

            return result.MapToPagedResult<Vehicle, VehicleResponseDto>(_mapper);
        }

        public async Task<IEnumerable<VehicleResponseDto>> GetActiveVehiclesByOwnerIdAsync(Guid ownerId)
        {
            var vehicles = await _vehicleRepository.GetActiveVehiclesByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<VehicleResponseDto>>(vehicles);
        }

        public async Task<VehicleResponseDto?> GetVehicleByIdAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            return _mapper.Map<VehicleResponseDto>(vehicle);
        }

        public async Task<VehicleResponseDto?> GetVehicleByLicensePlateAsync(string licensePlate)
        {
            var vehicle = await _vehicleRepository.GetByLicensePlateAsync(licensePlate);
            return _mapper.Map<VehicleResponseDto>(vehicle);
        }

        public async Task<bool> CreateVehicleAsync(VehicleRequestDto request)
        {
            if (await _vehicleRepository.IsLicensePlateDuplicateAsync(request.LicensePlate))
            {
                throw new Exception("Biển số phương tiện này đã tồn tại trong hệ thống.");
            }

            var vehicle = _mapper.Map<Vehicle>(request);

            vehicle.Id = Guid.NewGuid();
            vehicle.CreatedDate = DateTime.Now;
            vehicle.LastModified = null;
            vehicle.IsActive = true;
            vehicle.IsDeleted = false;

            await _vehicleRepository.AddAsync(vehicle);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateVehicleAsync(Guid id, VehicleUpdateDto request)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return false;
            }

            // prevent license plate duplicate
            if (await _vehicleRepository.IsLicensePlateDuplicateAsync(request.LicensePlate, id))
            {
                throw new Exception("Biển số phương tiện đã bị trùng với một phương tiện khác.");
            }

            _mapper.Map(request, vehicle);
            vehicle.Id = vehicle.Id; // keep entity id as-is
            vehicle.LastModified = DateTime.Now;

            // ensure flags
            vehicle.IsActive = request.IsActive;
            // IsDeleted giữ nguyên trạng thái (soft-delete)
            vehicle.IsDeleted = vehicle.IsDeleted;

            await _vehicleRepository.UpdateAsync(vehicle);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> ToggleVehicleStatusAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return false;
            }

            vehicle.IsActive = !vehicle.IsActive;
            vehicle.LastModified = DateTime.Now;

            await _vehicleRepository.UpdateAsync(vehicle);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteVehicleAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return false;
            }

            await _vehicleRepository.DeleteAsync(vehicle, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RestoreVehicleAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null || !vehicle.IsDeleted)
            {
                return false;
            }

            await _vehicleRepository.RestoreAsync(vehicle);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePermanentlyAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null || !vehicle.IsDeleted)
            {
                return false;
            }

            await _vehicleRepository.DeleteAsync(vehicle, isSoftDelete: false);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsLicensePlateDuplicateAsync(string licensePlate, Guid? excludeId = null)
        {
            return await _vehicleRepository.IsLicensePlateDuplicateAsync(licensePlate, excludeId);
        }
    }
}
