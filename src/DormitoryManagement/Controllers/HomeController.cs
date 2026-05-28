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
        IUserService userService,
        IStatisticService statisticService,
        IViolationService violationService
    ) : BaseController
    {
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            Logger.LogInformation("Đang truy cập trang chủ (Index).");
            
            if (User.Identity?.IsAuthenticated == true)
            {
                // 1. Dành cho Sinh viên
                if (User.IsInRole("Student"))
                {
                    var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (Guid.TryParse(userIdString, out var userId))
                    {
                        var student = await userService.GetUserByIdAsync(userId);
                        Logger.LogInformation("Người dùng là sinh viên: {StudentName} (ID: {UserId}). Đang tải thông tin cá nhân và hóa đơn.", student?.FullName ?? User.Identity.Name, userId);
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

                        // Vi phạm kỷ luật của sinh viên
                        var myViolations = (await violationService.GetViolationsByUserIdAsync(userId)).ToList();
                        Logger.LogInformation("Sinh viên {UserId} có {Count} biên bản vi phạm.", userId, myViolations.Count);
                        ViewBag.Violations = myViolations;
                        ViewBag.PendingViolationsCount = myViolations.Count(v => v.Status != "Đã xử lý");
                        ViewBag.TotalFineAmount = myViolations.Where(v => v.Status != "Đã xử lý").Sum(v => v.FineAmount);
                    }
                }
                // 2. Dành cho Admin / Nhân viên Quản lý
                else if (User.IsInRole("Admin") || User.IsInRole("ManagementStaff") || User.IsInRole("ManagerStaff") || User.IsInRole("Manager"))
                {
                    Logger.LogInformation("Người dùng là Admin/Quản lý. Đang tải thống kê dashboard.");
                    var stats = await statisticService.GetStatisticSummaryAsync();

                    ViewBag.TotalStudents = stats.TotalStudents;
                    ViewBag.EmptyRooms = stats.EmptyRooms;
                    ViewBag.UnpaidInvoicesCount = stats.UnpaidInvoices;
                    ViewBag.NewViolationsCount = stats.NewViolations;

                    ViewBag.RoomStatusFull = stats.RoomStatusData.Length > 0 ? stats.RoomStatusData[0] : 0;
                    ViewBag.RoomStatusAvailable = stats.RoomStatusData.Length > 1 ? stats.RoomStatusData[1] : 0;
                    ViewBag.RoomStatusMaintenance = stats.RoomStatusData.Length > 2 ? stats.RoomStatusData[2] : 0;
                    ViewBag.TotalRooms = ViewBag.RoomStatusFull + ViewBag.RoomStatusAvailable + ViewBag.RoomStatusMaintenance;

                    ViewBag.TotalBeds = stats.BedOccupancyStats.TotalBeds;
                    ViewBag.OccupiedBeds = stats.BedOccupancyStats.OccupiedBeds;
                    ViewBag.OccupancyRate = stats.BedOccupancyStats.OccupancyRate;

                    ViewBag.ActiveContractsCount = stats.ContractStats.ActiveCount;
                    ViewBag.PendingContractsCount = stats.ContractStats.PendingCount;

                    ViewBag.OpenMaintenanceCount = stats.MaintenanceStats.OpenCount;
                    ViewBag.InProgressMaintenanceCount = stats.MaintenanceStats.InProgressCount;

                    // Doanh thu 6 tháng gần nhất để vẽ biểu đồ
                    ViewBag.RevenueLabels = stats.RevenueLabels;
                    ViewBag.RevenuePaid = stats.RevenuePaid;
                    ViewBag.RevenueUnpaid = stats.RevenueUnpaid;
                }
                // 3. Dành cho Nhân viên Kỹ thuật
                else if (User.IsInRole("TechnicalStaff"))
                {
                    Logger.LogInformation("Người dùng là Nhân viên Kỹ thuật. Đang tải thống kê yêu cầu sửa chữa.");
                    var stats = await statisticService.GetStatisticSummaryAsync();

                    ViewBag.OpenMaintenanceCount = stats.MaintenanceStats.OpenCount;
                    ViewBag.InProgressMaintenanceCount = stats.MaintenanceStats.InProgressCount;
                    ViewBag.ResolvedMaintenanceCount = stats.MaintenanceStats.ResolvedCount;
                    ViewBag.ClosedMaintenanceCount = stats.MaintenanceStats.ClosedCount;
                }
            }

            return View();
        }

        [HttpGet("Rooms")]
        public async Task<IActionResult> Rooms(RoomFilterRequest filter)
        {
            filter.PageNumber = filter.PageNumber > 0 ? filter.PageNumber : 1;
            filter.PageSize = 6; // 3 cột * 2 hàng

            Logger.LogInformation("Đang truy cập danh sách phòng công khai trang {Page}, tìm kiếm: '{Search}', tòa: '{BlockId}', loại: '{RoomTypeId}'", filter.PageNumber, filter.SearchTerm, filter.BlockId, filter.RoomTypeId);
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
            Logger.LogInformation("Đang xem chi tiết phòng công khai ID: {Id}", id);
            var room = await roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                Logger.LogWarning("Không tìm thấy thông tin phòng ID: {Id}", id);
                return NotFound();
            }

            return View(room);
        }

        [HttpGet("Rooms/{id}/AvailableBeds")]
        public async Task<IActionResult> GetAvailableBeds(Guid id)
        {
            Logger.LogInformation("Đang lấy danh sách giường trống cho phòng ID: {Id}", id);
            var room = await roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                Logger.LogWarning("Không tìm thấy phòng ID: {Id} để lấy danh sách giường trống.", id);
                return NotFound();
            }

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
            Logger.LogInformation("Sinh viên {Username} truy cập trang dịch vụ tiện ích.", CurrentUserName);
            
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdString, out var userId))
            {
                var contracts = await contractService.GetByUserIdAsync(userId);
                var activeContract = contracts.FirstOrDefault(c => c.Status == ContractStatus.Active);
                if (activeContract != null && activeContract.Bed?.RoomId != null)
                {
                    var usages = await utilityService.GetUtilityUsagesByRoomIdAsync(activeContract.Bed.RoomId);
                    ViewBag.UtilityUsages = usages;
                    ViewBag.RoomNumber = activeContract.Bed.Room.RoomNumber;
                }
                else
                {
                    ViewBag.UtilityUsages = new List<UtilityUsage>();
                    ViewBag.RoomNumber = null;
                }

                var requests = await utilityService.GetServiceRequestsByUserIdAsync(userId);
                ViewBag.ServiceRequests = requests;
            }
            else
            {
                ViewBag.UtilityUsages = new List<UtilityUsage>();
                ViewBag.RoomNumber = null;
                ViewBag.ServiceRequests = new List<UtilityServiceRequest>();
            }

            var utilities = await utilityService.GetAllActiveUtilitiesAsync();
            return View(utilities);
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            Logger.LogInformation("Đang truy cập trang chính sách bảo mật.");
            return View();
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Logger.LogWarning("Có lỗi hệ thống xảy ra. Request ID: {RequestId}", requestId);
            return View(new ErrorViewModel { RequestId = requestId });
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            Logger.LogInformation("Đang truy cập trang liên hệ.");
            return View();
        }

        [HttpGet("Guide")]
        public IActionResult Guide()
        {
            Logger.LogInformation("Đang truy cập trang hướng dẫn sử dụng.");
            return View();
        }
    }
}
