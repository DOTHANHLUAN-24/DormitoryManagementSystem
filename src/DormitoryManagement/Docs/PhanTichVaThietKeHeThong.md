# 🧠 PHÂN TÍCH & THIẾT KẾ HỆ THỐNG

## Hệ thống quản lý ký túc xá

---

## 🎯 1. Mục tiêu hệ thống
Mô tả bối cảnh: quy mô 3 tòa ktx (2 tòa dành cho sinh viên, 1 tòa dành cho sinh viên nước ngoài)
Hiện trạng: Quản lý bằng sổ sách, Excel, hoặc phần mềm rời rạc → dễ sai sót, chậm, khó tra cứu, xung đột phòng, chậm báo cáo.
Hệ thống được xây dựng nhằm:

* Quản lý toàn bộ hoạt động ký túc xá một cách tập trung
* Giảm thiểu thao tác thủ công, sai sót dữ liệu
* Hỗ trợ quản lý hợp đồng, tài chính, và vận hành hiệu quả
* Đảm bảo bảo mật, phân quyền rõ ràng giữa các vai trò

Các tác vụ chính của hệ thống:
* Đăng ký phòng, đổi phòng, trả phòng.
* Quản lý hồ sơ sinh viên nội trú.
* Quản lý hợp đồng thuê phòng.
* Quản lý thu – chi phí điện, nước, dịch vụ.
* Quản lý thiết bị, tài sản phòng.
* Theo dõi vi phạm nội quy.
* Báo cáo tình trạng phòng, công nợ, doanh thu.
* Quản lý khách đến thăm, trực ca ktx.

---

## 👥 2. Stakeholders (Các bên liên quan)

* 👨‍🎓 Sinh viên: Đăng ký phòng, tra cứu thông tin phòng, bạn cùng phòng, xem hóa đơn, thanh toán phí/công nợ, gửi yêu cầu sửa chữa thiết bị.
* 🧑‍💼 Quản lý: Quản lý phòng, hợp đồng, tài chính, duyệt đăng ký, làm/xem báo cáo tổng hợp.
* 👮 Bảo vệ (sinh viên tình nguyện): Kiểm soát ra vào, khách, phương tiện, báo cáo vi phạm.
* 💰 Kế toán: Xử lý hóa đơn, thanh toán.
* 🧑‍💻 Admin hệ thống: Quản lý người dùng, phân quyền, cài đặt cấu hình hệ thống, sao lưu dữ liệu, phục hồi sự cố.

---

## 🧩 3. Functional Requirements (Yêu cầu chức năng)

### 👤 Người dùng
* Chức năng quản lí người dùng:
  - 1. Phân loại vai trò người dùng
Các vai trò chính:
Sinh viên: Người ở ký túc xá

Quản lý ký túc xá: Giám sát tổng thể

Nhân viên bảo vệ: Kiểm soát ra vào

Nhân viên vệ sinh/bảo trì: Xử lý công việc

Admin: Quản trị hệ thống
  - 2. Chức năng quản lý tài khoản
Đăng ký & Xác thực:
Đăng ký tài khoản: Sinh viên tự đăng ký bằng mã số sinh viên, email, số điện thoại

Xác thực email/SĐT: Gửi mã OTP để xác nhận

Đăng nhập: Bằng tài khoản/mật khẩu hoặc Google/Microsoft

Đăng xuất: Kết thúc phiên làm việc

Quên mật khẩu: Gửi link reset mật khẩu qua email

Đổi mật khẩu: Cho phép thay đổi mật khẩu định kỳ
 - 3. Chức năng phân quyền 
Quản lý vai trò:
Tạo vai trò mới: Admin tạo role tùy chỉnh (vd: Trưởng khu, Giám thị...)

Phân quyền chi tiết: Gán quyền (xem, thêm, sửa, xóa, duyệt) cho từng chức năng

Sửa/xóa vai trò: Điều chỉnh hoặc xóa vai trò không cần thiết
  - 4. Chức năng quản lý sinh viên (người ở)
Quản lý hồ sơ sinh viên:
Thêm sinh viên mới: Nhập thông tin thủ công hoặc import file Excel

Sửa thông tin sinh viên: Cập nhật khi sinh viên thay đổi

Xóa/vô hiệu hóa: Khi sinh viên chuyển đi hoặc thôi học

Tìm kiếm & lọc: Theo mã số, tên, phòng, khóa, khoa...

Xuất danh sách: Export ra Excel/PDF
  - 5. Chức năng cho nhân viên
