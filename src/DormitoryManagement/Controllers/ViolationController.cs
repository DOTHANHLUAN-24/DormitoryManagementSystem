using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    public class ViolationController(IViolationService violationService, IMapper mapper) : BaseController
    {
        private readonly IViolationService _violationService = violationService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("")]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            Logger.LogInformation("Đang tải danh sách biên bản vi phạm trang {Page}, tìm kiếm: '{Search}'", page, search);
            int pageSize = PageSize;

            var result = await _violationService.GetActiveViolationsPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            Logger.LogInformation("Đang truy cập trang lập biên bản vi phạm mới.");
            return View(new ViolationRequestDto());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViolationRequestDto violationDto)
        {
            Logger.LogInformation("Đang xử lý lập biên bản vi phạm mới cho sinh viên ID: {StudentId}", violationDto.StudentId);
            if (!ModelState.IsValid)
            {
                // In chi tiết các lỗi Validate ra màn hình Output để bạn dễ phát hiện lỗi khi code
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Logger.LogWarning("Dữ liệu lập biên bản vi phạm không hợp lệ: {Errors}", string.Join(", ", errors));
                return View(violationDto);
            }

            try
            {
                await _violationService.CreateViolationAsync(violationDto);
                Logger.LogInformation("Lập biên bản vi phạm thành công cho sinh viên ID: {StudentId}.", violationDto.StudentId);
                TempData["Success"] = "Lập biên bản vi phạm thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi lập biên bản vi phạm cho sinh viên ID: {StudentId}.", violationDto.StudentId);
                ModelState.AddModelError(string.Empty, "Thêm vi phạm thất bại: " + ex.Message);
                return View(violationDto);
            }
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            Logger.LogInformation("Đang xem chi tiết biên bản vi phạm ID: {Id}", id);
            var violationResponse = await _violationService.GetViolationByIdAsync(id);
            if (violationResponse == null)
            {
                Logger.LogWarning("Không tìm thấy biên bản vi phạm ID: {Id} để xem chi tiết.", id);
                return NotFound();
            }

            return View(violationResponse);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Logger.LogInformation("Đang tải trang chỉnh sửa biên bản vi phạm ID: {Id}", id);
            var violationResponse = await _violationService.GetViolationByIdAsync(id);
            if (violationResponse == null)
            {
                Logger.LogWarning("Không tìm thấy biên bản vi phạm ID: {Id} để chỉnh sửa.", id);
                return NotFound();
            }

            return View(violationResponse);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ViolationRequestDto violationRequest)
        {
            Logger.LogInformation("Đang xử lý cập nhật biên bản vi phạm ID: {Id}", id);
            if (ModelState.IsValid)
            {
                await _violationService.UpdateViolationAsync(id, violationRequest);
                Logger.LogInformation("Cập nhật biên bản vi phạm ID: {Id} thành công.", id);
                TempData["Success"] = "Cập nhật dữ liệu vi phạm thành công!";
                return RedirectToAction(nameof(Index));
            }

            Logger.LogWarning("Dữ liệu cập nhật biên bản vi phạm ID: {Id} không hợp lệ.", id);
            // Ánh xạ sang ViolationResponseDto để trả về View tương thích kiểu dữ liệu
            var violationResponse = new ViolationResponseDto
            {
                Id = id,
                StudentId = violationRequest.StudentId,
                Room = violationRequest.Room,
                Severity = violationRequest.Severity,
                Date = violationRequest.Date,
                Content = violationRequest.Content,
                Status = violationRequest.Status,
                FineAmount = violationRequest.Severity switch
                {
                    "Nhẹ" => 50000m,
                    "Trung bình" => 100000m,
                    "Nghiêm trọng" => 200000m,
                    "Cảnh cáo" => 300000m,
                    _ => 0m
                }
            };
            return View(violationResponse);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Logger.LogInformation("Đang yêu cầu xóa biên bản vi phạm ID: {Id}", id);
            try
            {
                var success = await _violationService.DeleteViolationAsync(id);
                if (success)
                {
                    Logger.LogInformation("Xóa biên bản vi phạm ID: {Id} thành công.", id);
                    return Json(new { success = true, message = "Xóa biên bản vi phạm thành công!" });
                }
                Logger.LogWarning("Không tìm thấy hoặc không thể xóa biên bản vi phạm ID: {Id}.", id);
                return Json(new { success = false, message = "Không tìm thấy biên bản vi phạm hoặc không thể xóa." });
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Lỗi xảy ra khi xóa biên bản vi phạm ID: {Id}.", id);
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}