# 🏢 Hệ Thống Quản Lý Ký Túc Xá (Dormitory Management System)

Chào mừng bạn đến với dự án **Hệ Thống Quản Lý Ký Túc Xá**. Đây là đồ án chuyên đề chuyên ngành Công nghệ phần mềm, được xây dựng nhằm số hóa và tự động hóa toàn bộ quy trình quản lý ký túc xá, thay thế cho các phương pháp quản lý thủ công truyền thống qua sổ sách hay file Excel.

---

## 🎯 Mục Tiêu Dự Án
Hệ thống cung cấp giải pháp toàn diện giúp Ban quản lý và Sinh viên nội trú tương tác dễ dàng:
- **Tự động hóa** quy trình đăng ký, xét duyệt và phân bổ phòng.
- **Quản lý tập trung** thông tin sinh viên, hợp đồng, cơ sở vật chất (phòng, giường, tài sản).
- **Minh bạch tài chính** trong việc ghi nhận chỉ số, tính toán và thanh toán hóa đơn điện, nước, tiền phòng.
- **Tối ưu hóa vận hành** qua việc theo dõi yêu cầu bảo trì, vi phạm kỷ luật và quản lý khách ra vào.

---

## 🚀 Các Chức Năng Chính
- 🧑‍🎓 **Quản lý người dùng (RBAC):** Phân quyền chi tiết cho Admin, Manager, Staff, và Student.
- 🛏️ **Quản lý Phòng & Giường:** Theo dõi sơ đồ phòng, sức chứa, trạng thái phòng (trống, đã đầy, bảo trì).
- 📜 **Quản lý Hợp đồng thuê:** Tạo mới, gia hạn, thanh lý hợp đồng tự động.
- 💰 **Quản lý Hóa đơn & Tài chính:** Ghi chỉ số điện/nước, tạo hóa đơn tự động và theo dõi thanh toán/công nợ.
- 🛠️ **Quản lý Vận hành & An ninh:** Báo cáo sự cố, yêu cầu sửa chữa, theo dõi vi phạm nội quy, quản lý phương tiện và khách ra vào.

---

## 💻 Công Nghệ Và Nền Tảng Sử Dụng
Dự án được xây dựng dựa trên các tiêu chuẩn và công nghệ hiện đại, đảm bảo tính mở rộng và bảo mật cao:

### Backend
- **Ngôn ngữ:** C#
- **Framework:** ASP.NET Core 8
- **Kiến trúc:** Clean Architecture / 3-Layer Architecture kết hợp Repository Pattern & Dependency Injection.
- **Cơ sở dữ liệu:** SQL Server
- **ORM:** Entity Framework Core (Code First)
- **Bảo mật:** ASP.NET Core Identity, JWT Authentication

### Thư viện & Công cụ hỗ trợ
- **Validation:** FluentValidation
- **Mapping:** AutoMapper
- **Logging:** Serilog
- **Testing:** xUnit, Postman, Swagger
- **Quản lý mã nguồn:** Git & GitHub

---

## 📂 Cấu Trúc Dự Án (Clean Architecture)
Dự án được phân tách thành các layer độc lập nhằm tách biệt business logic và infrastructure:

```text
DormitoryManagementSystem/
│
├── src/
│   ├── DormitoryManagement/                  (API - Startup project / Controller Layer)
│   ├── DormitoryManagement.Infrastructure/   (DbContext, EF Migrations, Repositories)
│   ├── DormitoryManagement.Application/      (Business Logic, Services, DTOs, AutoMapper)
│   └── DormitoryManagement.Domain/           (Entities, Enums, Core Interfaces)
```

---

## ⚙️ Hướng Dẫn Cài Đặt (Getting Started)

### 1. Yêu cầu hệ thống
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio 2022 / VS Code

### 2. Cài đặt và khởi chạy
**Bước 1:** Clone repository về máy:
```bash
git clone <repository_url>
cd DormitoryManagementSystem/src
```

**Bước 2:** Cấu hình chuỗi kết nối cơ sở dữ liệu (`ConnectionStrings`) trong file `appsettings.json` của project `DormitoryManagement`.

**Bước 3:** Cập nhật cơ sở dữ liệu bằng EF Core Tools:
```bash
dotnet ef database update --project DormitoryManagement.Infrastructure --startup-project DormitoryManagement
```

**Bước 4:** Khởi chạy ứng dụng:
```bash
dotnet run --project DormitoryManagement
```
*Sau khi chạy, truy cập vào `https://localhost:<port>/swagger` để test các API.*

---

## 🔄 Quy Trình Làm Việc Nhóm (Git Workflow)
Chúng tôi áp dụng mô hình Agile (Scrum) và tuân thủ các nguyên tắc làm việc với Git:
1. Cập nhật code mới nhất: `git checkout main` -> `git pull`
2. Tạo nhánh tính năng mới: `git checkout -b feature/<tên-chức-năng>`
3. Commit code với thông điệp rõ ràng.
4. Push lên remote và tạo Pull Request (PR) để merge vào nhánh `main`.

*Chi tiết về quy tắc đặt tên nhánh, commit message và tiêu chuẩn code, vui lòng xem tại [Hướng Dẫn Đóng Góp](./src/DormitoryManagement/Docs/HowToContribute.md) (Contributing Guide).*

---

## 👥 Đội Ngũ Phát Triển (Nhóm 02)
- **Đỗ Thành Luân** (Trưởng nhóm - 2221050046)
- **Lê Thị Cẩm Tú** (2321050008)
- **Đỗ Quang Huy** (2221050047)
- **Vũ Thị Kim Oanh** (2221050566)

**Giảng viên hướng dẫn:** Thầy Ngô Ngọc Anh  
*Trường Đại học Mỏ - Địa chất | Hà Nội, Năm 2026*