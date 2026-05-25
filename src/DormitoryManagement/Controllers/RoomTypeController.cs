using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Repositories;
using DormitoryManagement.Domain.Interfaces.UnitOfWork;
using DormitoryManagement.Infrastructure.Data; // Để dùng SaveChangesAsync nếu chưa có UnitOfWork
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
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
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
            var roomType = await _roomTypeRepository.GetRoomTypeWithRoomsAsync(id);
            if (roomType == null) return NotFound();

            return View(roomType);
        }

        [HttpGet("Create")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Create(RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng tên loại phòng
                if (await _roomTypeRepository.IsTypeNameDuplicateAsync(roomType.TypeName))
                {
                    ModelState.AddModelError("TypeName", "Tên loại phòng này đã tồn tại trong hệ thống.");
                    return View(roomType);
                }

                await _roomTypeRepository.AddAsync(roomType);
                await _unitOfWork.SaveChangesAsync();

                TempData["Success"] = "Thêm loại phòng mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null) return NotFound();

            return View(roomType);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Edit(Guid id, RoomType roomType)
        {
            if (id != roomType.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                // Kiểm tra tên mới có bị trùng với loại phòng khác không
                if (await _roomTypeRepository.IsTypeNameDuplicateAsync(roomType.TypeName, id))
                {
                    ModelState.AddModelError("TypeName", "Tên loại phòng này đã được sử dụng.");
                    return View(roomType);
                }

                try
                {
                    // Lấy bản ghi gốc từ DB để tránh mất dữ liệu Audit (CreatedDate)
                    var existing = await _roomTypeRepository.GetByIdAsync(id);
                    if (existing == null) return NotFound();

                    // Cập nhật các trường thông tin
                    existing.TypeName = roomType.TypeName;
                    existing.BasePrice = roomType.BasePrice;
                    existing.MaxOccupants = roomType.MaxOccupants;
                    existing.Description = roomType.Description;
                    existing.IsActive = roomType.IsActive;

                    await _roomTypeRepository.UpdateAsync(existing);
                    await _unitOfWork.SaveChangesAsync();

                    TempData["Success"] = "Cập nhật loại phòng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình cập nhật.");
                }
            }
            return View(roomType);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(id);
            if (roomType == null) return NotFound();

            // Quy trình nghiệp vụ: Kiểm tra xem loại phòng này có đang chứa phòng nào không
            var withRooms = await _roomTypeRepository.GetRoomTypeWithRoomsAsync(id);
            if (withRooms != null && withRooms.Rooms.Any(r => !r.IsDeleted))
            {
                TempData["Error"] = "Không thể xóa loại phòng này vì đang có phòng thuộc danh mục này.";
                return RedirectToAction(nameof(Index));
            }

            await _roomTypeRepository.DeleteAsync(roomType, isSoftDelete: true);
            await _unitOfWork.SaveChangesAsync();

            TempData["Success"] = "Xóa loại phòng thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}