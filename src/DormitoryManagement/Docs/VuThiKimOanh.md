# PHÂN TÍCH HỆ THỐNG QUẢN LÝ KÝ TÚC XÁ

## Thông tin sinh viên
- Họ và tên: Vũ Thị Kim Oanh
- Mssv: 2221050566
- Lớp: DCCTCT67_05A

## Nhiệm vụ được phân công
- Soạn thảo Chương Tổng quan dự án và Cơ sở lý thuyết (Các khái niệm cơ bản liên quan đến đề tài, lý thuyết nền tảng cho việc phát triển hệ thống, công nghệ và ngôn ngữ lập trình sử dụng, các thuật toán và kỹ thuật liên quan, các framework và thư viện được sử dụng, các tiêu chuẩn và quy trình phát triển phần mềm áp dụng (nếu có)) 

# CHƯƠNG 1: TỔNG QUAN DỰ ÁN

## 1.1. Giới thiệu chung
Trong bối cảnh số lượng sinh viên ở KTX ngày càng tăng cao, việc quản lý ký túc xá theo các phương pháp truyền thống như sổ sách hoặc file Excel rời rạc đã bộc lộ nhiều hạn chế như: 
- Dễ xảy ra sai sót dữ liệu
- Khó khăn trong việc tra cứu thông tin
- Không đồng bộ giữa các bộ phận
- Chậm trễ trong việc lập báo cáo
Dự án Hệ thống Quản lý KTX được xây dựng nhằm số hóa toàn bộ quy trình quản lý, giúp nâng cao hiệu quả và giảm thiểu sai sót

---

## 1.2. Mô tả hệ thống
Hệ thống được thiết kế để quản lý KTX bao gồm:
- 3 tòa: 2 tòa cho sinh viên trong nước và 1 tòa cho sinh viên nước ngoài
Hệ thống hỗ trợ quản lý tập trung các hoạt động:
- Quản lý phòng và giường
- Quản lý sinh viên nội trú
- Quản lý hợp đồng thuê
- Quản lý tài chính (hóa đơn, điện, nước, dịch vụ)
- Quản lý tài sản và thiết bị
- Theo dõi vi phạm nội quy
- Quản lý khách ra vào

---

## 1.3. Mục tiêu hệ thống
Hệ thống được xây dựng nhằm:
- Tự động hóa quy trình quản lý ký túc xá
- Giảm thiểu thao tác thủ công và sai sót dữ liệu
- Tăng tốc độ xử lý và tra cứu thông tin
- Hỗ trợ quản lý tài chính và hợp đồng hiệu quả
- Đảm bảo bảo mật và phân quyền rõ ràng
- Cung cấp báo cáo nhanh chóng, chính xác

---

## 1.4. Phạm vi hệ thống
Hệ thống phục vụ các đối tượng:
- Sinh viên nội trú
- Ban quản lý ký túc xá
- Nhân viên bảo vệ
- Nhân viên kế toán
- Quản trị hệ thống

---

## 1.5. Các chức năng chính
- Đăng ký đổi, trả phòng
- Quản lý hồ sơ sinh viên
- Quản lý hợp đồng thuê phòng
- Quản lý thu chi và hóa đơn
- Gửi thông báo hệ thống
- Quản lý tài sản và bảo trì
- Báo cáo thống kê
- Quản lý ra vào ký túc xá

---

# CHƯƠNG 2: CƠ SỞ LÝ THUYẾT

## 2.1. Các khái niệm cơ bản

### 2.1.1. Hệ thống thông tin (Information System)
Hệ thống thông tin là tập hợp các thành phần bao gồm con người, phần mềm, phần cứng và dữ liệu, được tổ chức để thu thập, xử lý và cung cấp thông tin hỗ trợ ra quyết định

---