Quản lý nhân viên:
Thêm nhân viên: Tạo tài khoản cho bảo vệ, lao công, kỹ thuật...

Phân ca làm việc: Gán lịch trực cho bảo vệ, nhân viên vệ sinh

Theo dõi hiệu suất: Đánh giá hoàn thành công việc

  - 6. Chức năng thông báo & liên lạc
Quản lý thông báo:
Gửi thông báo cá nhân: Đến từng sinh viên (vd: nhắc đóng phí)

Gửi thông báo nhóm: Theo phòng, theo tòa, theo khoa

Gửi thông báo toàn hệ thống: Thông báo khẩn, lịch cắt điện nước

Xem lịch sử thông báo: Đã gửi, đã đọc, chưa đọc
  - 7. Chức năng dành riêng cho Admin
Quản trị hệ thống:
Khóa/mở khóa tài khoản: Xử lý vi phạm hoặc bảo mật

Đặt lại mật khẩu: Cho người dùng quên mật khẩu

Xóa tài khoản vĩnh viễn: Dọn dẹp dữ liệu cũ

Backup/Restore dữ liệu người dùng: Sao lưu định kỳ
  - 8. Chức năng thống kê & báo cáo người dùng
Báo cáo:
Thống kê số lượng: Tổng số sinh viên, nhân viên, theo vai trò

Tỷ lệ sử dụng hệ thống: Số người đăng nhập hàng ngày/tuần/tháng

Báo cáo sinh viên mới/nghỉ: Theo tháng, theo học kỳ

Xuất báo cáo Excel/PDF: Phục vụ báo cáo định kỳ

---

## 🚫 4. Non-Functional Requirements (Phi chức năng)

* 🔐 Bảo mật: JWT, RBAC, hạn chế truy cập trái phép
* ⚡ Hiệu năng: caching, tối ưu query
* 📈 Khả năng mở rộng: kiến trúc tách lớp rõ ràng
* 🧾 Audit: lưu log hành động người dùng
* 🛡️ Độ tin cậy: xử lý exception toàn cục

---

## 🏗️ 5. Kiến trúc hệ thống

### 📌 Mô hình tổng thể: 3-Layer Architecture

* Controller Layer: nhận request từ client
* Service Layer: xử lý business logic
* Repository Layer: truy vấn database

### 📌 Áp dụng thêm:

* Repository Pattern
* Dependency Injection
* Middleware pipeline

---

## 🔄 6. Luồng xử lý chính (High-level Flow)

### 📥 Tạo hợp đồng

1. User gửi request tạo hợp đồng
2. Controller nhận request
3. Service validate dữ liệu (FluentValidation)
4. Repository lưu Contract + liên kết Bed
5. Trả kết quả về client

### 💸 Tạo hóa đơn

1. Hệ thống lấy Contract đang active
2. Tính tiền phòng + điện nước
3. Gộp vào Invoice
4. Lưu database

### 🛠️ Gửi yêu cầu sửa chữa

1. User tạo MaintenanceRequest
2. Lưu vào DB
3. Quản lý xử lý và cập nhật trạng thái

---

## 🗃️ 7. Thiết kế dữ liệu (Database Design)

* Sử dụng Entity Framework Core
* Áp dụng Code First
* Chuẩn hóa dữ liệu (3NF)

### 📌 Nguyên tắc thiết kế

* Không xóa cứng (Soft Delete)
* Dùng khóa ngoại rõ ràng
* Tách bảng trung gian nếu cần
* Tối ưu index cho truy vấn nhiều

---

## 🔐 8. Bảo mật & Phân quyền

* Sử dụng ASP.NET Identity
* JWT Authentication
* Role-Based Access Control (RBAC)
* Có thể mở rộng Permission-based

---

## 🧪 9. Validation & Error Handling

* FluentValidation cho input
* Global Exception Middleware
* Trả về chuẩn response (status + message)

---

## 📊 10. Logging & Monitoring

* Sử dụng Serilog
* Ghi log:

  * Request/Response
  * Error
  * Audit log (hành động user)

---

## 🚀 11. Khả năng mở rộng

* Tách DTO + AutoMapper
* Có thể nâng cấp sang Microservices
* Tích hợp cache (Memory/Redis)
* Có thể thêm Mobile App (Flutter)

---

## 📌 12. Tổng kết

Hệ thống được thiết kế theo hướng:

* Rõ ràng trách nhiệm từng layer
* Dễ mở rộng và bảo trì
* Đảm bảo hiệu năng và bảo mật

👉 Phù hợp cho hệ thống thực tế và có thể scale lớn trong tương lai.
