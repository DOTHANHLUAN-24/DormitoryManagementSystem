# 🧠 PHÂN TÍCH & THIẾT KẾ HỆ THỐNG

## Hệ thống quản lý ký túc xá

---

## 🎯 1. Mục tiêu hệ thống

Hệ thống được xây dựng nhằm:

* Quản lý toàn bộ hoạt động ký túc xá một cách tập trung
* Giảm thiểu thao tác thủ công, sai sót dữ liệu
* Hỗ trợ quản lý hợp đồng, tài chính, và vận hành hiệu quả
* Đảm bảo bảo mật, phân quyền rõ ràng giữa các vai trò

---

## 👥 2. Stakeholders (Các bên liên quan)

* 👨‍🎓 Sinh viên: Đăng ký phòng, xem hóa đơn, gửi yêu cầu
* 🧑‍💼 Quản lý: Quản lý phòng, hợp đồng, tài chính
* 👮 Bảo vệ: Kiểm soát ra vào, khách, phương tiện
* 💰 Kế toán: Xử lý hóa đơn, thanh toán
* 🧑‍💻 Admin hệ thống: Quản lý người dùng, phân quyền

---

## 🧩 3. Functional Requirements (Yêu cầu chức năng)

### 👤 Người dùng

* Đăng ký / đăng nhập (Identity + JWT)
* Phân quyền theo Role
* Cập nhật thông tin cá nhân

### 🏢 Hạ tầng

* Quản lý Block, Room, Bed
* Phân loại RoomType
* Theo dõi tài sản phòng (Asset)

### 📜 Hợp đồng

* Tạo / gia hạn / kết thúc hợp đồng
* Gán User vào Bed
* Theo dõi trạng thái hợp đồng

### ⚡ Điện nước

* Nhập chỉ số UtilityUsage
* Tính toán chi phí theo Utility

### 💰 Tài chính

* Tạo Invoice tự động theo kỳ
* Gộp nhiều khoản: tiền phòng + điện nước + phụ phí
* Thanh toán nhiều lần (Payment)

### 🛠️ Vận hành

* Gửi yêu cầu sửa chữa (MaintenanceRequest)
* Ghi nhận vi phạm (Violation)
* Quản lý khách (VisitorLog)
* Quản lý phương tiện (Vehicle)

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

## 12. UML
### Usecase
#### Usecase tổng quát
![UC tổng quát](../Source//image/uml/Use%20Case%20Diagram1.jpg)

---

## 📌 12. Tổng kết

Hệ thống được thiết kế theo hướng:

* Rõ ràng trách nhiệm từng layer
* Dễ mở rộng và bảo trì
* Đảm bảo hiệu năng và bảo mật

👉 Phù hợp cho hệ thống thực tế và có thể scale lớn trong tương lai.
