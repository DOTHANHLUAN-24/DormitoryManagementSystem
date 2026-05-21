using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests;
using DormitoryManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

            return View(violationRequest);
        }
    }
}