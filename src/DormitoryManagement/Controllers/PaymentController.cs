using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace DormitoryManagement.Controllers
{
    public class PaymentController : Controller
    {
        // Đường dẫn sẽ là /Payment hoặc /Payment/Index
        public IActionResult Index()
        {
            // Tạo dữ liệu giả (Mock Data) để xem giao diện hiển thị như thế nào mà không cần Database
            var mockPayments = new List<dynamic>
            {
                new { InvoiceCode = "HD-2024-001", StudentName = "Nguyễn Văn A", RoomName = "Phòng 101", Type = "Tiền phòng", Amount = 1500000, Status = "Đã thanh toán" },
                new { InvoiceCode = "HD-2024-002", StudentName = "Trần Thị B", RoomName = "Phòng 202", Type = "Điện nước", Amount = 450000, Status = "Chưa thanh toán" },
                new { InvoiceCode = "HD-2024-003", StudentName = "Lê Văn C", RoomName = "Phòng 105", Type = "Dịch vụ", Amount = 200000, Status = "Quá hạn" },
                new { InvoiceCode = "HD-2024-004", StudentName = "Phạm Minh D", RoomName = "Phòng 301", Type = "Tiền phòng", Amount = 1500000, Status = "Đã thanh toán" }
            };

            // Truyền danh sách này sang View
            return View(mockPayments);
        }
    }
}
