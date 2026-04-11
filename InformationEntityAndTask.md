# ☠️ Tóm gọn toàn bộ đối tượng có trong hệ thống quản lý ký túc xá
## Viết dưới dạng class trong C# ⚔️

``` csharp
#region Enums
public enum RoomStatus { Available, Full, Maintenance, Reserved }
public enum BedStatus { Available, Occupied, Maintenance }
public enum ContractStatus { Active, Expired, Terminated, Pending }
public enum InvoiceStatus { Unpaid, Paid, Overdue, PartiallyPaid }
public enum MaintenancePriority { Low, Medium, High, Urgent }
public enum MaintenanceStatus { Open, InProgress, Resolved, Closed }
public enum ViolationStatus { Pending, Resolved, Appealed }
public enum PaymentMethod { Cash, BankTransfer, CreditCard, EWallet }
public enum AssetStatus { Good, Broken, UnderRepair, Lost }
public enum UserRole { Admin, Manager, Student, Guard, Accountant }
#endregion

#region 0. Nhóm Người dùng (Users) - BỔ SUNG MỚI

// Người dùng
public class User : IdentityUser
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string FullName { get; set; }
    [Required, StringLength(50)]
    public string Code { get; set; } // MSSV hoặc Mã nhân viên
    public bool IsActive { get; set; } = true;
    public string IdentityCardNumber { get; set; } // CCCD/CMND - 13 số
    // Phần có sẵn trong IdentityRole
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public UserRole Role { get; set; } // Nếu không dùng IdentityRole - để tạm
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}

#endregion

#region 1. Nhóm Hạ tầng (Infrastructure)

// Vị trí tòa nhà
public class Block
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string BlockName { get; set; }
    public int TotalFloors { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}

// Loại phòng
public class RoomType
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(50)]
    public string TypeName { get; set; }
    public decimal BasePrice { get; set; }
    public int MaxOccupants { get; set; } // Bằng số lượng Bed trong phòng
    public string Description { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}

// Phòng
public class Room
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(20)]
    public string RoomNumber { get; set; }
    public int Floor { get; set; }
    public RoomStatus Status { get; set; } = RoomStatus.Available;

    public int BlockId { get; set; }
    [ForeignKey("BlockId")]
    public virtual Block Block { get; set; }

    public int RoomTypeId { get; set; }
    [ForeignKey("RoomTypeId")]
    public virtual RoomType RoomType { get; set; }

    // Liên kết
    public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();
    public virtual ICollection<UtilityUsage> UtilityUsages { get; set; } = new List<UtilityUsage>();
    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}

// Quản lý chi tiết từng giường - gần giống quân sự khu B (Giường có ghi tên)
public class Bed
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(20)]
    public string BedNumber { get; set; } // Ví dụ: G01, G02
    public BedStatus Status { get; set; } = BedStatus.Available;

    public int RoomId { get; set; }
    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}

#endregion

#region 2. Nhóm Quản lý Thuê & Dịch vụ (Leasing & Utilities)

// Hợp đồng
public class Contract
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(50)]
    public string ContractCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal DepositAmount { get; set; }
    public ContractStatus Status { get; set; } = ContractStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    // Map với User và Bed thay vì Room
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    public int BedId { get; set; }
    [ForeignKey("BedId")]
    public virtual Bed Bed { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();
}

// Dịch vụ / tiện ích (Điện, nước, internet, phí giữ xe)
public class Utility
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string UtilityName { get; set; } // Điện, Nước...
    public decimal UnitPrice { get; set; }
    public string Unit { get; set; }
    public bool IsActive { get; set; } = true;
}

// Số lần sử dụng cụ thể của **Dịch vụ / tiện ích**
public class UtilityUsage
{
    [Key]
    public int Id { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public double PreviousIndex { get; set; }
    public double CurrentIndex { get; set; }
    public double UsageQuantity { get; set; }
    public decimal TotalAmount { get; set; }

    // Gắn với Room để ghi nhận hàng tháng, có thể map nullable với Invoice
    public int RoomId { get; set; }
    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; }

    public int UtilityId { get; set; }
    [ForeignKey("UtilityId")]
    public virtual Utility Utility { get; set; }

    public int? InvoiceId { get; set; }
    [ForeignKey("InvoiceId")]
    public virtual Invoice Invoice { get; set; }
}

#endregion

#region 3. Nhóm Tài chính (Finance)

// Hóa đơn
public class Invoice
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(50)]
    public string InvoiceCode { get; set; }
    public string Title { get; set; }
    public int BillingMonth { get; set; }
    public int BillingYear { get; set; }
    public decimal TotalAmount { get; set; } // Tiền phòng + Điện nước + Phụ phí - Đã trả
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int ContractId { get; set; }
    [ForeignKey("ContractId")]
    public virtual Contract Contract { get; set; }

    public virtual ICollection<UtilityUsage> UtilityUsages { get; set; } = new List<UtilityUsage>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<Surcharge> Surcharges { get; set; } = new List<Surcharge>();
}

// Thanh toán
public class Payment
{
    [Key]
    public int Id { get; set; }
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public string TransactionCode { get; set; }
    public PaymentMethod Method { get; set; }
    public string Note { get; set; }

    public int InvoiceId { get; set; }
    [ForeignKey("InvoiceId")]
    public virtual Invoice Invoice { get; set; }
}

// Phụ phí kèm theo
public class Surcharge
{
    [Key]
    public int Id { get; set; }
    public string SurchargeName { get; set; } // Gửi xe, dọn vệ sinh...
    public decimal Amount { get; set; }

    public int InvoiceId { get; set; }
    [ForeignKey("InvoiceId")]
    public virtual Invoice Invoice { get; set; }
}

#endregion

#region 4. Nhóm Vận hành & An ninh (Operations)

// Yêu cầu bảo trì
public class MaintenanceRequest
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(200)]
    public string Title { get; set; }
    public string Description { get; set; }
    public MaintenancePriority Priority { get; set; }
    public MaintenanceStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ResolvedAt { get; set; }

    public int RoomId { get; set; }
    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; }

    public int RequesterId { get; set; }
    [ForeignKey("RequesterId")]
    public virtual User Requester { get; set; }
}

// Vi phạm
public class Violation
{
    [Key]
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal FineAmount { get; set; }
    public DateTime ViolationDate { get; set; }
    public ViolationStatus Status { get; set; }
    public string EvidenceImage { get; set; }

    public int ContractId { get; set; }
    [ForeignKey("ContractId")]
    public virtual Contract Contract { get; set; }
}

// Tài sản
public class Asset
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string AssetName { get; set; }
    public string AssetCode { get; set; }
    public AssetStatus Status { get; set; } = AssetStatus.Good;

    public int RoomId { get; set; }
    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; }
}

// Lưu dữ liệu khách đến thăm
public class VisitorLog
{
    [Key]
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string VisitorName { get; set; }
    public string IdNumber { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string Purpose { get; set; }

    public int HostId { get; set; } // Người được thăm
    [ForeignKey("HostId")]
    public virtual User Host { get; set; }
}

// Phương tiện
public class Vehicle
{
    [Key]
    public int Id { get; set; }
    public string VehicleType { get; set; }
    public string LicensePlate { get; set; }

    public int OwnerId { get; set; }
    [ForeignKey("OwnerId")]
    public virtual User Owner { get; set; }
}

#endregion
```
---

