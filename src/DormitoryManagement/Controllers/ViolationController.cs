using AutoMapper;
using DormitoryManagement.Application.Dtos.Requests; 
using DormitoryManagement.Application.Services.Interfaces; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagement.Controllers
{
    [Authorize] // Bắt buộc người dùng phải đăng nhập hệ thống mới có thể truy cập
    public class ViolationController : Controller
    {
        private readonly IViolationService _violationService;
        private readonly IMapper _mapper;

        public ViolationController(IViolationService violationService, IMapper mapper)
        {
            _violationService = violationService;
            _mapper = mapper;
        }

        // 1. INDEX: DANH SÁCH VI PHẠM (PHÂN TRANG & TÌM KIẾM)
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            int pageSize = 5; // Cấu hình hiển thị 5 bản ghi trên mỗi trang giống User của bạn
            
            var result = await _violationService.GetActiveViolationsPagedAsync(page, pageSize, search);

            ViewBag.Search = search;
            return View(result);
        }

        // 2. CREATE: TẠO BIÊN BẢN VI PHẠM MỚI
        // ==========================================
        [HttpGet]
        public IActionResult Create()
        {
            // Truyền một thực thể DTO trống sang View để tránh lỗi NullReference khi render form
            return View(new ViolationRequestDto());
        }

        [HttpPost]
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

        // 3. EDIT: CHỈNH SỬA BIÊN BẢN VI PHẠM (ĐÃ SỬA LỖI AUTOMAPPER)
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var violationResponse = await _violationService.GetViolationByIdAsync(id);
            if (violationResponse == null) return NotFound();

            // ĐÃ SỬA: Không dùng _mapper.Map nữa để tránh sập trang. 
            // Truyền thẳng dữ liệu sang Edit.cshtml (vì view đó nhận @model dynamic)
            return View(violationResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ViolationRequestDto violationRequest)
        {
            if (ModelState.IsValid)
            {
                // ĐÃ SỬA: Đảm bảo truyền đúng kiểu dữ liệu request sang hàm xử lý
                await _violationService.UpdateViolationAsync(id, violationRequest);
                TempData["Success"] = "Cập nhật dữ liệu vi phạm thành công!";
                return RedirectToAction(nameof(Index));
            }
            
            // Nếu form nhập lỗi, bắt buộc trả lại chính đối tượng đó để hiển thị lại lỗi trên View
            return View(violationRequest);
        }
    }
}