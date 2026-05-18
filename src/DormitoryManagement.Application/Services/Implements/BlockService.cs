using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Blocks;
using DormitoryManagement.Application.Dtos.Responses.Blocks;
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
        private readonly IMapper _mapper;

        public BlockService(IBlockRepository blockRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _blockRepository = blockRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<BlockResponseDto>> GetActiveBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm)
        {
            var pagedResult = await _blockRepository.SearchBlocksAsync(searchTerm ?? "", pageIndex, pageSize);

            // Map từ Entity sang Response DTO
            return new PagedResult<BlockResponseDto>(
                _mapper.Map<List<BlockResponseDto>>(pagedResult.Items),
                pagedResult.TotalCount,
                pageIndex,
                pageSize
            );
        }

        public async Task<PagedResult<BlockResponseDto>> GetDeletedBlocksPagedAsync(int pageIndex, int pageSize, string? searchTerm)
        {
            var pagedResult = await _blockRepository.GetByStatusPagedAsync(
                pageIndex, pageSize, isActive: null, isDeleted: true,
                predicate: b => string.IsNullOrEmpty(searchTerm) || b.BlockName.Contains(searchTerm));

            return new PagedResult<BlockResponseDto>(
                _mapper.Map<List<BlockResponseDto>>(pagedResult.Items),
                pagedResult.TotalCount,
                pageIndex,
                pageSize
            );
        }

        public async Task<BlockResponseDto?> GetBlockByIdAsync(Guid id)
        {
            var block = await _blockRepository.GetBlockWithRoomsAsync(id);
            return _mapper.Map<BlockResponseDto>(block);
        }

        public async Task<IEnumerable<BlockResponseDto>> GetAllBlocksAsync()
        {
            var blocks = await _blockRepository.GetAllAsync(includeDeleted: false);
            return _mapper.Map<IEnumerable<BlockResponseDto>>(blocks);
        }

        public async Task<bool> CreateBlockAsync(BlockRequestDto request)
        {
            // Logic nghiệp vụ: Kiểm tra trùng tên tòa nhà
            if (await _blockRepository.IsBlockNameExistsAsync(request.BlockName))
                throw new Exception("Tên tòa nhà đã tồn tại trong hệ thống.");

            var block = _mapper.Map<Block>(request);
            await _blockRepository.AddAsync(block);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBlockAsync(Guid id, BlockRequestDto request)
        {
            var block = await _blockRepository.GetByIdAsync(id);
            if (block == null) return false;

            // Kiểm tra trùng tên (trừ chính nó)
            if (await _blockRepository.IsBlockNameExistsAsync(request.BlockName, id))
                throw new Exception("Tên tòa nhà đã bị trùng với tòa nhà khác.");

            _mapper.Map(request, block);
            await _blockRepository.UpdateAsync(block);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteBlockAsync(Guid id)
        {
            var block = await _blockRepository.GetBlockWithRoomsAsync(id);
            if (block == null) return false;

            // Logic nghiệp vụ: Không cho xóa tòa nhà nếu vẫn còn phòng đang hoạt động
            if (block.Rooms != null && block.Rooms.Any(r => !r.IsDeleted))
                throw new Exception("Không thể xóa tòa nhà vì vẫn còn phòng bên trong. Hãy xóa các phòng trước.");

            await _blockRepository.DeleteAsync(block, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RestoreBlockAsync(Guid id)
        {
            // Vì GetByIdAsync của BaseRepo mặc định không lấy IsDeleted=true (tùy cấu hình)
            // Ta có thể dùng GetByStatusAsync để tìm trong thùng rác
            var blocks = await _blockRepository.GetByStatusAsync(isDeleted: true);
            var block = blocks.FirstOrDefault(x => x.Id == id);

            if (block == null) return false;

            await _blockRepository.RestoreAsync(block);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePermanentlyAsync(Guid id)
        {
            // Tìm cả trong thùng rác
            var blocks = await _blockRepository.GetByStatusAsync(isDeleted: true);
            var block = blocks.FirstOrDefault(x => x.Id == id);

            if (block == null) return false;

            await _blockRepository.DeleteAsync(block, isSoftDelete: false);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
