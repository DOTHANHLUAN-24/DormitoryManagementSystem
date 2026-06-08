# 🏢 Hệ Thống Quản Lý Ký Túc Xá (Dormitory Management System - DMS)

[![Framework](https://img.shields.io/badge/.NET-8.0-blueviolet.svg?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Database](https://img.shields.io/badge/Database-SQL_Server-blue.svg?style=flat-square&logo=microsoft-sql-server)](https://www.microsoft.com/en-us/sql-server/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_Architecture-green.svg?style=flat-square)](#-cấu-trúc-dự-án-clean-architecture)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](https://opensource.org/licenses/MIT)

Chào mừng bạn đến với dự án **Hệ Thống Quản Lý Ký Túc Xá (DMS)**. Đây là đồ án chuyên đề chuyên ngành Công nghệ phần mềm, được xây dựng nhằm số hóa và tự động hóa toàn bộ quy trình quản lý ký túc xá học sinh - sinh viên, thay thế cho các phương pháp quản lý thủ công truyền thống qua sổ sách hoặc file Excel.

---

## 🎯 Mục Tiêu Dự Án

Hệ thống cung cấp giải pháp toàn diện và tối ưu giúp Ban quản lý ký túc xá và Sinh viên nội trú dễ dàng quản lý và tương tác:
*   **Tự động hóa:** Quy trình đăng ký trực tuyến, xét duyệt tự động, và phân bổ phòng/giường.
*   **Quản lý tập trung:** Quản lý cơ sở dữ liệu tập trung về sinh viên, hợp đồng thuê, hạ tầng phòng ở (tòa nhà, phòng, giường, tài sản).
*   **Minh bạch tài chính:** Tự động hóa việc ghi nhận chỉ số điện nước hàng tháng, tạo lập hóa đơn, quản lý phụ phí và ghi nhận lịch sử thanh toán trực quan.
*   **Tối ưu hóa vận hành:** Theo dõi các yêu cầu bảo trì sửa chữa của phòng, quản lý vi phạm nội quy, quản lý phương tiện gửi xe, và nhật ký khách ra vào.

---

## 🚀 Các Chức Năng Chính Theo Phân Quyền (RBAC)

Hệ thống được thiết kế tối ưu hóa trải nghiệm cho từng nhóm đối tượng người dùng qua các trang Dashboard và tính năng chuyên biệt:

### 🧑‍🎓 1. Cổng Sinh Viên (Student Portal)
*   **Đăng ký thuê phòng:** Chọn giường trống trực tuyến và theo dõi tiến độ phê duyệt hợp đồng.
*   **Quản lý phòng ở:** Xem thông tin phòng hiện tại, giường số mấy, danh sách bạn cùng phòng và thông tin liên hệ.
*   **Hóa đơn & Thanh toán:** Theo dõi lịch sử hóa đơn hàng tháng trực quan thông qua biểu đồ tiền phòng, tiền điện, tiền nước.
*   **Yêu cầu bảo trì:** Báo cáo các sự cố hỏng hóc thiết bị (bóng đèn, điều hòa, khóa cửa...) trong phòng trực tiếp đến bộ phận kỹ thuật.
*   **Lịch sử vi phạm:** Xem danh sách biên bản vi phạm nội quy và tiến độ xử lý/đóng phạt hành chính.

### 🏢 2. Cổng Nhân Viên Quản Lý (Management Portal)
*   **Sơ đồ phòng ở trực quan:** Quản lý danh sách tòa nhà (Block), loại phòng, phòng ở và chi tiết từng giường trống theo thời gian thực.
*   **Hợp đồng & Đăng ký:** Tiếp nhận và duyệt đăng ký phòng của sinh viên, tạo mới, gia hạn, hoặc thanh lý hợp đồng thuê.
*   **Hóa đơn & Dịch vụ:** Ghi nhận chỉ số điện/nước định kỳ, tự động tính toán chi phí, lập hóa đơn tự động và theo dõi công nợ, doanh thu trực quan qua biểu đồ Dashboard.
*   **Quản lý phụ trợ:** Quản lý danh sách xe gửi, ghi nhận khách ra vào ký túc xá (Visitor log).

### 🛠️ 3. Cổng Nhân Viên Kỹ Thuật (Maintenance Portal)
*   **Tiếp nhận sự cố:** Theo dõi danh sách yêu cầu sửa chữa thiết bị từ các phòng được phân công.
*   **Cập nhật tiến độ:** Đổi trạng thái xử lý sự cố (Mở $\to$ Đang xử lý $\to$ Đã sửa xong $\to$ Đóng).
*   **Thống kê cá nhân:** Xem số lượng công việc cần hoàn thành trong ngày để tối ưu thời gian xử lý.

### 👑 4. Quản Trị Hệ Thống (Admin Panel)
*   **CRUD thực thể:** Quản trị toàn bộ các thực thể dữ liệu trong hệ thống.
*   **Phân quyền (RBAC):** Quản lý tài khoản, gán quyền chi tiết (Admin, Manager, Student, Guard, Tech, Accountant).
*   **Bảo mật:** Khóa/mở khóa tài khoản, giám sát hoạt động hệ thống.

---

## 🎨 Giao Diện Responsive & Trải Nghiệm Di Động (Responsive UI)

*   **Thiết kế Thích ứng (Responsive Design):** Bố cục tự động co giãn và tối ưu hóa trên mọi kích thước màn hình từ Desktop, Tablet đến Smartphone (từ 360px trở lên).
*   **Thanh menu kéo trượt (Overlay Drawer):** Trên các thiết bị di động, thanh Sidebar được ẩn gọn gàng và có thể kéo trượt mượt mà bằng nút điều hướng góc trên, kèm theo lớp phủ mờ (`backdrop-filter: blur`) hiện đại.
*   **Bảng dữ liệu & Biểu mẫu thích ứng:** Các bảng dữ liệu được hỗ trợ vuốt cuộn ngang mượt mà. Hệ thống lưới tự động chuyển sang dạng cột dọc cho các biểu mẫu và thanh lọc tìm kiếm trên di động để tăng không gian nhập liệu.

---

## 💻 Công Nghệ Và Nền Tảng Sử Dụng

### 🏗️ Kiến Trúc Xác Thực & Phân Quyền
*   **Xác thực kết hợp (Hybrid Authentication):** Hệ thống kết hợp giữa mô hình ứng dụng **ASP.NET Core MVC** truyền thống (phục vụ giao diện người dùng trực quan qua các Views) và **Web API** (cung cấp các API endpoints bảo mật cho tích hợp dịch vụ).
*   **JWT Cookie Authentication:** Cơ chế đăng nhập dựa trên **ASP.NET Core Identity** kết hợp với **JWT Token**. Sau khi đăng nhập thành công, JWT Token sẽ được lưu tự động dưới dạng HTTP-only Cookie (`JWTToken`). Nhờ đó, các yêu cầu từ giao diện MVC và các cuộc gọi API từ client đều được xác thực đồng bộ và bảo mật cao.

### 💾 Backend & Cơ Sở Dữ Liệu
*   **Ngôn ngữ lập trình:** C# (.NET 8)
*   **Mô hình thiết kế:** Clean Architecture (Kiến trúc sạch) tách biệt 4 lớp chính.
*   **Mẫu thiết kế áp dụng:** Repository Pattern & Unit of Work nhằm tách biệt business logic và infrastructure.
*   **Cơ sở dữ liệu:** Microsoft SQL Server.
*   **ORM:** Entity Framework Core (Code First) hỗ trợ quản lý migrations phiên bản DB.

### 🛠️ Thư Viện Hỗ Trợ
*   **Validation:** FluentValidation (Kiểm tra dữ liệu đầu vào chuẩn xác, ngăn ngừa dữ liệu lỗi vào DB).
*   **Mapping:** AutoMapper (Tự động ánh xạ dữ liệu giữa Entities và DTOs).
*   **Logging:** Serilog (Ghi log lỗi và dấu vết vận hành hệ thống).
*   **UI Libraries:** Bootstrap 5, FontAwesome 6, Chart.js, SweetAlert2, Select2.

---

## 📂 Cấu Trúc Dự Án (Clean Architecture)

Dự án được phân tách thành các layer độc lập nhằm tách biệt business logic, data access và presentation:

```text
DormitoryManagementSystem/
│
├── src/
│   ├── DormitoryManagement/                  (Presentation - Startup project / Razor Views & Controllers)
│   ├── DormitoryManagement.Application/      (Application - Services, Interfaces, DTOs, AutoMapper, FluentValidation)
│   ├── DormitoryManagement.Infrastructure/   (Infrastructure - DbContext, EF Migrations, Repositories, DbSeeder, Mail)
│   ├── DormitoryManagement.Domain/           (Domain - Entities, Enums, Core Interfaces, BaseEntity)
│   └── DormitoryManagement.Common/           (Common - Shared utilities, Helpers, Constants)
```

---

## ⚙️ Hướng Dẫn Cài Đặt (Getting Started)

### 1. Yêu cầu hệ thống
*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (hoặc SQL LocalDB)
*   Visual Studio 2022 hoặc VS Code (cài đặt C# Dev Kit extension)

### 2. Cài đặt và khởi chạy bằng lệnh CLI

**Bước 1: Clone repository về máy và di chuyển vào thư mục nguồn**
```bash
git clone <repository_url>
cd DormitoryManagementSystem
```

**Bước 2: Cấu hình tệp `appsettings.json`**
1. Vào thư mục [src/DormitoryManagement/](file:///d:/ChuyenDe/DormitoryManagementSystem/src/DormitoryManagement/).
2. Sao chép tệp `appsettings.Example.json` thành một tệp mới tên là `appsettings.json`.
3. Mở tệp `appsettings.json` vừa tạo và cập nhật chuỗi kết nối SQL Server tại mục:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=DormitoryDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
*(Lưu ý: Thay đổi `Server=.` bằng tên instance SQL Server của bạn nếu cần thiết)*

**Bước 3: Thực hiện Migration cập nhật Database**
*(Nếu chưa cài đặt công cụ EF CLI, chạy lệnh: `dotnet tool install --global dotnet-ef`)*
```bash
dotnet ef database update --project src/DormitoryManagement.Infrastructure --startup-project src/DormitoryManagement
```

**Bước 4: Khởi chạy dự án**
```bash
dotnet run --project src/DormitoryManagement
```
*Sau khi chạy thành công, truy cập ứng dụng tại địa chỉ: `http://localhost:5273`.*

---

### 🐳 3. Khởi chạy dự án bằng Docker (Tùy chọn)

Ứng dụng hỗ trợ đóng gói và chạy dưới dạng container thông qua Dockerfile đa giai đoạn (multi-stage). Bạn có thể xây dựng và khởi chạy ứng dụng bằng cách thực hiện các lệnh sau tại thư mục gốc của dự án:

```bash
# Xây dựng Docker Image cho hệ thống
docker build -t dormitory-system -f src/DormitoryManagement/Dockerfile .

# Khởi chạy container từ image vừa build
docker run -d -p 8080:10000 --name dormitory-app dormitory-system
```
*Lưu ý: Khi chạy qua Docker container, ứng dụng sẽ chạy trên cổng `10000` (được ánh xạ ra cổng `8080` ở máy chủ).*

> [!TIP]
> **Tự động khởi tạo dữ liệu mẫu (Auto-Seeding):**
> Khi ứng dụng khởi chạy lần đầu trong môi trường **Development**, hệ thống sẽ tự động gọi `DbSeeder` để tạo lập toàn bộ dữ liệu mẫu (Dãy nhà, Phòng ở, Giường ngủ, Sinh viên, Hợp đồng thuê, Hóa đơn dịch vụ, Sự cố sửa chữa...). Bạn có thể đăng nhập ngay để trải nghiệm bằng các tài khoản mẫu dưới đây.

---

## 🔑 Tài Khoản Thử Nghiệm Mặc Định (Default Accounts)

Hệ thống được cung cấp sẵn một tập hợp tài khoản mẫu phục vụ việc kiểm thử các luồng chức năng và phân quyền (RBAC):

| Vai trò (Role) | Username (Tên đăng nhập) | Password (Mật khẩu) | Mô tả / Chức năng kiểm thử |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin` | `Admin@123` | Quản trị viên tối cao: Quản lý người dùng, phân quyền, cấu hình hệ thống. |
| **Management Staff** | `manager1`<br>`manager2` | `Manager@123` | Nhân viên quản lý: Phê duyệt hợp đồng, ghi điện nước, thu phí, quản lý phòng/giường. |
| **Technical Staff** | `tech1`<br>`tech2`<br>`tech3` | `Tech@123` | Nhân viên kỹ thuật: Tiếp nhận và xử lý các yêu cầu sửa chữa, bảo trì thiết bị. |
| **Student** | Mã sinh viên mẫu:<br>`2221010001`<br>`2221010002`<br>... | `Student@123` | Sinh viên nội trú: Gửi yêu cầu sửa chữa, xem hóa đơn dịch vụ, danh sách bạn cùng phòng. |

---

## 🔄 Quy Trình Làm Việc Nhóm (Git Workflow)

Chúng tôi áp dụng mô hình Agile (Scrum) và tuân thủ các nguyên tắc làm việc với Git:
1.  Cập nhật code mới nhất từ nhánh chính: `git checkout main` $\to$ `git pull`
2.  Tạo nhánh tính năng mới: `git checkout -b feature/<tên-chức-năng>`
3.  Commit code với thông điệp rõ ràng theo chuẩn **Conventional Commits**.
4.  Push lên remote repository và tạo Pull Request (PR) để review trước khi merge vào nhánh `main`.

---

## 📚 Thư Mục Tài Liệu Dự Án (Documentation)

Hệ thống tài liệu hướng dẫn đặc tả kiến trúc, luồng xử lý và dữ liệu của dự án nằm trong thư mục `Docs/`:
*   📄 [Phân Tích và Thiết Kế Hệ Thống](./src/DormitoryManagement/Docs/PhanTichVaThietKeHeThong.md) - Tài liệu SRS chi tiết về kiến trúc dự án và thiết kế cơ sở dữ liệu.
*   📄 [Chi Tiết Phân Quyền & Thuật Toán](./src/DormitoryManagement/Docs/phanQuyen.md) - Mô tả thuật toán xếp phòng, tính tiền điện nước, tính công nợ và cơ chế Xóa mềm (Soft Delete).
*   📄 [Yêu Cầu Chức Năng & Phi Chức Năng](./src/DormitoryManagement/Docs/FunctionAndNonFunction.md) - Đặc tả yêu cầu phần mềm chi tiết.
*   📄 [Hướng Dẫn Đóng Góp Mã Nguồn](./src/DormitoryManagement/Docs/HowToContribute.md) - Quy tắc Git branch, commit message và Coding Convention của nhóm.
*   📄 [Hướng Dẫn Sử Dụng VS Code để Migration](./src/DormitoryManagement/Docs/EFCoreMigrationGuide/UsingVSCode.md) - Các bước cấu hình và chạy Entity Framework Core Migration trên Visual Studio Code.

---

## 👥 Đội Ngũ Phát Triển (Nhóm 02)

*   **Đỗ Thành Luân** (Trưởng nhóm - 2221050046)
*   **Lê Thị Cẩm Tú** (2321050008)
*   **Đỗ Quang Huy** (2221050047)
*   **Vũ Thị Kim Oanh** (2221050566)

**Giảng viên hướng dẫn:** Thầy Ngô Ngọc Anh  
*Trường Đại học Mỏ - Địa chất | Hà Nội, Năm 2026*
