# Framework và Thư viện

## APS.NET Core
ASP.NET Core được sử dụng làm nền tảng chính để xây dựng hệ thống backend cho Hệ thống quản lý ký túc xá.
Vai trò trong dự án:
- Xây dựng RESTful API phục vụ các chức năng:
  - đăng ký, đăng nhập người dùng 
  - quản lý sinh viên nội trú
  - quản lý hợp đồng và hóa đơn
  - xử lý yêu cầu sửa chữa
- Xử lý request từ client (Web/Mobile) 
- Triển khai Middleware pipeline: 
  - Authentication (JWT)
  - Logging
  - Exception handling.
- Tổ chức hệ thống theo mô hình 3-Layer (Controller - Service - Repository) 
Áp dụng thực tế trong hệ thống: 
- Controller nhận request từ:
  - sinh viên (đăng ký phòng, xem hóa đơn)
  - quản lý (duyệt hợp đồng, báo cáo). 
- Service xử lý nghiệp vụ:
  - kiểm tra phòng trống
  - kiểm tra hợp đồng còn hiệu lực
- Response trả về dạng JSON thống nhất.

----

## Entity Framework Core
Entity Framework Core (EF Core) được sử dụng để tương tác với cơ sở dữ liệu theo hướng Code First.
Vai trò trong dự án:
- Mapping các entity:
  - Student
  - Room
  - Bed
  - Contract
  - Invoice
  - MaintenanceRequest
- Thực hiện CRUD:
  - Thêm sinh viên
  - Cập nhật hợp đồng
  - Xóa mềm dữ liệu
Áp dụng thực tế:
- Khi sinh viên đăng ký phòng:
  - EF Core kiểm tra phòng còn trống
  - Tạo bản ghi Contract + liên kết Bed
- Khi tạo hóa đơn:
  - Query Contract active
  - Sinh Invoice tự động
- Ưu điểm trong hệ thống:
  - Giảm lỗi SQL thủ công
  - Tăng tốc phát triển
  - Dễ bảo trì khi thay đổi schema

----

## Bootstrap
Bootstrap được sử dụng để xây dựng giao diện web quản lý ký túc xá.
Vai trò trong dự án: Xây dựng giao diện web quản trị và Tạo UI nhanh responsive
- Xây dựng dashboard cho:
  - Quản lý ký túc xá
  - Admin hệ thống
- Tạo form:
  - Đăng ký phòng
  - Nhập thông tin sinh viên
- Hiển thị bảng dữ liệu:
  - Danh sách phòng
  - Danh sách sinh viên
Áp dụng thực tế: 
- Responsive cho nhiều thiết bị
- Tối ưu UX cho người dùng không rành công nghệ (quản lý, bảo vệ) 

---

# Quy trình phát triển

## Agile
Hệ thống được phát triển theo phương pháp Agile (Scrum) là chia dự án thành nhiều sprint nhỏ
Áp dụng trong dự án
Chia thành các sprint theo từng giai đoạn phát triển: 
- Sprint 1: Quản lý người dùng + đăng nhập
- Sprint 2: Quản lý phòng + sinh viên
- Sprint 3: Hợp đồng + hóa đơn
- Sprint 4: Báo cáo + thông báo
Lợi ích:
- Dễ điều chỉnh theo yêu cầu thực tế
- Có thể demo từng phần hệ thống
- Giảm rủi ro khi phát triển

----

## Solid
Áp dụng trong dự án
- Single Responsibility
  - Service chỉ xử lý logic
  - Repository chỉ truy vấn DB
- Open/Closed
  - Có thể thêm loại hóa đơn mới mà không sửa code cũ
- Dependency Inversion
  - Inject repository vào service
Lợi ích:
- Code dễ mở rộng, dễ đọc 
- Dễ test
- Dễ bảo trì

---

## DRY
Áp dụng trong dự án
- Tái sử dụng: 
  - Validation rules
  - Response format
- Dùng chung service cho nhiều module
Ví dụ: 
- Logic tính tiền điện nước chỉ viết 1 lần, dùng lại khi tạo invoice, tránh lặp logic 

---

## GIT
Áp dụng trong dự án
- Quản lý source code
- Làm việc nhóm 
- Phân nhánh:
  - feature/auth
  - feature/contract
- Merge code theo từng chức năng
Lợi ích: 
- Làm việc nhóm hiệu quả
- Dễ rollback khi lỗi

---

# KIỂM THỬ 

## Black-box testing
Áp dụng trong hệ thống 
Kiểm tra các chức năng dựa trên input/ output: 
- Đăng ký phòng:
  - Input hợp lệ thì thành công
  - Phòng đầy thì báo lỗi
- Tạo hóa đơn:
  - Kiểm tra tổng tiền đúng 
Mục tiêu: 
- Đảm bảo hệ thống đúng theo yêu cầu người dùng 

---

## White-box testing
Áp dụng trong hệ thống 
- Kiểm tra logic:
  - Tính tiền điện, nước
  - Kiểm tra hợp đồng hết hạn
- Test từng nhánh điều kiện 

---

# KỸ THUẬT KIỂM THỬ

## UNIT TESTING
Kiểm thử từng module nhỏ 
Áp dụng 
- Test Service:
  - ContractService
  - InvoiceService
Mục tiêu:
- Đảm bảo từng hàm hoạt động đúng logic 

--- 

## Integration Testing
Áp dụng 
- Test luồng: 
  - Đăng ký phòng → tạo contract → tạo invoice 
- Kiểm tra kết nối DB

--- 

## System Testing
Áp dụng
- Test toàn hệ thống:
  - Sinh viên đăng ký → quản lý duyệt → kế toán tạo hóa đơn
- Kiểm tra end-to-end

---

# CÔNG CỤ KIỂM THỬ

## xUnit
Vai trò trong dự án
- Viết Unit Test cho:
  -Service
  -Business logic
- Áp dụng:
  - Test business logic trong Service

---

## Postman
Vai trò
- Test API:
  - Đăng nhập
  -Tạo hợp đồng
  -Lấy danh sách phòng
Kiểm tra response JSON

---

## Swagger
Vai trò
  Hiển thị toàn bộ API dưới dạng UI
Áp dụng: 
- Test trực tiếp trên trình duyệt
- Hỗ trợ debug nhanh

--- 

# TỔNG KẾT
- Framework và công cụ được lựa chọn phù hợp với:
  - Bài toán quản lý ký túc xá thực tế
  - Dữ liệu lớn (sinh viên, phòng, hợp đồng)

- Hệ thống đảm bảo:
  - Dễ mở rộng
  - Dễ bảo trì
  - Hiệu năng tốt
  - Bảo mật cao

 Có thể triển khai thực tế và mở rộng trong tương lai (mobile app, microservices).
 