# 📖 ĐẶC TẢ ĐỐI TƯỢNG - HỆ THỐNG QUẢN LÝ KÝ TÚC XÁ (CANVAS)
## 👤 1. Nhóm Người dùng (Users)

### User - Sẽ được kế thừa từ [IdentityUser](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.identityuser?view=aspnetcore-8.0)

* **Mô tả**: Lưu thông tin tất cả đối tượng sử dụng hệ thống (Sinh viên, Quản lý, Bảo vệ, Kế toán)
* **Thuộc tính chính**: Id, Name, Email, Phone, Role
* **Quan hệ**:

  * 1 - N với Contract (1 User có nhiều hợp đồng)
  * 1 - N với MaintenanceRequest (1 User có thể tạo nhiều yêu cầu)
  * 1 - N với Violation (1 User có thể có nhiều vi phạm)
  * 1 - N với Vehicle (1 User sở hữu nhiều phương tiện)
  * 1 - N với VisitorLog (1 User có thể là Host của nhiều lượt khách)

---

## 🏢 2. Nhóm Hạ tầng (Infrastructure)

### Block

* **Mô tả**: Tòa nhà (A1, B2...)
* **Quan hệ**:

  * 1 - N với Room (1 Block có nhiều Room)

### RoomType

* **Mô tả**: Loại phòng (4 giường, 2 giường VIP)
* **Thuộc tính**: BasePrice
* **Quan hệ**:

  * 1 - N với Room (1 loại áp dụng cho nhiều phòng)

### Room

* **Mô tả**: Phòng cụ thể
* **Quan hệ**:

  * N - 1 với Block
  * N - 1 với RoomType
  * 1 - N với Bed (1 phòng có nhiều giường)
  * 1 - N với UtilityUsage (1 phòng có nhiều bản ghi điện nước)
  * 1 - N với Asset (1 phòng có nhiều tài sản)

### Bed

* **Mô tả**: Giường
* **Quan hệ**:

  * N - 1 với Room
  * 1 - N với Contract (1 giường có nhiều hợp đồng theo thời gian, nhưng chỉ 1 active)

---

## 📜 3. Nhóm Hợp đồng & Dịch vụ (Leasing & Utilities)

### Contract

* **Mô tả**: Hợp đồng thuê giường
* **Thuộc tính**: StartDate, EndDate, Deposit
* **Quan hệ**:

  * N - 1 với User
  * N - 1 với Bed
  * 1 - N với Invoice (1 hợp đồng có nhiều hóa đơn)

### Utility

