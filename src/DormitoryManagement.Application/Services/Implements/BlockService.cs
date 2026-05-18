using System.Linq.Expressions;
using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Blocks;
using DormitoryManagement.Application.Dtos.Responses.Blocks;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Application.Services.Implements
{
    public class BlockService : IBlockService
    {
        private readonly IBlockRepository _blockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlockService(IBlockRepository blockRepository, IUnitOfWork unitOfWork)
        {
            _blockRepository = blockRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<BlockResponseDto>> GetActiveBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm)
        {
            Expression<Func<Block, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.ToLower().Trim();
                predicate = x => x.BlockName.ToLower().Contains(term) || x.Description.ToLower().Contains(term);
            }

            var pagedData = await _blockRepository.GetByStatusPagedAsync(
                pageIndex, pageSize,
                isActive: true,
                isDeleted: false,
                predicate: predicate,
                includeProperties: x => x.Rooms); // Include Rooms để đếm số phòng

            var dtoList = pagedData.Items.Select(x => new BlockResponseDto
            {
                Id = x.Id,
                BlockName = x.BlockName,
                TotalFloors = x.TotalFloors,
                Description = x.Description,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                CreatedDate = x.CreatedDate,
                TotalRooms = x.Rooms != null ? x.Rooms.Count(r => !r.IsDeleted) : 0
            }).ToList();

            return new PagedResult<BlockResponseDto>(dtoList, pagedData.TotalCount, pageIndex, pageSize);
        }

        public async Task<PagedResult<BlockResponseDto>> GetDeletedBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm)
        {
            Expression<Func<Block, bool>>? predicate = null;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.ToLower().Trim();
                predicate = x => x.BlockName.ToLower().Contains(term);
            }

            // Gọi hàm từ BaseRepo: isDeleted = true (Chỉ lấy đã xóa)
            var pagedData = await _blockRepository.GetByStatusPagedAsync(
                pageIndex, pageSize,
                isActive: null,
                isDeleted: true,
                predicate: predicate);

            var dtoList = pagedData.Items.Select(x => new BlockResponseDto
            {
                Id = x.Id,
                BlockName = x.BlockName,
                TotalFloors = x.TotalFloors,
                IsDeleted = x.IsDeleted,
                CreatedDate = x.CreatedDate
            }).ToList();

            return new PagedResult<BlockResponseDto>(dtoList, pagedData.TotalCount, pageIndex, pageSize);
        }

        public async Task<BlockResponseDto?> GetBlockByIdAsync(Guid id)
        {
            var block = await _blockRepository.GetBlockWithRoomsAsync(id);
            if (block == null) return null;

            return new BlockResponseDto
            {
                Id = block.Id,
                BlockName = block.BlockName,
                TotalFloors = block.TotalFloors,
                Description = block.Description,
                IsActive = block.IsActive,
                CreatedDate = block.CreatedDate,
                TotalRooms = block.Rooms?.Count ?? 0
            };
        }

        public async Task<IEnumerable<BlockResponseDto>> GetAllBlocksAsync()
        {
            var blocks = await _blockRepository.GetByStatusAsync(isActive: true, isDeleted: false);
            return blocks.Select(x => new BlockResponseDto
            {
                Id = x.Id,
                BlockName = x.BlockName
            });
        }

        public async Task<bool> CreateBlockAsync(BlockRequestDto request)
        {
            if (await _blockRepository.IsBlockNameExistsAsync(request.BlockName))
                throw new Exception("Tên tòa nhà đã tồn tại trong hệ thống.");

            var newBlock = new Block
            {
                BlockName = request.BlockName.Trim(),
                TotalFloors = request.TotalFloors,
                Description = request.Description?.Trim() ?? "",
                IsActive = request.IsActive
            };

            await _blockRepository.AddAsync(newBlock);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBlockAsync(Guid id, BlockRequestDto request)
        {
            var block = await _blockRepository.GetByIdAsync(id);
            if (block == null || block.IsDeleted) return false;

            // Kiểm tra trùng tên với tòa nhà khác
            if (await _blockRepository.IsBlockNameExistsAsync(request.BlockName, id))
                throw new Exception("Tên tòa nhà này đã được sử dụng.");

            block.BlockName = request.BlockName.Trim();
            block.TotalFloors = request.TotalFloors;
            block.Description = request.Description?.Trim() ?? "";
            block.IsActive = request.IsActive;

            await _blockRepository.UpdateAsync(block);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteBlockAsync(Guid id)
        {
            var block = await _blockRepository.GetByIdAsync(id);
            if (block == null || block.IsDeleted) return false;

            // Chỗ này nếu kỹ có thể kiểm tra: Nếu tòa nhà đang có phòng thì không cho xóa
            // if (block.Rooms.Any(r => !r.IsDeleted)) throw new Exception("Không thể xóa tòa nhà đang có phòng");

            await _blockRepository.DeleteAsync(block, isSoftDelete: true);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RestoreBlockAsync(Guid id)
        {
            var block = await _blockRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (block == null || !block.IsDeleted) return false;

            await _blockRepository.RestoreAsync(block);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePermanentlyAsync(Guid id)
        {
            var block = await _blockRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (block == null) return false;

            await _blockRepository.DeleteAsync(block, isSoftDelete: false);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<PagedResult<BlockResponseDto>> GetSuspendedBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm)
        {
            Expression<Func<Block, bool>>? predicate = null;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.ToLower().Trim();
                predicate = x => x.BlockName.ToLower().Contains(term) || x.Description.ToLower().Contains(term);
            }

            var pagedData = await _blockRepository.GetByStatusPagedAsync(
                pageIndex, pageSize,
                isActive: false,
                isDeleted: false,
                predicate: predicate,
                includeProperties: x => x.Rooms);

            var dtoList = pagedData.Items.Select(x => new BlockResponseDto
            {
                Id = x.Id,
                BlockName = x.BlockName,
                TotalFloors = x.TotalFloors,
                Description = x.Description,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                CreatedDate = x.CreatedDate,
                TotalRooms = x.Rooms != null ? x.Rooms.Count(r => !r.IsDeleted) : 0
            }).ToList();

            return new PagedResult<BlockResponseDto>(dtoList, pagedData.TotalCount, pageIndex, pageSize);
        }

        public async Task<bool> ToggleBlockStatusAsync(Guid id)
        {
            var block = await _blockRepository.GetByIdAsync(id);
            if (block == null || block.IsDeleted) return false;

            block.IsActive = !block.IsActive;

            await _blockRepository.UpdateAsync(block);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}