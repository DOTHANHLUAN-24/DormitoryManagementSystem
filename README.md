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

Hệ thống được thiết kế tối ưu hóa trải nghiệm cho từng nhóm đối tượng người dùng qua các trang Dashboard chuyên biệt:

### 🧑‍🎓 Sinh Viên Nội Trú (Student Portal)

- Đăng ký thuê phòng và theo dõi tình trạng phê duyệt hợp đồng.
- Xem chi tiết thông tin phòng ở hiện tại, số giường và danh sách bạn cùng phòng.
- Theo dõi lịch sử hóa đơn hàng tháng trực quan thông qua biểu đồ tiền phòng, tiền điện, tiền nước.
- Báo cáo sự cố, yêu cầu sửa chữa thiết bị hỏng hóc trong phòng.
- Xem lịch sử vi phạm nội quy và theo dõi tình trạng xử lý biên bản vi phạm.

### 🏢 Nhân Viên Quản Lý (Management Portal)

- Quản lý sơ đồ phòng ở, tòa nhà (Block), loại phòng và giường trống theo thời gian thực.
- Quản lý thông tin sinh viên nội trú và quá trình gửi phương tiện.
- Phê duyệt đăng ký phòng, tạo mới, gia hạn hoặc thanh lý hợp đồng thuê.
- Ghi chỉ số điện/nước định kỳ, tạo hóa đơn tự động và theo dõi công nợ, doanh thu trực quan qua biểu đồ Dashboard.
- Quản lý khách ra vào ký túc xá (Visitor logs).

### 🛠️ Nhân Viên Kỹ Thuật (Maintenance Portal)

- Theo dõi danh sách yêu cầu bảo trì từ sinh viên và ban quản lý.
- Cập nhật trạng thái xử lý sự cố (Mở, Đang xử lý, Đã hoàn thành, Đóng).
- Xem thống kê các yêu cầu kỹ thuật cần giải quyết để tối ưu hóa công việc.

### 👑 Quản Trị Hệ Thống (Admin Panel)

- CRUD toàn bộ các thực thể dữ liệu trong hệ thống.
- Quản lý tài khoản người dùng, phân vai trò chi tiết (RBAC).
- Cấu hình các tham số hệ thống, khóa/mở khóa tài khoản người dùng.

---

## 💻 Công Nghệ Và Nền Tảng Sử Dụng

Dự án được xây dựng dựa trên các tiêu chuẩn và công nghệ hiện đại, đảm bảo tính mở rộng và bảo mật cao:

### Kiến Trúc Xác Thực & Phân Quyền

- **Xác thực kết hợp (Hybrid Authentication):** Hệ thống kết hợp giữa mô hình ứng dụng **ASP.NET Core MVC** truyền thống (phục vụ giao diện người dùng trực quan qua các Views) và **Web API** (cung cấp các API endpoints bảo mật cho tích hợp dịch vụ).
- **JWT Cookie Authentication:** Cơ chế đăng nhập dựa trên **ASP.NET Core Identity** kết hợp với **JWT Token**. Sau khi đăng nhập thành công, JWT Token sẽ được lưu tự động dưới dạng HTTP-only Cookie (`JWTToken`). Nhờ đó, các yêu cầu từ giao diện MVC và các cuộc gọi API từ client đều được xác thực đồng bộ và bảo mật cao.

### Backend & Cơ sở dữ liệu

- **Ngôn ngữ:** C#
- **Framework:** ASP.NET Core 8
- **Kiến trúc:** Clean Architecture (Kiến trúc sạch) kết hợp Repository Pattern & Unit of Work nhằm tách biệt business logic và infrastructure.
- **Cơ sở dữ liệu:** SQL Server
- **ORM:** Entity Framework Core (Code First)

### Thư viện & Công cụ hỗ trợ

- **Validation:** FluentValidation (Kiểm tra tính hợp lệ của dữ liệu đầu vào)
- **Mapping:** AutoMapper (Tự động ánh xạ dữ liệu giữa Entities và DTOs)
- **Logging:** Serilog (Ghi log lỗi và thông tin vận hành hệ thống)
- **Testing & API Docs:** Postman, Swagger
- **Quản lý mã nguồn:** Git & GitHub

---

## 📂 Cấu Trúc Dự Án (Clean Architecture)

Dự án được phân tách thành các layer độc lập nhằm tách biệt business logic, data access và presentation:

```text
DormitoryManagementSystem/
│
├── src/
│   ├── DormitoryManagement/                  (MVC & API - Startup project / Controller & Presentation Layer)
│   ├── DormitoryManagement.Application/      (Business Logic, Services, Interfaces, DTOs, AutoMapper, FluentValidation)
│   ├── DormitoryManagement.Infrastructure/   (Data Access, DbContext, EF Migrations, Repositories, DbSeeder, MailService)
│   ├── DormitoryManagement.Domain/           (Entities, Enums, Core Interfaces)
│   └── DormitoryManagement.Common/           (Shared utilities, Helpers, Constants)
```

---

## ⚙️ Hướng Dẫn Cài Đặt (Getting Started)

### 1. Yêu cầu hệ thống

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (hoặc SQL LocalDB)
- Visual Studio 2022 / VS Code

### 2. Cài đặt và khởi chạy

**Bước 1:** Clone repository về máy:

```bash
git clone <repository_url>
cd DormitoryManagementSystem/src
```

