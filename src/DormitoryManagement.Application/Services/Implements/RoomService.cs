using System.Linq.Expressions;
using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Dtos.Responses.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Mappings;
using DormitoryManagement.Domain.Common;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;

namespace DormitoryManagement.Application.Services
{
    /// <summary>
    /// Lớp triển khai dịch vụ quản lý phòng (RoomService).
    /// </summary>
    public class RoomService(IRoomRepository roomRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRoomService
    {
        private readonly IRoomRepository _roomRepository = roomRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Lấy danh sách phòng phân trang kèm theo bộ lọc nâng cao (tòa nhà, loại phòng, trạng thái, khoảng giá...).
        /// </summary>
        public async Task<PagedResult<RoomResponse>> GetPagedRoomsAsync(RoomFilterRequest filter)
        {
            var result = await _roomRepository.GetByStatusPagedAsync(
                filter.PageNumber,
                filter.PageSize,
                isActive: true,
                isDeleted: false,
                predicate: r =>
                    (string.IsNullOrEmpty(filter.SearchTerm) || r.RoomNumber.Contains(filter.SearchTerm)) &&
                    (!filter.BlockId.HasValue || r.BlockId == filter.BlockId) &&
                    (!filter.RoomTypeId.HasValue || r.RoomTypeId == filter.RoomTypeId) &&
                    (!filter.Status.HasValue || r.Status == filter.Status) &&
                    (!filter.MinPrice.HasValue || r.RoomType.BasePrice >= filter.MinPrice) &&
                    (!filter.MaxPrice.HasValue || r.RoomType.BasePrice <= filter.MaxPrice),
                includeProperties: new Expression<Func<Room, object>>[] { r => r.Block, r => r.RoomType }
            );

            return result.MapToPagedResult<Room, RoomResponse>(_mapper);
        }

        /// <summary>
        /// Lấy danh sách phòng đã xóa mềm phân trang.
        /// </summary>
        public async Task<PagedResult<RoomResponse>> GetDeletedRoomsPagedAsync(RoomFilterRequest filter)
        {
            var result = await _roomRepository.GetByStatusPagedAsync(
                filter.PageNumber,
                filter.PageSize,
                isActive: null,
                isDeleted: true,
                predicate: r => string.IsNullOrEmpty(filter.SearchTerm) || r.RoomNumber.Contains(filter.SearchTerm),
                includeProperties: new Expression<Func<Room, object>>[] { r => r.Block, r => r.RoomType }
            );

            return result.MapToPagedResult<Room, RoomResponse>(_mapper);
        }

        /// <summary>
        /// Lấy chi tiết phòng (kèm theo danh sách giường và các trang thiết bị tài sản) theo Id.
        /// </summary>
        public async Task<RoomDetailResponse?> GetRoomByIdAsync(Guid id)
        {
            var room = await _roomRepository.GetRoomWithFullDetailsAsync(id);
            return _mapper.Map<RoomDetailResponse>(room);
        }

        /// <summary>
        /// Lấy danh sách phòng thuộc một tòa cụ thể.
        /// </summary>
        public async Task<IEnumerable<RoomResponse>> GetRoomsByBlockAsync(Guid blockId)
        {
            var rooms = await _roomRepository.GetRoomsByBlockAsync(blockId);
            return _mapper.Map<IEnumerable<RoomResponse>>(rooms);
        }

        /// <summary>
        /// Tạo mới một phòng và lưu vào cơ sở dữ liệu.
        /// </summary>
        public async Task<bool> CreateRoomAsync(CreateRoomRequest request)
        {
            if (await _roomRepository.IsRoomNumberDuplicateAsync(request.RoomNumber, request.BlockId))
                throw new Exception("Số phòng này đã tồn tại trong tòa nhà.");

            var room = _mapper.Map<Room>(request);
            room.Id = Guid.NewGuid();
            room.CreatedDate = DateTime.UtcNow;

            await _roomRepository.AddAsync(room);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Cập nhật thông tin phòng.
        /// </summary>
        public async Task<bool> UpdateRoomAsync(Guid id, UpdateRoomRequest request)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return false;

            if (await _roomRepository.IsRoomNumberDuplicateAsync(request.RoomNumber, request.BlockId, id))
                throw new Exception("Số phòng bị trùng với phòng khác.");

            _mapper.Map(request, room);
            room.LastModified = DateTime.UtcNow;

            await _roomRepository.UpdateAsync(room);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa mềm một phòng (chỉ được xóa khi phòng không có sinh viên ở).
        /// </summary>
        public async Task<bool> DeleteRoomAsync(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return false;

            // Ví dụ: Không cho xóa phòng đang trạng thái "Full" (Đã đầy)
            if (room.Status == RoomStatus.Full)
                throw new Exception("Không thể xóa phòng đang có sinh viên cư trú.");

            await _roomRepository.DeleteAsync(room, isSoftDelete: true);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Khôi phục phòng đã bị xóa mềm về hoạt động lại bình thường.
        /// </summary>
        public async Task<bool> RestoreRoomAsync(Guid id)
        {
            // Lấy trực tiếp từ repo (bao gồm cả trạng thái xóa)
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null || !room.IsDeleted) return false;

            await _roomRepository.RestoreAsync(room);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Xóa vĩnh viễn phòng khỏi DB.
        /// </summary>
        public async Task<bool> DeletePermanentlyAsync(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null || !room.IsDeleted) return false;

            await _roomRepository.DeleteAsync(room, isSoftDelete: false);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Lấy thống kê phòng (tổng số phòng, phòng đầy, phòng trống, bảo trì...).
        /// </summary>
        public async Task<RoomStatisticsDto> GetRoomStatisticsAsync()
        {
            var allRooms = await _roomRepository.GetAllAsync(); // Hoặc dùng IQueryable để tối ưu hiệu năng
            return new RoomStatisticsDto
            {
                TotalRooms = allRooms.Count(),
                AvailableRooms = allRooms.Count(r => r.Status == RoomStatus.Available),
                OccupiedRooms = allRooms.Count(r => r.Status == RoomStatus.Full),
                MaintenanceRooms = allRooms.Count(r => r.Status == RoomStatus.Maintenance)
            };
        }
    }
}