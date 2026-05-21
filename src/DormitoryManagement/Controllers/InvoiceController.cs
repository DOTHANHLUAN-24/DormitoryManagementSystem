using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
// using DormitoryManagement.Application.Services.Interfaces;
// using DormitoryManagement.Application.Dtos.Requests.Invoices;

namespace DormitoryManagement.Controllers
{
    [Route("Invoice")]
    [Authorize]
    public class InvoiceController : Controller
    {
        // private readonly IInvoiceService _invoiceService;
        // private readonly IMapper _mapper;

        // public InvoiceController(IInvoiceService invoiceService, IMapper mapper)
        // {
        //     _invoiceService = invoiceService;
        //     _mapper = mapper;
        // }

        /// <summary>
        /// Hiển thị danh sách hóa đơn (Trang Index)
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            // Hiện tại View đang dùng Mock Data bên trong file .cshtml
            return View();
        }

        /// <summary>
        /// GET: Hiển thị form tạo hóa đơn mới
        /// </summary>
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Xử lý lưu hóa đơn mới
        /// </summary>
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(object invoiceDto) // Thay object bằng InvoiceRequestDto khi có
        {
            if (!ModelState.IsValid) return View(invoiceDto);

            // Logic gọi Service lưu DB sẽ nằm ở đây
            TempData["Success"] = "Tạo hóa đơn thành công!";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: Hiển thị form chỉnh sửa hóa đơn
        /// </summary>
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(string id)
        {
            // Trong thực tế sẽ lấy dữ liệu từ _invoiceService.GetByCodeAsync(id)
            return View();
        }

        /// <summary>
        /// POST: Cập nhật thông tin hóa đơn
        /// </summary>
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, object updateDto) // Thay object bằng InvoiceUpdateDto
        {
            if (!ModelState.IsValid) return View(updateDto);

            TempData["Success"] = "Cập nhật hóa đơn thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