**Bước 2:** Cấu hình chuỗi kết nối cơ sở dữ liệu (`ConnectionStrings`) trong file `appsettings.Development.json` (phục vụ môi trường local phát triển) hoặc `appsettings.json` của project `DormitoryManagement`.

**Bước 3:** Cập nhật cơ sở dữ liệu bằng EF Core Tools:

```bash
dotnet ef database update --project DormitoryManagement.Infrastructure --startup-project DormitoryManagement
```

**Bước 4:** Khởi chạy ứng dụng:

```bash
dotnet run --project DormitoryManagement
```

*Sau khi chạy, truy cập vào `https://localhost:<port>/` để truy cập giao diện MVC hoặc `https://localhost:<port>/swagger` để thử nghiệm các API.*

> [!TIP]
> **Tự động khởi tạo dữ liệu mẫu (Auto-Seeding):**
> Khi ứng dụng khởi chạy lần đầu trong môi trường **Development**, hệ thống sẽ tự động gọi `DbSeeder` để tạo lập toàn bộ dữ liệu mẫu (Dãy nhà, Phòng ở, Giường ngủ, Sinh viên, Hợp đồng thuê, Hóa đơn dịch vụ, Sự cố sửa chữa...). Bạn có thể đăng nhập ngay để trải nghiệm bằng các tài khoản mẫu dưới đây.

---

## 🔑 Tài Khoản Thử Nghiệm Mặc Định (Default Accounts)

Hệ thống được cung cấp sẵn một tập hợp tài khoản mẫu phục vụ việc kiểm thử các luồng chức năng và phân quyền (RBAC):

| Vai trò (Role) | Username | Password (Mật khẩu) | Mô tả / Chức năng kiểm thử |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin` | `Admin@123` | Quản trị viên tối cao: Quản lý người dùng, phân quyền, cấu hình hệ thống. |
| **Management Staff** | `manager1`<br>`manager2` | `Manager@123` | Nhân viên quản lý: Phê duyệt hợp đồng, ghi điện nước, thu phí, quản lý phòng/giường. |
| **Technical Staff** | `tech1`<br>`tech2`<br>`tech3` | `Tech@123` | Nhân viên kỹ thuật: Tiếp nhận và xử lý các yêu cầu sửa chữa, bảo trì thiết bị. |
| **Student** | Mã sinh viên mẫu:<br>`2221010001`<br>`2221010002`<br>... | `Student@123` | Sinh viên nội trú: Gửi yêu cầu sửa chữa, xem hóa đơn dịch vụ, danh sách bạn cùng phòng. |

---

## 🔄 Quy Trình Làm Việc Nhóm (Git Workflow)

Chúng tôi áp dụng mô hình Agile (Scrum) và tuân thủ các nguyên tắc làm việc với Git:

1. Cập nhật code mới nhất: `git checkout main` -> `git pull`
2. Tạo nhánh tính năng mới: `git checkout -b feature/<tên-chức-năng>`
3. Commit code với thông điệp rõ ràng theo chuẩn Conventional Commits.
4. Push lên remote và tạo Pull Request (PR) để merge vào nhánh `main`.

*Chi tiết về quy tắc đặt tên nhánh, commit message và tiêu chuẩn code, vui lòng xem tại [Hướng Dẫn Đóng Góp](./src/DormitoryManagement/Docs/HowToContribute.md) (Contributing Guide).*

---

## 📚 Tài Liệu Thiết Kế & Vận Hành (Documentation)

Dưới đây là các tài liệu kỹ thuật chi tiết của dự án nằm trong thư mục `Docs/`:

- 📄 [Phân Tích và Thiết Kế Hệ Thống](./src/DormitoryManagement/Docs/PhanTichVaThietKeHeThong.md) - Tài liệu SRS chi tiết về kiến trúc dự án và thiết kế cơ sở dữ liệu.
- 📄 [Chi Tiết Phân Quyền & Thuật Toán](./src/DormitoryManagement/Docs/phanQuyen.md) - Mô tả thuật toán xếp phòng, tính tiền điện nước, tính công nợ và cơ chế Xóa mềm (Soft Delete).
- 📄 [Yêu Cầu Chức Năng & Phi Chức Năng](./src/DormitoryManagement/Docs/FunctionAndNonFunction.md) - Đặc tả yêu cầu phần mềm chi tiết.
- 📄 [Hướng Dẫn Đóng Góp Mã Nguồn](./src/DormitoryManagement/Docs/HowToContribute.md) - Quy tắc Git branch, commit message và Coding Convention của nhóm.
- 📄 [Hướng Dẫn Sử Dụng VS Code để Migration](./src/DormitoryManagement/Docs/EFCoreMigrationGuide/UsingVSCode.md) - Các bước cấu hình và chạy Entity Framework Core Migration trên Visual Studio Code.

---

## 👥 Đội Ngũ Phát Triển (Nhóm 02)

- **Đỗ Thành Luân** (Trưởng nhóm - 2221050046)
- **Lê Thị Cẩm Tú** (2321050008)
- **Đỗ Quang Huy** (2221050047)
- **Vũ Thị Kim Oanh** (2221050566)

**Giảng viên hướng dẫn:** Thầy Ngô Ngọc Anh  
*Trường Đại học Mỏ - Địa chất | Hà Nội, Năm 2026*