### 2.1.2. Quản lý KTX
Là quá trình quản lý toàn bộ hoạt động liên quan đến sinh viên nội trú, bao gồm:
- Phân bổ phòng
- Quản lý hợp đồng
- Thu phí
- Kiểm soát nội quy
- Bảo trì cơ sở vật chất 

---

### 2.1.3. RBAC (Role-Based Access Control)
Là mô hình phân quyền dựa trên vai trò, trong đó:
- Người dùng được gán vai trò
- Mỗi vai trò có tập quyền riêng
- Giúp kiểm soát truy cập hiệu quả và bảo mật

---

## 2.2. Lý thuyết nền tảng phát triển hệ thống

### 2.2.1. Kiến trúc 3-Layer
Hệ thống được xây dựng theo mô hình 3 lớp:
- Controller Layer: Nhận và xử lý request từ client
- Service Layer: Xử lý logic nghiệp vụ
- Repository Layer: Tương tác với cơ sở dữ liệu
Ưu điểm:
- Dễ bảo trì
- Tách biệt rõ trách nhiệm
- Dễ mở rộng

---

### 2.2.2. Repository Pattern
Là mẫu thiết kế giúp:
- Tách logic truy vấn dữ liệu khỏi business logic
- Dễ dàng thay đổi database
- Tăng khả năng test

---

### 2.2.3. Dependency Injection
Kỹ thuật giúp: 
- Giảm sự phụ thuộc giữa các module
- Tăng khả năng mở rộng
- Dễ dàng kiểm thử

---

## 2.3. Công nghệ và ngôn ngữ sử dụng

### 2.3.1. Ngôn ngữ lập trình
- C# 

---

### 2.3.2. Framework
- ASP.NET Core Web API

---

### 2.3.3. Cơ sở dữ liệu
- SQL Server
- Entity Framework Core (Code First)

---

### 2.3.4. Công cụ hỗ trợ
- Visual Studio
- Postman (test API)

---

## 2.4. Các kỹ thuật và thuật toán áp dụng

### 2.4.1. Chuẩn hóa dữ liệu (3NF)
- Loại bỏ dư thừa dữ liệu
- Đảm bảo tính nhất quán
- Tối ưu lưu trữ

---

### 2.4.2. Caching
- Giảm tải database
- Tăng tốc độ phản hồi
- Sử dụng Memory Cache hoặc Redis

---

### 2.4.3. Validation dữ liệu
- Sử dụng FluentValidation
- Đảm bảo dữ liệu đầu vào hợp lệ

---

### 2.4.4. Xử lý lỗi tập trung
- Global Exception Middleware
- Trả về response chuẩn

---

### 2.4.5. Framework và thư viện sử dụng
- ASP.NET Identity (xác thực người dùng)
- JWT Authentication (bảo mật API)
- FluentValidation (validate dữ liệu)
- AutoMapper (mapping DTO)
- Serilog (logging hệ thống)
 
--- 

## 2.6. Tiêu chuẩn và quy trình phát triển phần mềm

### 2.6.1. Mô hình phát triển
Agile (Scrum)

Ưu điểm:
- Linh hoạt thay đổi yêu cầu
- Phát triển theo từng sprint
- Dễ kiểm soát tiến độ

---

### 2.6.2. Quy trình phát triển
- Phân tích yêu cầu
- Thiết kế hệ thống
- Xây dựng
- Kiểm thử
- Triển khai
- Bảo trì

---

### 2.6.3. Coding Convention
- Đặt tên biến rõ ràng
- Tách lớp theo trách nhiệm
- Comment hợp lý
- Tuân thủ Clean Code

---

## 2.7. Tổng kết
Chương này đã trình bày:
- Các khái niệm nền tảng
- Kiến trúc và mô hình phát triển
- Công nghệ và kỹ thuật sử dụng
Đây là cơ sở quan trọng để triển khai hệ thống quản lý ký túc xá một cách hiệu quả, đảm bảo tính mở rộng, bảo mật và ổn định trong thực tế.