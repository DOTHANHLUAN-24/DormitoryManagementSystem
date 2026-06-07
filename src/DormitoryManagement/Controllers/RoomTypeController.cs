using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
// using DormitoryManagement.Infrastructure.Data; // Để dùng SaveChangesAsync nếu chưa có UnitOfWork
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class RoomTypeController
    (
        IRoomTypeRepository roomTypeRepository,
        IUnitOfWork unitOfWork
    ) : BaseController
    {
        private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("")]
        [HttpGet("Index")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải danh sách loại phòng quản lý trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;

            var result = await _roomTypeRepository.GetPagedAsync(
                pageIndex: page,
                pageSize: pageSize,
                predicate: x => (string.IsNullOrEmpty(search) || x.TypeName.Contains(search)) && !x.IsDeleted,
                orderBy: x => x.OrderBy(rt => rt.TypeName)
            );

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<IActionResult> List(int page = 1, string search = "")
        {
            int pageSize = 6;
            if (Request.Query.TryGetValue("pageSize", out var qsPageSize) && int.TryParse(qsPageSize, out int ps) && ps > 0)
            {
                pageSize = ps;
            }
            else if (Request.Query.TryGetValue("PageSize", out var qsPageSize2) && int.TryParse(qsPageSize2, out int ps2) && ps2 > 0)
            {
                pageSize = ps2;
            }

            var result = await _roomTypeRepository.GetPagedAsync(
                pageIndex: page,
                pageSize: pageSize,
                predicate: x => (string.IsNullOrEmpty(search) || x.TypeName.Contains(search)) && !x.IsDeleted && x.IsActive,
                orderBy: x => x.OrderBy(rt => rt.TypeName)
            );

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("Details/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Details(Guid id)
        {
            Logger.LogInformation("Đang xem chi tiết loại phòng ID: {Id}", id);
            var roomType = await _roomTypeRepository.GetRoomTypeWithRoomsAsync(id);
            if (roomType == null)
            {
                Logger.LogWarning("Không tìm thấy loại phòng ID: {Id}", id);
                return NotFound();
            }

            return View(roomType);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang tạo mới loại phòng.");
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Create(RoomType roomType)
        {
            Logger.LogInformation("Đang thực hiện thêm mới loại phòng: '{TypeName}'", roomType.TypeName);
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng tên loại phòng
                if (await _roomTypeRepository.IsTypeNameDuplicateAsync(roomType.TypeName))
                {
                    Logger.LogWarning("Thêm loại phòng thất bại: Tên loại phòng '{TypeName}' đã tồn tại.", roomType.TypeName);
                    ModelState.AddModelError("TypeName", "Tên loại phòng này đã tồn tại trong hệ thống.");
                    return View(roomType);
                }

                await _roomTypeRepository.AddAsync(roomType);
                await _unitOfWork.SaveChangesAsync();

                Logger.LogInformation("Thêm loại phòng '{TypeName}' thành công.", roomType.TypeName);
                TempData["Success"] = "Thêm loại phòng mới thành công!";
                return RedirectToAction(nameof(Index));
            }

            Logger.LogWarning("Dữ liệu thêm mới loại phòng không hợp lệ.");
            return View(roomType);
        }

        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang tải trang chỉnh sửa loại phòng ID: {Id}", id);
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                Logger.LogWarning("Không tìm thấy loại phòng ID: {Id} để chỉnh sửa.", id);
                return NotFound();
            }

            return View(roomType);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Edit(Guid id, RoomType roomType)
        {
            Logger.LogInformation("Đang xử lý cập nhật loại phòng ID: {Id}", id);
            if (id != roomType.Id)
            {
                Logger.LogWarning("Cập nhật loại phòng thất bại: ID không khớp ({Id} vs {RoomTypeId}).", id, roomType.Id);
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                // Kiểm tra tên mới có bị trùng với loại phòng khác không
                if (await _roomTypeRepository.IsTypeNameDuplicateAsync(roomType.TypeName, id))
                {
                    Logger.LogWarning("Cập nhật loại phòng thất bại: Tên '{TypeName}' đã được sử dụng bởi loại phòng khác.", roomType.TypeName);
                    ModelState.AddModelError("TypeName", "Tên loại phòng này đã được sử dụng.");
                    return View(roomType);
                }

                try
                {
                    // Lấy bản ghi gốc từ DB để tránh mất dữ liệu Audit (CreatedDate)
                    var existing = await _roomTypeRepository.GetByIdAsync(id);
                    if (existing == null)
                    {
                        Logger.LogWarning("Không tìm thấy loại phòng ID: {Id} trong database.", id);
                        return NotFound();
                    }

                    // Cập nhật các trường thông tin
                    existing.TypeName = roomType.TypeName;
                    existing.BasePrice = roomType.BasePrice;
                    existing.MaxOccupants = roomType.MaxOccupants;
                    existing.Description = roomType.Description;
                    existing.IsActive = roomType.IsActive;

                    await _roomTypeRepository.UpdateAsync(existing);
                    await _unitOfWork.SaveChangesAsync();

                    Logger.LogInformation("Cập nhật loại phòng ID: {Id} thành công.", id);
                    TempData["Success"] = "Cập nhật loại phòng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Lỗi xảy ra khi cập nhật loại phòng ID: {Id}.", id);
                    ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình cập nhật.");
                }
            }
            else
            {
                Logger.LogWarning("Dữ liệu cập nhật loại phòng ID: {Id} không hợp lệ.", id);
            }
            return View(roomType);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa loại phòng ID: {Id}", id);
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                Logger.LogWarning("Không tìm thấy loại phòng ID: {Id} để xóa.", id);
                return NotFound();
            }

            // Quy trình nghiệp vụ: Kiểm tra xem loại phòng này có đang chứa phòng nào không
            var withRooms = await _roomTypeRepository.GetRoomTypeWithRoomsAsync(id);
            if (withRooms != null && withRooms.Rooms.Any(r => !r.IsDeleted))
            {
                Logger.LogWarning("Không thể xóa loại phòng ID: {Id} vì vẫn còn phòng đang thuộc loại phòng này.", id);
                TempData["Error"] = "Không thể xóa loại phòng này vì đang có phòng thuộc danh mục này.";
                return RedirectToAction(nameof(Index));
            }

            await _roomTypeRepository.DeleteAsync(roomType, isSoftDelete: true);
            await _unitOfWork.SaveChangesAsync();

            Logger.LogInformation("Xóa mềm loại phòng ID: {Id} thành công.", id);
            TempData["Success"] = "Xóa loại phòng thành công!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("RecycleBin")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải thùng rác loại phòng, trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;

            var result = await _roomTypeRepository.GetPagedAsync(
                pageIndex: page,
                pageSize: pageSize,
                predicate: x => (string.IsNullOrEmpty(search) || x.TypeName.Contains(search)) && x.IsDeleted,
                orderBy: x => x.OrderByDescending(rt => rt.LastModified),
                includeDeleted: true
            );

            ViewBag.Search = search;
            return View(result);
        }

        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khôi phục loại phòng ID: {Id}", id);
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                Logger.LogWarning("Không tìm thấy loại phòng ID: {Id} để khôi phục.", id);
                return Json(new { success = false, message = "Không tìm thấy loại phòng." });
            }

            roomType.IsDeleted = false;
            roomType.LastModified = DateTime.Now;
            await _roomTypeRepository.UpdateAsync(roomType);
            await _unitOfWork.SaveChangesAsync();

            Logger.LogInformation("Khôi phục loại phòng ID: {Id} thành công.", id);
            return Json(new { success = true, message = "Khôi phục loại phòng thành công!" });
        }

        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn loại phòng ID: {Id}", id);
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null)
            {
                Logger.LogWarning("Không tìm thấy loại phòng ID: {Id} để xóa vĩnh viễn.", id);
                return Json(new { success = false, message = "Không tìm thấy loại phòng." });
            }

            await _roomTypeRepository.DeleteAsync(roomType, isSoftDelete: false);
            await _unitOfWork.SaveChangesAsync();

            Logger.LogInformation("Xóa vĩnh viễn loại phòng ID: {Id} thành công.", id);
            return Json(new { success = true, message = "Đã xóa vĩnh viễn loại phòng khỏi hệ thống." });
        }
    }
}
