using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests.Blocks;
using DormitoryManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize(Roles = "Admin,ManagerStaff")]
    public class BlockController(IBlockService blockService) : BaseController
    {
        private readonly IBlockService _blockService = blockService;

        [HttpGet("")]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            int pageSize = PageSize;
            var result = await _blockService.GetActiveBlocksPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("RecycleBin")]
        public async Task<IActionResult> RecycleBin(int page = 1, string search = "")
        {
            int pageSize = PageSize;
            var result = await _blockService.GetDeletedBlocksPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var block = await _blockService.GetBlockByIdAsync(id);
            if (block == null) return NotFound();

            return View(block);
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new BlockRequestDto());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlockRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                // Hàm CreateBlockAsync trong Service sẽ ném Exception nếu trùng tên
                await _blockService.CreateBlockAsync(request);

                TempData["Success"] = "Thêm tòa nhà thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }
        }


        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blockResponse = await _blockService.GetBlockByIdAsync(id);
            if (blockResponse == null) return NotFound();

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
            if (!ModelState.IsValid)
            {
                ViewBag.BlockId = id;
                return View(request);
            }

            try
            {
                var result = await _blockService.UpdateBlockAsync(id, request);
                if (result)
                {
                    TempData["Success"] = "Cập nhật thông tin tòa nhà thành công!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Cập nhật thất bại. Không tìm thấy tòa nhà.");
                ViewBag.BlockId = id;
                return View(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.BlockId = id;
                return View(request);
            }
        }


        [HttpPost("SoftDelete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return Json(new { success = false, message = "ID không hợp lệ" });

            try
            {
                var result = await _blockService.SoftDeleteBlockAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Đã chuyển tòa nhà vào thùng rác." });
                }
                return Json(new { success = false, message = "Không thể tìm thấy tòa nhà để xóa." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("Restore/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            try
            {
                var result = await _blockService.RestoreBlockAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Khôi phục tòa nhà thành công." });
                }
                return Json(new { success = false, message = "Không thể tìm thấy tòa nhà để khôi phục." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("DeletePermanently/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanently(Guid id)
        {
            try
            {
                var result = await _blockService.DeletePermanentlyAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Xóa vĩnh viễn thành công." });
                }
                return Json(new { success = false, message = "Không thể xóa. Tòa nhà không tồn tại." });
            }
            catch (Exception ex)
            {
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
            int pageSize = PageSize;
            var result = await _blockService.GetSuspendedBlocksPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpPost("ToggleStatus/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            try
            {
                var result = await _blockService.ToggleBlockStatusAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "Đã thay đổi trạng thái hoạt động của tòa nhà." });
                }
                return Json(new { success = false, message = "Không tìm thấy tòa nhà hoặc tòa nhà đã bị xóa." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}