using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

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

        public async Task<IEnumerable<Block>> GetAllBlocksAsync(bool includeDeleted = false)
        {
            return await _blockRepository.GetAllAsync(includeDeleted);
        }

        public async Task<PagedResult<Block>> GetPagedBlocksAsync(int pageIndex, int pageSize, string? searchTerm = null)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Sử dụng hàm GetPagedAsync có sẵn ở BaseRepository
                return await _blockRepository.GetPagedAsync(
                    pageIndex,
                    pageSize,
                    predicate: x => !x.IsDeleted,
                    orderBy: x => x.OrderBy(b => b.BlockName));
            }

            // Sử dụng hàm Search đã viết ở BlockRepository
            return await _blockRepository.SearchBlocksAsync(searchTerm, pageIndex, pageSize);
        }

        public async Task<Block?> GetBlockByIdAsync(Guid id)
        {
            return await _blockRepository.GetByIdAsync(id);
        }

        public async Task<Block?> GetBlockWithRoomsAsync(Guid id)
        {
            return await _blockRepository.GetBlockWithRoomsAsync(id);
        }

        public async Task<bool> CreateBlockAsync(Block block)
        {
            // 1. Logic nghiệp vụ: Kiểm tra trùng tên
            if (await _blockRepository.IsBlockNameExistsAsync(block.BlockName))
            {
                throw new InvalidOperationException($"Tòa nhà có tên '{block.BlockName}' đã tồn tại.");
            }

            // 2. Thiết lập mặc định
            block.Id = Guid.NewGuid();
            block.CreatedDate = DateTime.UtcNow;
            block.IsActive = true;
            block.IsDeleted = false;

            await _blockRepository.AddAsync(block);

            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateBlockAsync(Block block)
        {
            var existingBlock = await _blockRepository.GetByIdAsync(block.Id);
            if (existingBlock == null || existingBlock.IsDeleted)
                throw new KeyNotFoundException("Không tìm thấy tòa nhà hoặc đã bị xóa.");

            // Kiểm tra trùng tên với các block khác
            if (await _blockRepository.IsBlockNameExistsAsync(block.BlockName, block.Id))
            {
                throw new InvalidOperationException($"Tên tòa nhà '{block.BlockName}' đã được sử dụng bởi khu vực khác.");
            }

            // Cập nhật thông tin
            existingBlock.BlockName = block.BlockName;
            existingBlock.TotalFloors = block.TotalFloors;
            existingBlock.Description = block.Description;
            existingBlock.IsActive = block.IsActive;

            await _blockRepository.UpdateAsync(existingBlock);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteBlockAsync(Guid id, bool isSoftDelete = true)
        {
            var block = await _blockRepository.GetBlockWithRoomsAsync(id);
            if (block == null) throw new KeyNotFoundException("Không tìm thấy tòa nhà.");

            // Nghiệp vụ: Không được xóa nếu tòa nhà đang có phòng (tùy yêu cầu)
            if (block.Rooms.Any(r => !r.IsDeleted))
            {
                throw new InvalidOperationException("Không thể xóa tòa nhà vì vẫn còn phòng đang tồn tại.");
            }

            await _blockRepository.DeleteAsync(block, isSoftDelete);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> RestoreBlockAsync(Guid id)
        {
            var block = await _blockRepository.GetByIdAsync(id);
            if (block == null) throw new KeyNotFoundException("Không tìm thấy tòa nhà.");

            await _blockRepository.RestoreAsync(block);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> IsNameDuplicateAsync(string name, Guid? excludeId = null)
        {
            return await _blockRepository.IsBlockNameExistsAsync(name, excludeId);
        }
    }
}
