using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Assets;
using DormitoryManagement.Application.Dtos.Responses.Assets;
using DormitoryManagement.Application.Mappings;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý tài sản (AssetService).
    /// </summary>
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Khởi tạo AssetService.
        /// </summary>
        /// <param name="assetRepository">Repository tài sản</param>
        /// <param name="unitOfWork">Bộ quản lý UnitOfWork</param>
        /// <param name="mapper">Bộ ánh xạ AutoMapper</param>
        public AssetService(IAssetRepository assetRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy danh sách tài sản phân trang, hỗ trợ tìm kiếm theo tên/mã tài sản, lọc theo trạng thái và phòng.
        /// </summary>
        public async Task<PagedResult<AssetResponse>> GetPagedAssetsAsync(int pageIndex, int pageSize, string? searchTerm = null, AssetStatus? status = null, Guid? roomId = null)
        {
            var result = await _assetRepository.GetAssetsWithDetailsPagedAsync(pageIndex, pageSize, searchTerm, status, roomId);
            return result.MapToPagedResult<Asset, AssetResponse>(_mapper);
        }

        /// <summary>
        /// Lấy danh sách tài sản đã bị xóa mềm (thùng rác) phân trang.
        /// </summary>
        public async Task<PagedResult<AssetResponse>> GetDeletedAssetsPagedAsync(int pageIndex, int pageSize, string? searchTerm = null)
        {
            var result = await _assetRepository.GetDeletedAssetsWithDetailsPagedAsync(pageIndex, pageSize, searchTerm);
            return result.MapToPagedResult<Asset, AssetResponse>(_mapper);
        }

        /// <summary>
        /// Lấy thông tin chi tiết của tài sản theo Id.
        /// </summary>
        public async Task<AssetResponse?> GetAssetByIdAsync(Guid id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            return _mapper.Map<AssetResponse>(asset);
        }

        /// <summary>
        /// Lấy thông tin tài sản theo mã tài sản (AssetCode).
        /// </summary>
        public async Task<AssetResponse?> GetAssetByCodeAsync(string assetCode)
        {
            var asset = await _assetRepository.GetByAssetCodeAsync(assetCode);
            return _mapper.Map<AssetResponse>(asset);
        }

        /// <summary>
        /// Lấy danh sách tài sản đang hoạt động của một phòng cụ thể.
        /// </summary>
        public async Task<IEnumerable<AssetResponse>> GetActiveAssetsByRoomIdAsync(Guid roomId)
        {
            var assets = await _assetRepository.GetActiveAssetsByRoomIdAsync(roomId);
            return _mapper.Map<IEnumerable<AssetResponse>>(assets);
        }

        /// <summary>
        /// Tạo mới một tài sản vào hệ thống.
        /// </summary>
        public async Task<bool> CreateAssetAsync(CreateAssetRequest request)
        {
            if (await _assetRepository.IsAssetCodeDuplicateAsync(request.AssetCode))
            {
                throw new Exception("Mã tài sản này đã tồn tại trong hệ thống.");
            }

            var asset = _mapper.Map<Asset>(request);
            asset.Id = Guid.NewGuid();
            asset.CreatedDate = DateTime.Now;
            asset.IsActive = true;
            asset.IsDeleted = false;

            await _assetRepository.AddAsync(asset);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Cập nhật thông tin tài sản hiện tại.
        /// </summary>
        public async Task<bool> UpdateAssetAsync(Guid id, UpdateAssetRequest request)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset == null) return false;

            if (await _assetRepository.IsAssetCodeDuplicateAsync(request.AssetCode, id))
            {
                throw new Exception("Mã tài sản đã bị trùng với tài sản khác.");
            }

            _mapper.Map(request, asset);
            asset.LastModified = DateTime.Now;

            await _assetRepository.UpdateAsync(asset);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa mềm tài sản (chuyển vào thùng rác).
        /// </summary>
        public async Task<bool> SoftDeleteAssetAsync(Guid id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset == null) return false;

            await _assetRepository.DeleteAsync(asset, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Khôi phục tài sản bị xóa mềm từ thùng rác.
        /// </summary>
        public async Task<bool> RestoreAssetAsync(Guid id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset == null || !asset.IsDeleted) return false;

            await _assetRepository.RestoreAsync(asset);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa vĩnh viễn tài sản ra khỏi cơ sở dữ liệu.
        /// </summary>
        public async Task<bool> DeletePermanentlyAsync(Guid id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset == null || !asset.IsDeleted) return false;

            await _assetRepository.DeleteAsync(asset, isSoftDelete: false);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Kích hoạt hoặc hủy kích hoạt (Toggle) trạng thái hoạt động của tài sản.
        /// </summary>
        public async Task<bool> ToggleAssetStatusAsync(Guid id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset == null) return false;

            asset.IsActive = !asset.IsActive;
            asset.LastModified = DateTime.Now;

            await _assetRepository.UpdateAsync(asset);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Kiểm tra trùng mã tài sản trong hệ thống (tránh trùng khi thêm mới hoặc cập nhật).
        /// </summary>
        public async Task<bool> IsAssetCodeDuplicateAsync(string assetCode, Guid? excludeId = null)
        {
            return await _assetRepository.IsAssetCodeDuplicateAsync(assetCode, excludeId);
        }
    }
}