* **Mô tả**: Loại dịch vụ (Điện, Nước)
* **Quan hệ**:

  * 1 - N với UtilityUsage

### UtilityUsage

* **Mô tả**: Chỉ số sử dụng
* **Thuộc tính**: OldIndex, NewIndex
* **Quan hệ**:

  * N - 1 với Room
  * N - 1 với Utility
  * N - 1 với Invoice (được gộp vào hóa đơn)

---

## 💰 4. Nhóm Tài chính (Finance)

### Invoice

* **Mô tả**: Hóa đơn tổng hợp
* **Thuộc tính**: TotalAmount, DueDate
* **Quan hệ**:

  * N - 1 với Contract
  * 1 - N với Payment (1 hóa đơn có nhiều lần thanh toán)
  * 1 - N với Surcharge (1 hóa đơn có nhiều phụ phí)
  * 1 - N với UtilityUsage (gom nhiều chỉ số)

### Payment

* **Mô tả**: Thanh toán
* **Quan hệ**:

  * N - 1 với Invoice

### Surcharge

* **Mô tả**: Phụ phí
* **Quan hệ**:

  * N - 1 với Invoice

---

## 🛠️ 5. Nhóm Vận hành & An ninh (Operations)

### MaintenanceRequest

* **Mô tả**: Yêu cầu sửa chữa
* **Quan hệ**:

  * N - 1 với User
  * N - 1 với Room

### Violation

* **Mô tả**: Vi phạm
* **Quan hệ**:

  * N - 1 với User

### Asset

* **Mô tả**: Tài sản phòng
* **Quan hệ**:

  * N - 1 với Room
  * 1 - 1 với Surcharge (nếu hỏng/mất sẽ tạo phụ phí tương ứng)

### VisitorLog

* **Mô tả**: Khách ra vào
* **Quan hệ**:

  * N - 1 với User (Host)

### Vehicle

* **Mô tả**: Phương tiện
* **Quan hệ**:

  * N - 1 với User

---

## 📊 TỔNG QUAN QUAN HỆ CHÍNH

* User ↔ Contract ↔ Bed ↔ Room ↔ Block
* Room ↔ UtilityUsage ↔ Utility
* Contract ↔ Invoice ↔ Payment
* Invoice ↔ Surcharge
* Room ↔ Asset

---

## 💡 Task nâng cao cần làm cho dự án bao gồm: 

* [Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio)
* Thêm bảng **Role** riêng (User - Role: N - 1) __Thay bằng__ [IdentityRole](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.entityframeworkcore.identityrole?view=aspnetcore-1.1&viewFallbackFrom=aspnetcore-8.0).
* Thêm bảng **Permission** nếu cần [RBAC - Role-Based Access Control](https://codestar.vn/tim-hieu-ve-rbac-va-abac/) để chỉ định xem với quyền này chỉ được sử dụng những chức năng nào, để đảm bảo phần bảo mật tránh cho người dùng những quyền không cần thiết. 
* Tách **InvoiceItem** để quản lý chi tiết từng dòng tiền (Đang nghĩ cách ...)
* Dùng **Soft Delete** cho dữ liệu quan trọng: Tránh xóa dữ liệu gây loạn dữ liệu khi liên kết. Một số cách đọc để tham khảo [link](https://livebook.manning.com/book/entity-framework-core-in-action/chapter-8)
* Dùng một số interface để làm base cơ bản cho toàn bộ Entity trong db để viết nhanh hơn (...)
* Tách project theo dạng [Repo-Service-Controller](https://dotnettutorials.net/lesson/repository-design-pattern-csharp/)
* Thêm JWT Authentication [docx](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-8.0)
* Thêm bảng AuditLogs: Kiểm soát ai đã làm gì trong hệ thống. Tránh gây ra lỗi mà không biết ai làm
    * Ví dụ:
    - User A đã tạo hợp đồng
    - Admin đã chỉnh sửa phòng
    - Admin đã xóa phòng
* Thêm Serilog vào trong hệ thống để support cho Backend API đọc dữ liệu lỗi ở đâu
* Tích hợp [FluentValidation](https://docs.fluentvalidation.net/en/latest/) support việc kiểm tra dữ liệu để tránh thất thoát và bỏ sót dữ liệu lỗi
* Áp dụng Middleware để bọc toàn bộ web giúp xử lý dữ liệu mượt mà
* Tạo ra Global Exception Handling bằng cách áp dụng middleware
* Tách các Entity thành các Data Transfer Object [DTO](https://shareprogramming.net/dto-la-gi-dung-dto-trong-nhung-truong-hop-nao/) để phục vụ cho việc trao đổi dữ liệu giữa các layer, giúp tránh lộ thông tin nhạy cảm và tối ưu dữ liệu trả về. Áp dụng thư viện [AutoMapper](https://docs.automapper.io/en/stable/) để tự động ánh xạ giữa Entity và DTO.
* Cố [cache data](https://learn.microsoft.com/en-us/dotnet/core/extensions/caching) để tăng hiệu suất của website - có thể làm

---
