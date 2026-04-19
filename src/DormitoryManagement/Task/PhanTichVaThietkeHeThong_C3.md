# CHƯƠNG 3. PHÂN TÍCH VÀ THIẾT KẾ HỆ THỐNG

## 3.1. Phân tích yêu cầu của hệ thống

### 3.1.1. Mục tiêu hệ thống

Hệ thống quản lý ký túc xá sinh viên được xây dựng nhằm tối ưu hóa việc quản lý phòng ở, sinh viên nội trú và các hoạt động vận hành trong ký túc xá. Hệ thống hỗ trợ theo dõi tình trạng phòng, quản lý đăng ký ở, và cung cấp nền tảng trực tuyến giúp sinh viên dễ dàng tra cứu và đăng ký chỗ ở.

Các mục tiêu chính của hệ thống bao gồm:

* Quản lý tập trung thông tin sinh viên và phòng ở.
* Tự động hóa quy trình đăng ký, duyệt và phân phòng.
* Theo dõi tình trạng phòng (còn trống, đã đầy, bảo trì).
* Hỗ trợ sinh viên đăng ký ở ký túc xá trực tuyến.
* Cung cấp báo cáo thống kê phục vụ quản lý.
* Giảm thiểu sai sót và nâng cao hiệu quả vận hành.

---

### 3.1.2. Đối tượng sử dụng hệ thống

Hệ thống quản lý ký túc xá sinh viên được thiết kế phục vụ cho nhiều nhóm người dùng khác nhau, mỗi nhóm có vai trò và quyền hạn riêng trong quá trình vận hành hệ thống. Cụ thể gồm các đối tượng sau:

---

### 1. Quản trị viên (Admin)

Quản trị viên là người có quyền cao nhất trong hệ thống, chịu trách nhiệm cấu hình và giám sát toàn bộ hoạt động của ký túc xá.

**Chức năng chính:**

* Quản lý danh mục phòng và khu/tòa nhà
* Quản lý tài khoản người dùng và phân quyền
* Theo dõi, giám sát hoạt động hệ thống
* Xem và xuất báo cáo thống kê
* Quản lý thông báo và kỷ luật sinh viên

---

### 2. Nhân viên quản lý ký túc xá (Staff)

Nhân viên là người trực tiếp vận hành hệ thống, xử lý các nghiệp vụ liên quan đến sinh viên và phòng ở.

**Chức năng chính:**

* Tiếp nhận và xử lý đăng ký ở ký túc xá
* Duyệt đơn đăng ký và phân phòng
* Quản lý thông tin sinh viên nội trú
* Xử lý chuyển phòng, trả phòng
* Cập nhật chỉ số điện, nước và quản lý hóa đơn
* Tiếp nhận và xử lý yêu cầu báo hỏng thiết bị
* Ghi nhận vi phạm nội quy

---

### 3. Sinh viên (Student)

Sinh viên là người sử dụng hệ thống để đăng ký và theo dõi thông tin liên quan đến việc ở ký túc xá.

**Chức năng chính:**

* Tra cứu thông tin phòng ký túc xá
* Đăng ký ở ký túc xá
* Theo dõi trạng thái đăng ký
* Xem thông tin phòng và tình trạng cư trú
* Xem hóa đơn điện, nước và phí ở
* Gửi yêu cầu báo hỏng thiết bị
* Nhận thông báo từ ban quản lý

---

### 4. Hệ thống ngoài (External System) – (Mở rộng)

Đây là các hệ thống bên ngoài có thể tích hợp để hỗ trợ hoạt động của hệ thống ký túc xá.

**Chức năng chính:**

* Gửi email/SMS thông báo đến sinh viên
* Hỗ trợ xác thực người dùng (SSO nếu có)
* Tích hợp dịch vụ thanh toán (trong tương lai)

---

### 3.1.3. Phạm vi hệ thống

#### Phạm vi chức năng

Hệ thống bao gồm các phân hệ chính:

* Quản lý người dùng và phân quyền
* Quản lý phòng ký túc xá
* Quản lý đăng ký và phân phòng
* Quản lý sinh viên nội trú
* Theo dõi tình trạng phòng
* Báo cáo và thống kê

#### Ngoài phạm vi (Out of Scope)

Trong giai đoạn hiện tại, hệ thống không bao gồm:

* Thanh toán học phí hoặc phí ký túc xá trực tuyến
* Quản lý chi tiết dịch vụ điện, nước
* Tích hợp với hệ thống đào tạo của nhà trường

---

### 3.1.4. Mô tả tổng quan nghiệp vụ

Quy trình hoạt động của hệ thống như sau:

1. Quản trị viên thiết lập danh sách phòng và cấu trúc ký túc xá
2. Sinh viên truy cập hệ thống và đăng ký ở ký túc xá
3. Hệ thống ghi nhận thông tin đăng ký
4. Nhân viên quản lý kiểm tra và duyệt đăng ký
5. Hệ thống phân phòng dựa trên tình trạng phòng
6. Sinh viên được cập nhật thông tin phòng ở
7. Trong quá trình ở, nhân viên cập nhật các thay đổi (chuyển phòng, trả phòng)
8. Quản trị viên theo dõi báo cáo và tình trạng hoạt động ký túc xá

## 3.2. Yêu cầu chức năng và phi chức năng

### 3.2.1. Yêu cầu chức năng (Functional Requirements)

