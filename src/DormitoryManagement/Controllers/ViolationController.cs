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
            int pageSize = PageSize;

            var result = await _violationService.GetActiveViolationsPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new ViolationRequestDto());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViolationRequestDto violationDto)
        {
            if (!ModelState.IsValid)
            {
                // In chi tiết các lỗi Validate ra màn hình Output để bạn dễ phát hiện lỗi khi code
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine(">>> LỖI TẠI FORM CREATE VIOLATION: " + string.Join(", ", errors));
                return View(violationDto);
            }

            try
            {
                await _violationService.CreateViolationAsync(violationDto);
                TempData["Success"] = "Lập biên bản vi phạm thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Thêm vi phạm thất bại: " + ex.Message);
                return View(violationDto);
            }
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var violationResponse = await _violationService.GetViolationByIdAsync(id);
            if (violationResponse == null) return NotFound();

            return View(violationResponse);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var violationResponse = await _violationService.GetViolationByIdAsync(id);
            if (violationResponse == null) return NotFound();

            return View(violationResponse);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ViolationRequestDto violationRequest)
        {
            if (ModelState.IsValid)
            {
                await _violationService.UpdateViolationAsync(id, violationRequest);
                TempData["Success"] = "Cập nhật dữ liệu vi phạm thành công!";
                return RedirectToAction(nameof(Index));
            }

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
            try
            {
                var success = await _violationService.DeleteViolationAsync(id);
                if (success)
                {
                    return Json(new { success = true, message = "Xóa biên bản vi phạm thành công!" });
                }
                return Json(new { success = false, message = "Không tìm thấy biên bản vi phạm hoặc không thể xóa." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}