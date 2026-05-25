using System.Diagnostics;
using System.Security.Claims;
using DormitoryManagement.Application.Dtos.Requests.Rooms;
using DormitoryManagement.Application.Interfaces.Services;
using DormitoryManagement.Application.Services.Interfaces;
using DormitoryManagement.Models;
using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DormitoryManagement.Controllers
{
    [Route("/")]
    [AllowAnonymous]
    public class HomeController(
        IRoomService roomService,
        IUtilityService utilityService,
        IBlockService blockService,
        IRoomTypeService roomTypeService,
        IContractService contractService,
        IInvoiceService invoiceService,
        IUserService userService
    ) : BaseController
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated == true && User.IsInRole("Student"))
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userIdString, out var userId))
                {
                    var student = await userService.GetUserByIdAsync(userId);
                    var contracts = await contractService.GetByUserIdAsync(userId);
                    var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active);

                    ViewBag.HasActiveContract = activeContract != null;
                    if (activeContract != null)
                    {
                        var bedNumber = activeContract.Bed?.BedNumber ?? "Chưa xếp giường";
                        var roomNumber = activeContract.Bed?.Room?.RoomNumber ?? "Chưa xếp phòng";
                        var blockName = activeContract.Bed?.Room?.Block?.BlockName ?? "";
                        var roomType = activeContract.Bed?.Room?.RoomType?.TypeName ?? "";
                        var roomPrice = activeContract.Bed?.Room?.RoomType?.BasePrice ?? 0m;

                        ViewBag.RoomName = string.IsNullOrEmpty(blockName) ? roomNumber : $"{roomNumber} - {blockName}";
                        ViewBag.BedNumber = bedNumber;
                        ViewBag.RoomType = roomType;
                        ViewBag.RoomPrice = roomPrice;
                        ViewBag.ContractCode = activeContract.ContractCode;
                        ViewBag.StartDate = activeContract.StartDate.ToString("dd/MM/yyyy");
                        ViewBag.EndDate = activeContract.EndDate.ToString("dd/MM/yyyy");

                        // Tìm bạn cùng phòng
                        if (activeContract.Bed?.RoomId != null)
                        {
                            var allActiveContracts = await contractService.GetPagedContractsAsync(1, 1000, status: ContractStatus.Active);
                            var roommates = allActiveContracts.Items
                                .Where(c => c.Bed?.RoomId == activeContract.Bed.RoomId && c.UserId != userId)
                                .Select(c => c.User?.FullName ?? "Ẩn danh")
                                .ToList();
                            ViewBag.Roommates = roommates;
                        }
                    }

                    // Lấy tất cả hóa đơn của sinh viên
                    var allInvoices = new List<Invoice>();
                    foreach (var c in contracts)
                    {
                        var invoices = await invoiceService.GetByContractIdAsync(c.Id);
                        allInvoices.AddRange(invoices);
                    }

                    // Sắp xếp và lấy ra 4 hóa đơn gần nhất để vẽ biểu đồ
                    var latestInvoices = allInvoices
                        .OrderBy(i => i.BillingYear)
                        .ThenBy(i => i.BillingMonth)
                        .TakeLast(4)
                        .ToList();

                    var chartMonths = new List<string>();
                    var roomFees = new List<decimal>();
                    var electricityFees = new List<decimal>();
                    var waterFees = new List<decimal>();

                    foreach (var invoice in latestInvoices)
                    {
                        chartMonths.Add($"Tháng {invoice.BillingMonth}/{invoice.BillingYear}");
                        
                        // Tiền phòng gốc
                        var roomFee = invoice.Contract?.Bed?.Room?.RoomType?.BasePrice ?? 0m;
                        roomFees.Add(roomFee);

                        // Tiền điện từ UtilityUsages
                        var elec = invoice.UtilityUsages.FirstOrDefault(u => u.Utility?.UtilityName?.Contains("Điện", StringComparison.OrdinalIgnoreCase) == true);
                        electricityFees.Add(elec?.TotalAmount ?? 0m);

                        // Tiền nước từ UtilityUsages
                        var wat = invoice.UtilityUsages.FirstOrDefault(u => u.Utility?.UtilityName?.Contains("Nước", StringComparison.OrdinalIgnoreCase) == true);
                        waterFees.Add(wat?.TotalAmount ?? 0m);
                    }

                    ViewBag.ChartMonths = chartMonths;
                    ViewBag.RoomFees = roomFees;
                    ViewBag.ElectricityFees = electricityFees;
                    ViewBag.WaterFees = waterFees;
                    ViewBag.StudentName = student?.FullName ?? User.Identity.Name;

                    // Hóa đơn chưa thanh toán
                    var unpaidInvoices = allInvoices.Where(i => i.Status == InvoiceStatus.Unpaid || i.Status == InvoiceStatus.Overdue).ToList();
                    ViewBag.UnpaidAmount = unpaidInvoices.Sum(i => i.TotalAmount);
                    ViewBag.UnpaidCount = unpaidInvoices.Count;
                }
            }

            return View();
        }

        [HttpGet("Rooms")]
        public async Task<IActionResult> Rooms(RoomFilterRequest filter)
        {
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = 6; // 3 cột * 2 hàng

            var pagedRooms = await roomService.GetPagedRoomsAsync(filter);

            var blocks = await blockService.GetAllBlocksAsync();
            var roomTypes = await roomTypeService.GetAllRoomTypesAsync();

            ViewBag.Blocks = new SelectList(blocks, "Id", "BlockName", filter.BlockId);
            ViewBag.RoomTypes = new SelectList(roomTypes, "Id", "TypeName", filter.RoomTypeId);
            ViewBag.Filter = filter;

            return View(pagedRooms);
        }

        [HttpGet("Rooms/Details/{id}")]
        public async Task<IActionResult> RoomDetails(Guid id)
        {
            var room = await roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            return View(room);
        }

        [HttpGet("Rooms/{id}/AvailableBeds")]
        public async Task<IActionResult> GetAvailableBeds(Guid id)
        {
            var room = await roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            var availableBeds = room.Beds
                .Where(b => !b.IsOccupied)
                .Select(b => new { id = b.Id, bedNumber = b.BedNumber })
                .ToList();

            return Json(availableBeds);
        }

        [HttpGet("Services")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Services()
        {
            var utilities = await utilityService.GetAllActiveUtilitiesAsync();
            return View(utilities);
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet("Guide")]
        public IActionResult Guide()
        {
            return View();
        }
    }
}
