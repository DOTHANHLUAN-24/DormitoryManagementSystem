
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
