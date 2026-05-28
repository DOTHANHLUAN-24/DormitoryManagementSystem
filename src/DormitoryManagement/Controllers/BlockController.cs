using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Blocks;
using DormitoryManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize(Roles = "Admin,ManagerStaff,ManagementStaff")]
    public class BlockController(IBlockService blockService) : BaseController
    {
        private readonly IBlockService _blockService = blockService;

        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải danh sách tòa nhà hoạt động trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            var result = await _blockService.GetActiveBlocksPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("RecycleBin")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải danh sách tòa nhà đã xóa trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            var result = await _blockService.GetDeletedBlocksPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            Logger.LogInformation("Đang xem chi tiết tòa nhà ID: {Id}", id);
            var block = await _blockService.GetBlockByIdAsync(id);
            if (block == null)
            {
                Logger.LogWarning("Không tìm thấy tòa nhà với ID: {Id}", id);
                return NotFound();
            }

            return View(block);
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang thêm mới tòa nhà.");
            return View(new BlockRequestDto());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlockRequestDto request)
        {
            Logger.LogInformation("Đang thực hiện thêm mới tòa nhà với tên: '{BlockName}'", request.BlockName);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu thêm mới tòa nhà không hợp lệ.");
                return View(request);
            }

            try
            {
                // Hàm CreateBlockAsync trong Service sẽ ném Exception nếu trùng tên
                await _blockService.CreateBlockAsync(request);

                Logger.LogInformation("Thêm tòa nhà '{BlockName}' thành công.", request.BlockName);
                TempData["Success"] = "Thêm tòa nhà thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi thêm tòa nhà '{BlockName}'.", request.BlockName);
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
        }


        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang truy cập trang chỉnh sửa tòa nhà ID: {Id}", id);
            var blockResponse = await _blockService.GetBlockByIdAsync(id);
            if (blockResponse == null)
            {
                Logger.LogWarning("Không tìm thấy tòa nhà ID: {Id} để chỉnh sửa.", id);
                return NotFound();
            }

            // Chuyển dữ liệu từ Response sang Request để nạp vào Form
            // (Nếu ở MappingProfile bạn có Map từ Response sang Request thì dùng AutoMapper)
            // Dưới đây map tay cho an toàn vì các trường khá ít:
            var requestDto = new BlockRequestDto
            {
                BlockName = blockResponse.BlockName,
                TotalFloors = blockResponse.TotalFloors,
                Description = blockResponse.Description,
                IsActive = blockResponse.IsActive
            };

            ViewBag.BlockId = blockResponse.Id; // Giữ lại ID để POST
            return View(requestDto);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BlockRequestDto request)
        {
            Logger.LogInformation("Đang xử lý yêu cầu cập nhật thông tin tòa nhà ID: {Id}", id);
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Dữ liệu cập nhật tòa nhà ID: {Id} không hợp lệ.", id);
                ViewBag.BlockId = id;
                return View(request);
            }

            try
            {
                var result = await _blockService.UpdateBlockAsync(id, request);
                if (result)
                {
                    Logger.LogInformation("Cập nhật tòa nhà ID: {Id} thành công.", id);
                    TempData["Success"] = "Cập nhật thông tin tòa nhà thành công!";
                    return RedirectToAction(nameof(Index));
                }

                Logger.LogWarning("Cập nhật tòa nhà ID: {Id} thất bại (Không tìm thấy).", id);
                ModelState.AddModelError("", "Cập nhật thất bại. Không tìm thấy tòa nhà.");
                ViewBag.BlockId = id;
                return View(request);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi cập nhật tòa nhà ID: {Id}.", id);
                ModelState.AddModelError("", ex.Message);
                ViewBag.BlockId = id;
                return View(request);
            }
        }


        [HttpPost("SoftDelete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete([FromRoute] Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa mềm tòa nhà ID: {Id}", id);
            if (id == Guid.Empty)
            {
                Logger.LogWarning("Yêu cầu xóa mềm thất bại do ID không hợp lệ.");
                return Json(new { success = false, message = "ID không hợp lệ" });
            }

            try
            {
                var result = await _blockService.SoftDeleteBlockAsync(id);
                if (result)
                {
                    Logger.LogInformation("Đã xóa mềm tòa nhà ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Đã chuyển tòa nhà vào thùng rác." });
                }
                Logger.LogWarning("Không tìm thấy tòa nhà ID: {Id} để xóa mềm.", id);
                return Json(new { success = false, message = "Không thể tìm thấy tòa nhà để xóa." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi xóa mềm tòa nhà ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu khôi phục tòa nhà ID: {Id}", id);
            try
            {
                var result = await _blockService.RestoreBlockAsync(id);
                if (result)
                {
                    Logger.LogInformation("Khôi phục tòa nhà ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Khôi phục tòa nhà thành công." });
                }
                Logger.LogWarning("Không tìm thấy tòa nhà ID: {Id} để khôi phục.", id);
                return Json(new { success = false, message = "Không thể tìm thấy tòa nhà để khôi phục." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi khôi phục tòa nhà ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa vĩnh viễn tòa nhà ID: {Id}", id);
            try
            {
                var result = await _blockService.DeletePermanentlyAsync(id);
                if (result)
                {
                    Logger.LogInformation("Xóa vĩnh viễn tòa nhà ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Xóa vĩnh viễn thành công." });
                }
                Logger.LogWarning("Không tìm thấy tòa nhà ID: {Id} để xóa vĩnh viễn.", id);
                return Json(new { success = false, message = "Không thể xóa. Tòa nhà không tồn tại." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi xóa vĩnh viễn tòa nhà ID: {Id}.", id);
                // Bắt lỗi Forein Key (Nếu tòa nhà vẫn còn phòng thì SQL sẽ báo lỗi)
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                if (message.Contains("REFERENCE") || message.Contains("FOREIGN KEY"))
                {
                    return Json(new { success = false, message = "Không thể xóa! Tòa nhà này đang chứa dữ liệu các phòng." });
                }

                return Json(new { success = false, message = "Lỗi hệ thống: " + message });
            }
        }

        [HttpGet("Suspended")]
        public async Task<IActionResult> SuspendedList(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải danh sách tòa nhà tạm ngưng trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;
            var result = await _blockService.GetSuspendedBlocksPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpPost("ToggleStatus/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu thay đổi trạng thái hoạt động tòa nhà ID: {Id}", id);
            try
            {
                var result = await _blockService.ToggleBlockStatusAsync(id);
                if (result)
                {
                    Logger.LogInformation("Đã thay đổi trạng thái hoạt động tòa nhà ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Đã thay đổi trạng thái hoạt động của tòa nhà." });
                }
                Logger.LogWarning("Không tìm thấy tòa nhà ID: {Id} để thay đổi trạng thái hoặc đã bị xóa.", id);
                return Json(new { success = false, message = "Không tìm thấy tòa nhà hoặc tòa nhà đã bị xóa." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi thay đổi trạng thái hoạt động tòa nhà ID: {Id}.", id);
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}