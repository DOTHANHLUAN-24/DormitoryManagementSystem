# CHƯƠNG 1. CƠ SỞ LÝ THUYẾT

## 1.1. Khái niệm cơ bản liên quan đến đề tài

### 1.1.1. Quản lý ký túc xá

Trình bày khái niệm quản lý ký túc xá, vai trò trong việc quản lý sinh viên nội trú, phân bổ phòng, theo dõi cư trú.

### 1.1.2. Ký túc xá sinh viên

Mô tả đặc điểm của ký túc xá: phòng ở, khu vực sinh hoạt, đối tượng sử dụng.

### 1.1.3. Hệ thống quản lý ký túc xá

Giới thiệu hệ thống phần mềm hỗ trợ quản lý toàn bộ hoạt động trong ký túc xá.

### 1.1.4. Đăng ký phòng, phân phòng và quản lý cư trú

Trình bày quy trình đăng ký, xét duyệt, phân phòng và theo dõi sinh viên.

## 1.2. Lý thuyết nền tảng cho việc phát triển hệ thống

### 1.2.1. Phân tích và thiết kế hệ thống thông tin

Trình bày các bước phân tích yêu cầu và thiết kế hệ thống.

### 1.2.2. Mô hình hóa hệ thống bằng ngôn ngữ UML

Giới thiệu UML và các loại sơ đồ sử dụng(Use Case, Class, Sequence, Activity).

## 1.3. Công nghệ và ngôn ngữ lập trình sử dụng

### 1.3.1. Backend: ASP.NET Core 8 Web API

Mô tả lý do lựa chọn và cách sử dụng.

### 1.3.2. Frontend: React

Trình bày cách xây dựng giao diện người dùng.

### 1.3.3. Hệ quản trị cơ sở dữ liệu

Ví dụ: SQL Server/MySQL.

### 1.3.4. Công nghệ kết nối và công cụ hỗ trợ

REST API, JSON, Postman, Git,...

## 1.4. Các thuật toán và kỹ thuật liên quan

### 1.4.1. Thuật toán phân phòng và quản lý sức chứa

### 1.4.2. Kỹ thuật xác thực và kiểm soát dữ liệu

## 1.5. Các framework và thư viện được sử dụng

Liệt kê: Entity Framework, JWT, Bootstrap,...

## 1.6. Các tiêu chuẩn và quy trình phát triển phần mềm áp dụng

### 1.6.1. Quy trình phát triển phần mềm

Agile/Scrum hoặc Waterfall.

### 1.6.2. Tiêu chuẩn lập trình

Coding convention, clean code.

## 1.7. Các phương pháp kiểm thử

### 1.7.1. Kiểm thử hộp đen

### 1.7.2. Kiểm thử hộp trắng

### 1.7.3. Kiểm thử tích hợp

### 1.7.4. So sánh các phương pháp

## 1.8. Các kỹ thuật kiểm thử

Boundary value, equivalence partitioning,...

## 1.9. Các công cụ và framework kiểm thử sử dụng

JUnit, Selenium, Postman,...

## 1.10. Kết luận chương

---

# CHƯƠNG 2. KHẢO SÁT HỆ THỐNG

## 2.1. Mục tiêu khảo sát hệ thống

Phân tích thực trạng quản lý ký túc xá hiện tại.

## 2.2. Đối tượng và phạm vi khảo sát

### 2.2.1. Đối tượng khảo sát

Sinh viên, quản lý ký túc xá, nhân viên.

### 2.2.2. Phạm vi khảo sát

Trong phạm vi một ký túc xá hoặc trường đại học.

## 2.3. Phương pháp khảo sát

### 2.3.1. Quan sát thực tế

### 2.3.2. Phân tích tài liệu

## 2.4. Hiện trạng quản lý ký túc xá

Mô tả cách quản lý hiện tại (thủ công/excel/...)

## 2.5. Đánh giá và vấn đề tồn tại

Thiếu đồng bộ, dễ sai sót, khó quản lý số lượng lớn.

## 2.6. Nhu cầu hệ thống mới

Tự động hóa, chính xác, dễ sử dụng.

## 2.7. Kết luận chương

---

# CHƯƠNG 3. PHÂN TÍCH VÀ THIẾT KẾ HỆ THỐNG

## 3.1. Phân tích yêu cầu

### 3.1.1. Mục tiêu hệ thống

Quản lý sinh viên, phòng ở, đăng ký và thanh toán.

### 3.1.2. Đối tượng sử dụng

Sinh viên, quản lý, nhân viên.

### 3.1.3. Phạm vi hệ thống

Quản lý nội trú.

### 3.1.4. Mô tả nghiệp vụ

Luồng đăng ký phòng, duyệt, phân phòng, thanh toán.

## 3.2. Yêu cầu hệ thống

### 3.2.1. Yêu cầu chức năng

Đăng nhập, đăng ký phòng, quản lý phòng, hóa đơn,...

### 3.2.2. Yêu cầu phi chức năng

Bảo mật, hiệu năng, khả năng mở rộng.

## 3.3. Mô hình hóa hệ thống

## 3.4. Use Case Diagram

Bao gồm các UC chính của hệ thống.

## 3.5. Activity Diagram

Mô tả luồng xử lý.

## 3.6. Sequence Diagram

Tương tác giữa các thành phần.

## 3.7. Class Diagram

Thiết kế lớp.

## 3.8. Thiết kế cơ sở dữ liệu

### 3.8.1. Mục tiêu

### 3.8.2. Mô hình dữ liệu

### 3.8.3. Các bảng chính

Students, Rooms, Registrations, Payments,...

### 3.8.4. Quan hệ bảng

## 3.9. Thiết kế giao diện

Trang đăng nhập, dashboard, quản lý phòng,...

---

# CHƯƠNG 4. KIỂM THỬ HỆ THỐNG

## 4.1. Mục tiêu kiểm thử

## 4.2. Test Scenarios

## 4.3. Test Cases

## 4.4. Triển khai kiểm thử

## 4.5. Automation Testing

---

# CHƯƠNG 5. KẾT QUẢ TRIỂN KHAI

---

# CHƯƠNG 6. PHÂN TÍCH KẾT QUẢ