| Mã   | Tên chức năng                | Mô tả chi tiết                                                                   | Tác nhân                    |
| ---- | ---------------------------- | -------------------------------------------------------------------------------- | --------------------------- |
| FR1  | Xác thực người dùng          | Cho phép người dùng đăng nhập, đăng xuất và duy trì phiên làm việc an toàn.      | Admin, Nhân viên, Sinh viên |
| FR2  | Quản lý phân quyền           | Thiết lập vai trò và phân quyền truy cập theo cơ chế RBAC.                       | Admin                       |
| FR3  | Quản lý phòng ký túc xá      | Quản lý thông tin phòng (số phòng, sức chứa, trạng thái, cập nhật và tra cứu).   | Admin                       |
| FR4  | Quản lý khu/tòa nhà          | Quản lý thông tin các khu/tòa nhà trong hệ thống ký túc xá.                      | Admin                       |
| FR5  | Quản lý đăng ký ở            | Sinh viên gửi yêu cầu đăng ký ở, hệ thống ghi nhận và theo dõi trạng thái xử lý. | Sinh viên                   |
| FR6  | Quản lý duyệt đăng ký        | Duyệt hoặc từ chối các yêu cầu đăng ký của sinh viên.                            | Admin, Nhân viên            |
| FR7  | Quản lý phân phòng           | Phân bổ sinh viên vào phòng phù hợp dựa trên sức chứa và tình trạng phòng.       | Admin, Nhân viên            |
| FR8  | Quản lý sinh viên nội trú    | Theo dõi và cập nhật thông tin sinh viên đang cư trú tại ký túc xá.              | Nhân viên                   |
| FR9  | Quản lý chuyển/trả phòng     | Xử lý nghiệp vụ chuyển phòng và trả phòng, cập nhật trạng thái phòng tương ứng.  | Nhân viên                   |
| FR10 | Quản lý tra cứu phòng        | Cho phép tìm kiếm, lọc và xem thông tin tình trạng phòng.                        | Admin, Nhân viên, Sinh viên |
| FR11 | Quản lý báo cáo và thống kê  | Tổng hợp dữ liệu và hiển thị báo cáo về tình trạng phòng và sinh viên.           | Admin                       |
| FR12 | Quản lý tài khoản            | Quản lý vòng đời tài khoản (tạo mới, cập nhật, khóa/mở).                         | Admin                       |
| FR13 | Quản lý điện nước            | Ghi nhận chỉ số điện, nước và tính toán mức tiêu thụ theo phòng.                 | Nhân viên                   |
| FR14 | Quản lý hóa đơn & thanh toán | Tạo hóa đơn định kỳ, cho phép sinh viên theo dõi và quản lý xác nhận thanh toán. | Admin, Nhân viên, Sinh viên |
| FR15 | Quản lý thiết bị & báo hỏng  | Ghi nhận yêu cầu báo hỏng thiết bị và theo dõi trạng thái sửa chữa.              | Nhân viên, Sinh viên        |
| FR16 | Quản lý thông báo            | Tạo và gửi thông báo chung đến sinh viên.                                        | Admin, Nhân viên            |
| FR17 | Quản lý kỷ luật              | Ghi nhận vi phạm nội quy và phục vụ đánh giá sinh viên.                          | Admin, Nhân viên            |

<center><b>Bảng 3.1 – Các yêu cầu chức năng</b></center>

---

### 3.2.2. Yêu cầu phi chức năng (Non-Functional Requirements)

| Mã    | Tên yêu cầu                            | Mô tả chi tiết                                                                        | Áp dụng cho        |
| ----- | -------------------------------------- | ------------------------------------------------------------------------------------- | ------------------ |
| NFR1  | Hiệu năng (Performance)                | Thời gian phản hồi ≤ 3 giây cho các thao tác phổ biến trong điều kiện tải trung bình. | Toàn hệ thống      |
| NFR2  | Khả năng chịu tải (Concurrency)        | Hệ thống hoạt động ổn định với tối thiểu 100 người dùng đồng thời.                    | Toàn hệ thống      |
| NFR3  | Bảo mật (Security)                     | Mật khẩu được mã hóa (Bcrypt/Argon2), API bảo vệ bằng JWT hoặc tương đương.           | Toàn hệ thống      |
| NFR4  | Toàn vẹn dữ liệu (Data Integrity)      | Đảm bảo tính nhất quán dữ liệu trong các giao dịch đồng thời.                         | Backend & CSDL     |
| NFR5  | Khả năng mở rộng (Scalability)         | Kiến trúc hệ thống theo mô hình nhiều tầng (N-Tier), dễ dàng mở rộng.                 | Kiến trúc hệ thống |
| NFR6  | Tính sẵn sàng (Availability)           | Đảm bảo uptime ≥ 99% trong thời gian vận hành.                                        | Toàn hệ thống      |
| NFR7  | Tính khả dụng (Usability)              | Giao diện trực quan, các thao tác chính hoàn thành trong ≤ 3–4 bước.                  | Frontend           |
| NFR8  | Ghi log (Audit & Logging)              | Ghi nhận đầy đủ các thao tác quan trọng (ai, làm gì, thời gian nào).                  | Backend            |
| NFR9  | Sao lưu & phục hồi (Backup & Recovery) | Dữ liệu được sao lưu hàng ngày, thời gian phục hồi ≤ 4 giờ.                           | CSDL               |
| NFR10 | Tính tương thích (Responsive)          | Giao diện hiển thị tốt trên Desktop, Tablet và Smartphone.                            | Frontend           |
| NFR11 | Tương thích trình duyệt                | Hoạt động ổn định trên các trình duyệt phổ biến (Chrome, Edge, Safari).               | Frontend           |
| NFR12 | Trải nghiệm người dùng (UX)            | Cung cấp phản hồi rõ ràng (Success, Error, Warning) sau mỗi thao tác.                 | Frontend & Backend |

<center><b>Bảng 3.2 – Các yêu cầu phi chức năng</b></center>

---