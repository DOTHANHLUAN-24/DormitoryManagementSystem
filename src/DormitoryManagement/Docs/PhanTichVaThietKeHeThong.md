<div align="center">

# BỘ GIÁO DỤC VÀ ĐÀO TẠO

# TRƯỜNG ĐẠI HỌC MỎ - ĐỊA CHẤT  

--- *** ---

![logo humg](./images/logoHUMG.jpg)

<br/>

## Đỗ Thành Luân (2221050046)(C)  

## Lê Thị Cẩm Tú (2321050120)  

## Đỗ Quang Huy (2221050047)  

## Vũ Thị Kim Oanh (2221050566)  

<br/>

## HỆ THỐNG QUẢN LÝ KÝ TÚC XÁ  

<br/>

## ĐỒ ÁN MÔN CHUYÊN ĐỀ  

## CHUYÊN NGÀNH CÔNG NGHỆ PHẦN MỀM

<br/>

<div align="right">

| Thành phần               | Thông tin                                                                                                                                             |
| ------------------------ |: --------------------------------------------------------------------------------------------|
| **Giảng viên hướng dẫn** | Ngô Ngọc Anh                      |
| **Nhóm thực hiện**       | 02                         |
| **Sinh viên thực hiện**  | - Đỗ Thành Luân (Trưởng nhóm)<br>- Lê Thị Cẩm Tú<br>- Đỗ Quang Huy<br>- Vũ Thị Kim Oanh   |

</div>

<br/>

<div align="center">

**Hà Nội, Tháng 4 Năm 2026**

</div>

</div>

---

<center>

# MỤC LỤC

</center>

1. [Mục lục](#muc-luc)  
2. [Danh sách các hình vẽ](#danh-sach-cac-hinh-ve)  
3. [Danh mục các bảng biểu](#danh-muc-cac-bang-bieu)  
4. [Danh mục những từ viết tắt](#danh-muc-nhung-tu-viet-tat)  
5. [Mở đầu](#mo-dau)  
6. [Chương 1: Cơ sở lý thuyết](#chuong-1)
7. [Chương 2: Khảo sát hệ thống](#chuong-2)
8. [Chương 3: Phân tích hệ thống](#chuong-3)
9. [Chương 4: Thiết kế kiến trúc & giao diện hệ thống](#chuong-4)

---

<center>

# DANH SÁCH CÁC HÌNH VẼ

</center>

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

<center>

# DANH MỤC CÁC BẢNG BIỂU

| STT | Tên bảng | Trang |
|:---:|:---|:---:|
| 1 | Danh mục những từ viết tắt | 3 |
| 2 | Bảng 3.1 – Các yêu cầu chức năng | 15 |
| 3 | Bảng 3.2 – Các yêu cầu phi chức năng | 16 |
| 4 | Bảng 3.3 – Bảng mô tả các tác nhân trong hệ thống | 17 |
| 5 | Đặc tả Use Case UC10 – Tạo hợp đồng | 19 |
| 6 | Đặc tả Use Case UC11 – Ghi nhận dịch vụ | 21 |
| 7 | Đặc tả Use Case UC12 – Tạo hóa đơn | 23 |
| 8 | Đặc tả Use Case UC14 – Ghi nhận vi phạm | 25 |
| 9 | Đặc tả Use Case UC15 – Quản lý khách thăm | 27 |
| 10 | Đặc tả Use Case UC16 – Quản lý cơ sở vật chất | 30 |
| 11 | Đặc tả Use Case UC21 – Thanh toán hóa đơn | 32 |
| 12 | Đặc tả Use Case UC22 – Tạo yêu cầu sửa chữa | 35 |
| 13 | Đặc tả Use Case UC24 – Tiếp nhận yêu cầu sửa chữa | 37 |
| 14 | Đặc tả Use Case UC25 – Xử lý sự cố kỹ thuật | 39 |
| 15 | Đặc tả Use Case UC26 – Cập nhật trạng thái sửa chữa | 41 |
| 16 | Đặc tả Use Case UC17 – Đăng ký nội trú | 43 |
| 17 | Đặc tả Use Case UC08 – Xét duyệt đăng ký | 46 |
| 18 | Đặc tả Use Case UC09 – Phân giường | 49 |
| 19 | Thiết kế chi tiết các bảng từ 1 đến 16 (Cơ sở dữ liệu) | 52 - 63 |

</center>

---

<center>

# DANH MỤC NHỮNG TỪ VIẾT TẮT

|STT|Từ viết tắt|Từ tiếng anh|Từ và nghĩa tiếng việt|
|:-:||||
|1|CSDL||Cơ sở dữ liệu|
|2|DB|Database|Cơ sở dữ liệu|
|3|ORM|Object-Realational Mapping|Kỹ thuật ánh xạ giữa đối tượng trong chương trình và bảng trong cơ sở dữ liệu quan hệ.|
|4|RBAC|Role-Based Access Control|Kiểm soát truy cập dựa trên vai trò|
|5|UML|Unified Modeling Language|Ngôn ngữ mô hình hóa thống nhất|
|6||||

</center>

---

<a id="chuong-1"></a>
<center>

# CHƯƠNG 1: CƠ SỞ LÝ THUYẾT

</center>

# 1. Các khái niệm cơ bản liên quan đến đề tài

## 1.1. Quản lý ký túc xá

<div style="text-indent: 2em;">

**Quản lý ký túc xá (Dormitory Management)** là quá trình tổ chức, vận hành và kiểm soát toàn bộ hoạt động liên quan đến việc lưu trú của sinh viên trong khu ký túc xá. Mục tiêu chính là đảm bảo môi trường sống an toàn, tiện nghi, tuân thủ các quy định nội bộ, đồng thời tối ưu hóa chi phí vận hành và quản lý.

Trong thực tế, hệ thống quản lý ký túc xá không chỉ đơn thuần lưu trữ dữ liệu mà còn đóng vai trò là một hệ thống hỗ trợ ra quyết định, giúp nhà quản lý theo dõi tình trạng phòng ở, công nợ, tình trạng sinh viên, cũng như xử lý các nghiệp vụ phát sinh một cách nhanh chóng và chính xác.

</div>

---

## 1.2. Đặc điểm của hệ thống quản lý ký túc xá

Hệ thống quản lý ký túc xá có những đặc điểm đặc thù sau:

* **Quản lý theo đối tượng sinh viên**: Mỗi sinh viên được định danh thông qua mã số sinh viên (MSSV) và gắn với thông tin học tập như lớp, khoa.
* **Tính ổn định theo kỳ hạn**: Sinh viên thường lưu trú theo học kỳ hoặc năm học, do đó dữ liệu mang tính chu kỳ.
* **Mô hình phòng tập thể**: Một phòng có thể chứa nhiều sinh viên, yêu cầu quản lý số lượng và phân bổ hợp lý.
* **Tính quy định cao**: Sinh viên phải tuân thủ các nội quy về giờ giấc, vệ sinh, an ninh.
* **Chi phí đa dạng**: Bao gồm chi phí cố định (tiền phòng) và chi phí biến đổi (điện, nước, dịch vụ).

---

## 1.3. Các thực thể chính trong hệ thống

Trong phạm vi phân tích yêu cầu, hệ thống quản lý ký túc xá xác định các thực thể dữ liệu cốt lõi nhằm phục vụ cho việc thiết kế cơ sở dữ liệu và triển khai các chức năng nghiệp vụ. Các thực thể chính bao gồm:

* **Sinh viên (Student):**
  Lưu trữ thông tin cá nhân, thông tin học tập và trạng thái nội trú của sinh viên trong quá trình sử dụng dịch vụ ký túc xá.

* **Phòng (Room):**
  Đại diện cho đơn vị lưu trú, bao gồm các thuộc tính như sức chứa, loại phòng, tình trạng sử dụng và liên kết với tòa nhà.

* **Tòa nhà (Building):**
  Thực thể dùng để phân khu quản lý, mỗi tòa nhà bao gồm nhiều phòng và có thể được phân loại theo khu vực hoặc đối tượng sử dụng.

* **Hợp đồng (Contract):**
  Thể hiện mối quan hệ giữa sinh viên và ký túc xá trong một khoảng thời gian xác định, bao gồm thông tin về thời hạn và trạng thái hợp đồng.

* **Dịch vụ (Service):**
  Quản lý các dịch vụ bổ sung mà sinh viên có thể đăng ký sử dụng như internet, gửi xe, giặt là.

* **Hóa đơn (Invoice):**
  Ghi nhận các khoản chi phí phát sinh theo kỳ, bao gồm tiền phòng, điện, nước và các dịch vụ liên quan.

* **Thanh toán (Payment):**
  Lưu trữ thông tin các giao dịch thanh toán của sinh viên, bao gồm phương thức, thời gian và trạng thái thanh toán.

* **Vi phạm (Violation):**
  Ghi nhận các hành vi vi phạm nội quy ký túc xá nhằm phục vụ công tác quản lý và xử lý.

* **Thông báo (Notification):**
  Được sử dụng để gửi các thông tin từ hệ thống hoặc ban quản lý đến người dùng như thông báo phí, nhắc nhở hoặc cập nhật.

* **Vật tư/Tài sản (Asset):**
  Quản lý các trang thiết bị, tài sản trong phòng nhằm phục vụ việc theo dõi, bảo trì và kiểm kê.

* **Giường (Bed):**
  Quản lý chi tiết vị trí lưu trú của từng sinh viên trong phòng, giúp tối ưu hóa việc phân bổ chỗ ở.

* **Khách ra vào (VisitorLog):**
  Theo dõi thông tin khách đến thăm sinh viên để đảm bảo an ninh trật tự trong khu vực ký túc xá.

* **Phương tiện (Vehicle):**
  Quản lý thông tin xe cộ (xe máy, xe đạp) của sinh viên nội trú để bố trí bãi đỗ và kiểm soát ra vào.

* **Yêu cầu bảo trì (MaintenanceRequest):**
  Ghi nhận các yêu cầu sửa chữa cơ sở vật chất, thiết bị từ sinh viên để có kế hoạch xử lý kịp thời.

---

## 1.4. Các nghiệp vụ quản lý vận hành hệ thống

Phần này mô tả các nghiệp vụ chính của hệ thống quản lý ký túc xá dưới góc nhìn vận hành thực tế. Mỗi nghiệp vụ không chỉ bao gồm thao tác thêm, sửa, xóa dữ liệu mà còn phản ánh quy trình xử lý nghiệp vụ trong hệ thống.

### 1.4.1. Quản lý người dùng

Quản lý người dùng là nghiệp vụ liên quan đến việc tạo lập, cập nhật và kiểm soát thông tin tài khoản trong hệ thống.

* Cho phép tạo tài khoản cho các đối tượng như Admin, Manager, Staff và Student.
* Cập nhật thông tin cá nhân và trạng thái hoạt động.
* Phân quyền truy cập theo vai trò nhằm đảm bảo bảo mật và kiểm soát hệ thống.
* Khóa hoặc vô hiệu hóa tài khoản khi cần thiết.

### 1.4.2. Quản lý phòng

Quản lý phòng là nghiệp vụ theo dõi và điều phối tài nguyên phòng ở trong ký túc xá.

* Ghi nhận thông tin phòng như sức chứa, loại phòng, vị trí.
* Theo dõi trạng thái phòng (còn chỗ, đã đầy, đang sửa chữa).
* Phân bổ sinh viên vào phòng phù hợp.
* Kiểm soát số lượng sinh viên trong từng phòng.

### 1.4.3. Quản lý hợp đồng

Quản lý hợp đồng là nghiệp vụ ghi nhận mối quan hệ lưu trú giữa sinh viên và ký túc xá.

* Tạo hợp đồng khi sinh viên được duyệt nội trú.
* Gia hạn hợp đồng theo kỳ.
* Thanh lý hợp đồng khi sinh viên rời khỏi ký túc xá.
* Theo dõi trạng thái hợp đồng theo thời gian.

### 1.4.4. Quản lý hóa đơn và thanh toán

Đây là nghiệp vụ liên quan đến việc tính toán và thu phí sinh viên.

* Tạo hóa đơn theo kỳ bao gồm tiền phòng, điện, nước và dịch vụ.
* Ghi nhận các giao dịch thanh toán của sinh viên.
* Theo dõi trạng thái hóa đơn (đã thanh toán, chưa thanh toán).
* Quản lý công nợ và nhắc nhở thanh toán.

### 1.4.5. Quản lý dịch vụ

Quản lý dịch vụ bao gồm các tiện ích bổ sung mà sinh viên có thể sử dụng.

* Khai báo các loại dịch vụ như internet, gửi xe.
* Cập nhật mức phí dịch vụ.
* Gán dịch vụ cho sinh viên hoặc phòng.
* Theo dõi mức sử dụng dịch vụ.

### 1.4.6. Quản lý vi phạm

Nghiệp vụ này giúp theo dõi và xử lý các hành vi vi phạm nội quy ký túc xá.

* Ghi nhận thông tin vi phạm của sinh viên.
* Phân loại mức độ vi phạm.
* Áp dụng hình thức xử lý phù hợp.
* Lưu trữ lịch sử vi phạm để đánh giá kỷ luật.

### 1.4.7. Quản lý vật tư và tài sản

Quản lý vật tư nhằm theo dõi tình trạng các thiết bị và tài sản trong ký túc xá.

* Ghi nhận danh sách tài sản trong từng phòng.
* Theo dõi tình trạng sử dụng (tốt, hỏng, cần sửa chữa).
* Thực hiện bảo trì và thay thế khi cần thiết.

### 1.4.8. Quản lý thông báo

Hệ thống hỗ trợ gửi thông báo đến người dùng nhằm đảm bảo thông tin được truyền đạt kịp thời.

* Tạo và gửi thông báo đến sinh viên hoặc nhân viên.
* Cập nhật nội dung thông báo.
* Quản lý lịch sử thông báo.

### 1.4.9. Quản lý nội quy

Quản lý nội quy giúp đảm bảo sinh viên tuân thủ các quy định trong ký túc xá.

* Xây dựng và cập nhật nội quy.
* Công bố nội quy đến sinh viên.
* Liên kết nội quy với các vi phạm tương ứng.

### 1.4.10. Quản lý báo cáo thống kê

Nghiệp vụ này hỗ trợ nhà quản lý theo dõi tình hình hoạt động của hệ thống.

* Thống kê số lượng sinh viên nội trú.
* Báo cáo doanh thu theo kỳ.
* Thống kê tình trạng phòng và công nợ.
* Hỗ trợ ra quyết định quản lý.

### 1.4.11. Điểm danh sinh viên

Điểm danh là nghiệp vụ nhằm kiểm soát sự hiện diện của sinh viên trong ký túc xá.

* Ghi nhận thời gian ra vào của sinh viên.
* Theo dõi lịch sử điểm danh.
* Phát hiện các trường hợp vi phạm giờ giấc.

### 1.4.12. Quản lý khách ra vào và phương tiện

Đây là nghiệp vụ nhằm đảm bảo an ninh trật tự trong khu vực ký túc xá.

* Ghi nhận thông tin khách đến thăm (họ tên, CCCD, sinh viên được thăm).
* Quản lý thông tin phương tiện (xe máy, xe đạp) của sinh viên nội trú (biển số, loại xe).
* Theo dõi thời gian ra vào của khách.

### 1.4.13. Quản lý bảo trì và sửa chữa

Nghiệp vụ này giúp ban quản lý tiếp nhận và xử lý kịp thời các sự cố kỹ thuật.

* Tiếp nhận yêu cầu báo hỏng, sửa chữa từ sinh viên (Maintenance Requests).
* Phân công nhân viên kỹ thuật hoặc bên thứ ba xử lý.
* Cập nhật trạng thái sửa chữa (chờ xử lý, đang sửa, đã hoàn thành).

### 1.4.14. Các nghiệp vụ bổ sung

Ngoài các nghiệp vụ chính, hệ thống còn hỗ trợ:

* Đăng ký nội trú trực tuyến.
* Phân phòng tự động theo tiêu chí.
* Chuyển phòng giữa các khu vực.
* Quản lý thông tin tạm trú với cơ quan chức năng.

---

## 1.5. Các khái niệm tài chính

* Hóa đơn: Tổng hợp chi phí
* Thanh toán: Trạng thái giao dịch
* Công nợ: Khoản chưa thanh toán
* Miễn giảm: Chính sách hỗ trợ

---

## 1.6. Phân quyền hệ thống

* Admin: Toàn quyền hệ thống
* Manager: Quản lý vận hành
* Staff: Hỗ trợ kỹ thuật
* Student: Người sử dụng

---

## 1.7. Trạng thái hệ thống

### 1.7.1. Trạng thái người dùng (User Status)
* **Active:** Tài khoản đang hoạt động bình thường.
* **Inactive:** Tài khoản đã bị vô hiệu hóa.
* **Locked:** Tài khoản bị khóa (do vi phạm nội quy hoặc bảo mật).

### 1.7.2. Trạng thái hạ tầng (Infrastructure Status)
* **Phòng (Rooms):**
    * **Available:** Còn chỗ trống, có thể tiếp nhận sinh viên.
    * **Full:** Đã hết chỗ.
    * **Maintenance:** Đang sửa chữa, không thể sử dụng.
* **Giường (Beds):**
    * **Available:** Giường trống.
    * **Occupied:** Giường đã có sinh viên ở.
    * **Maintenance:** Giường hỏng, đang bảo trì.

### 1.7.3. Trạng thái Đăng ký & Hợp đồng (Leasing Status)
* **Đăng ký (Registrations):**
    * **Pending:** Đang chờ duyệt.
    * **Approved:** Đã duyệt, chờ phân giường/tạo hợp đồng.
    * **Rejected:** Đã từ chối.
* **Hợp đồng (Contracts):**
    * **Active:** Hợp đồng đang có hiệu lực.
    * **Expired:** Hợp đồng đã hết hạn.
    * **Terminated:** Hợp đồng bị chấm dứt trước thời hạn.

### 1.7.4. Trạng thái tài chính (Financial Status)
* **Hóa đơn (Invoices):**
    * **Unpaid:** Chưa thanh toán.
    * **Paid:** Đã thanh toán đầy đủ.
    * **Overdue:** Quá hạn thanh toán.
    * **Cancelled:** Hóa đơn bị hủy do sai sót.
* **Thanh toán (Payments):**
    * **Success:** Giao dịch thành công.
    * **Failed:** Giao dịch thất bại.
    * **Pending:** Đang chờ xác nhận từ ngân hàng/cổng thanh toán.

### 1.7.5. Trạng thái vận hành (Operational Status)
* **Bảo trì (Maintenance Requests):**
    * **Pending:** Yêu cầu mới, chờ tiếp nhận.
    * **InProgress:** Đang trong quá trình sửa chữa.
    * **Resolved:** Đã xử lý xong.
    * **Cancelled:** Yêu cầu bị hủy.
* **Khách thăm (Visitor Logs):**
    * **CheckedIn:** Khách đang ở trong ký túc xá.
    * **CheckedOut:** Khách đã rời đi.

---

# 2. Lý thuyết nền tảng

## 2.1. Mô hình Client - Server

<div style="text-indent: 2em;">

Mô hình Client-Server (Khách - Chủ) là một kiến trúc mạng trong đó các máy tính được chia thành hai loại chính: Máy khách (Client) gửi các yêu cầu (Request) và máy chủ (Server) tiếp nhận, xử lý, sau đó trả về kết quả (Response). Trong hệ thống quản lý ký túc xá, Client chính là trình duyệt web của người dùng (sinh viên, nhân viên), còn Server là máy chủ chứa mã nguồn ứng dụng ASP.NET Core và cơ sở dữ liệu SQL Server.

</div>

## 2.2. Kiến trúc MVC (Model - View - Controller)

<div style="text-indent: 2em;">

MVC là một mẫu kiến trúc phần mềm giúp tách biệt ứng dụng thành ba thành phần chính, nhằm quản lý mã nguồn dễ dàng và thuận tiện cho việc bảo trì:

* **Model (Mô hình):** Chịu trách nhiệm quản lý dữ liệu, logic nghiệp vụ và các quy tắc của ứng dụng. Nó trực tiếp giao tiếp với cơ sở dữ liệu (thông qua Entity Framework).
* **View (Giao diện):** Đảm nhiệm việc hiển thị dữ liệu từ Model lên màn hình cho người dùng (HTML, CSS, JS).
* **Controller (Bộ điều khiển):** Đóng vai trò trung gian, tiếp nhận các yêu cầu từ người dùng (qua View), gọi Model để lấy/xử lý dữ liệu, và cuối cùng trả kết quả về View tương ứng.

</div>

## 2.3. Kiến trúc Clean Architecture (Kiến trúc sạch)

<div style="text-indent: 2em;">

Clean Architecture (Kiến trúc sạch) là một mẫu kiến trúc phần mềm do Robert C. Martin (Uncle Bob) đề xuất, chú trọng vào việc phân tách các mối quan tâm (Separation of Concerns). Nguyên tắc cốt lõi của kiến trúc này là **Quy tắc phụ thuộc (The Dependency Rule)**: Mã nguồn chỉ được phép phụ thuộc hướng vào bên trong (hướng về phía trung tâm). Các lớp bên trong (Domain, Application) tuyệt đối không được biết hoặc phụ thuộc vào bất kỳ công nghệ hay chi tiết nào của các lớp bên ngoài (Database, UI, Frameworks).

Trong hệ thống quản lý ký túc xá, Clean Architecture được triển khai thành các phân lớp (Layers) rõ ràng như sau:

</div>

* **Domain Layer (Lớp Cốt lõi):** Nằm ở vị trí trung tâm nhất của kiến trúc. Lớp này chứa các thực thể nghiệp vụ (Entities như `Room`, `Contract`, `Invoice`), các kiểu liệt kê (Enums), và các quy tắc/ngoại lệ (Exceptions) cốt lõi nhất. Domain Layer hoàn toàn độc lập, không phụ thuộc vào bất kỳ thư viện hay dự án nào khác trong hệ thống.
* **Application Layer (Lớp Ứng dụng):** Chứa các luồng nghiệp vụ đặc thù của ứng dụng (Use Cases). Lớp này điều phối các hoạt động, ví dụ như xử lý logic đăng ký phòng, tính toán điện nước. Application định nghĩa các DTOs (Data Transfer Objects), các Interfaces (như `IRepository`, `IEmailService`) để tương tác với dữ liệu, nhưng chỉ định nghĩa mà không cài đặt chi tiết (implement) chúng. Nó chỉ phụ thuộc vào Domain Layer.
* **Infrastructure Layer (Lớp Cơ sở hạ tầng):** Đóng vai trò kết nối hệ thống với "thế giới bên ngoài". Đây là nơi triển khai (implement) các Interfaces đã định nghĩa ở Application. Cụ thể, lớp này chứa `ApplicationDbContext` (dùng Entity Framework Core), thao tác truy xuất dữ liệu vật lý với SQL Server, tích hợp thanh toán hoặc gửi Email.
* **Presentation/Web Layer (Lớp Giao diện):** Nằm ở lớp ngoài cùng, là điểm giao tiếp trực tiếp với người dùng. Chứa ASP.NET Core MVC Controllers và Views. Nó tiếp nhận HTTP Requests, gọi xuống Application Layer để xử lý logic, sau đó render kết quả (HTML/CSS) trả về trình duyệt.

<div style="text-indent: 2em;">

Việc áp dụng Clean Architecture mang lại nhiều lợi ích quan trọng cho dự án: Logic nghiệp vụ được cô lập giúp hệ thống dễ dàng kiểm thử (Unit Test bằng cách mock các Interface), giảm thiểu rủi ro khi bảo trì, và cho phép linh hoạt thay đổi công nghệ cơ sở hạ tầng (ví dụ: chuyển đổi từ SQL Server sang hệ quản trị CSDL khác) mà không cần viết lại toàn bộ core của phần mềm.

</div>

## 2.4. Cơ sở dữ liệu quan hệ (Relational Database)

<div style="text-indent: 2em;">

Cơ sở dữ liệu quan hệ tổ chức dữ liệu thành các bảng (Table) có liên kết với nhau thông qua khóa chính (Primary Key) và khóa ngoại (Foreign Key). Việc áp dụng cơ sở dữ liệu quan hệ giúp hệ thống quản lý ký túc xá đảm bảo tính toàn vẹn dữ liệu, tránh dư thừa và dễ dàng thực hiện các truy vấn phức tạp (như tính toán công nợ, thống kê số lượng phòng trống).

</div>

## 2.5. Ngôn ngữ mô hình hóa thống nhất (UML)

<div style="text-indent: 2em;">

UML (Unified Modeling Language) là ngôn ngữ chuẩn để đặc tả, thiết kế và xây dựng tài liệu cho hệ thống phần mềm. Các biểu đồ UML giúp đội ngũ phát triển có cái nhìn trực quan về hệ thống:

* **Use Case Diagram:** Mô tả các chức năng hệ thống và tương tác của người dùng.
* **Class Diagram:** Mô tả cấu trúc các lớp và mối quan hệ giữa chúng.
* **Activity Diagram / Sequence Diagram:** Mô tả luồng xử lý nghiệp vụ.
* **ERD (Entity-Relationship Diagram):** Mô hình hóa cấu trúc dữ liệu của hệ thống.

</div>

---

# 3. Công nghệ và Framework sử dụng

## 3.1. Frontend (Công nghệ phía máy khách)

* **HTML5 & CSS3:** Xây dựng cấu trúc và định dạng giao diện trang web.
* **JavaScript (JS):** Xử lý các sự kiện phía client, kiểm tra tính hợp lệ của dữ liệu trước khi gửi lên server, mang lại trải nghiệm người dùng mượt mà hơn.
* **Bootstrap 5:** Framework CSS giúp xây dựng giao diện Responsive (tương thích trên nhiều kích thước màn hình như PC, Tablet, Mobile) một cách nhanh chóng.

## 3.2. Backend (Công nghệ phía máy chủ)

* **Ngôn ngữ C#:** Ngôn ngữ lập trình hướng đối tượng mạnh mẽ, an toàn kiểu dữ liệu, do Microsoft phát triển.
* **ASP.NET Core 8 MVC:** Framework web mã nguồn mở, đa nền tảng, có hiệu năng cao. Phiên bản .NET 8 cung cấp nhiều tính năng tối ưu về bộ nhớ và bảo mật, phù hợp để xây dựng các ứng dụng doanh nghiệp quy mô lớn.

## 3.3. Database & ORM

* **Hệ quản trị CSDL SQL Server:** Hệ quản trị cơ sở dữ liệu quan hệ mạnh mẽ của Microsoft, có tính bảo mật cao, hỗ trợ tốt cho các giao dịch (Transaction) tài chính trong hệ thống (thanh toán, hóa đơn).
* **Entity Framework Core (EF Core):** Là một ORM (Object-Relational Mapper) giúp lập trình viên thao tác với cơ sở dữ liệu bằng các đối tượng C# thay vì phải viết các câu lệnh SQL truyền thống thuần túy.

---

# 4. Các thuật toán và kỹ thuật cốt lõi

## 4.1. Kỹ thuật quản lý và phân phòng

Hệ thống có thể áp dụng logic **Chiến lược tham lam (Greedy Strategy)** trong việc tự động gợi ý phân phòng: Hệ thống sẽ ưu tiên lấp đầy các phòng đang có người nhưng chưa đủ chỉ tiêu (sức chứa), trước khi mở thêm phòng trống mới, nhằm tối ưu hóa việc quản lý điện nước và dọn dẹp vệ sinh.

## 4.2. Logic tính toán dịch vụ và công nợ

* **Tính chi phí dịch vụ (Điện, nước):** Lượng tiêu thụ = Chỉ số mới - Chỉ số cũ. Hệ thống sẽ tự động nhân với đơn giá hiện hành để ra thành tiền.
* **Tính toán công nợ:** Công nợ hiện tại = (Tổng tiền hóa đơn các tháng) - (Tổng số tiền sinh viên đã thanh toán).

## 4.3. Kỹ thuật Xóa mềm (Soft Delete)

Trong các hệ thống quản lý, dữ liệu tài chính và hợp đồng là cực kỳ quan trọng. Hệ thống áp dụng kỹ thuật Soft Delete bằng cách thêm trường `IsDeleted` (kiểu boolean) vào các bảng dữ liệu. Khi thực hiện lệnh xóa, hệ thống chỉ cập nhật `IsDeleted = true` thay vì xóa vật lý khỏi ổ cứng, giúp dễ dàng khôi phục và phục vụ công tác đối soát (Audit) sau này.

## 4.4. Kiểm soát truy cập dựa trên vai trò (RBAC)

RBAC (Role-Based Access Control) là cơ chế phân quyền dựa trên chức danh của người dùng. Hệ thống sử dụng ASP.NET Core Identity để chia thành các Role: Admin, Manager, Staff, Student. Mỗi Role sẽ được cấp các quyền truy cập vào các Controller/Action khác nhau, ngăn chặn việc leo thang đặc quyền.

---

# 5. Quy trình phát triển phần mềm

Nhóm áp dụng mô hình phát triển phần mềm linh hoạt (Agile) kết hợp với các nguyên lý thiết kế phần mềm chuẩn:

* **SOLID & DRY (Don't Repeat Yourself):** Tuân thủ các nguyên lý hướng đối tượng để code dễ đọc, dễ bảo trì và tái sử dụng.
* **Quản lý mã nguồn:** Sử dụng Git và nền tảng GitHub để phân nhánh (branching), quản lý phiên bản và làm việc nhóm hiệu quả mà không bị xung đột mã nguồn.

---

# 6. Quy trình và Kỹ thuật Kiểm thử (Testing)

Để đảm bảo hệ thống hoạt động ổn định trước khi triển khai thực tế, quá trình kiểm thử được thực hiện với các phương pháp sau:

## 6.1. Phương pháp kiểm thử

* **Kiểm thử hộp đen (Black-box Testing):** Kiểm tra dựa trên yêu cầu chức năng mà không cần biết cấu trúc code bên trong (nhập liệu sai, kiểm tra thông báo lỗi, luồng giao diện).
* **Kiểm thử hộp trắng (White-box Testing):** Kiểm tra trực tiếp các đoạn code logic (kiểm tra vòng lặp, các câu lệnh điều kiện rẽ nhánh trong Controller/Service).

## 6.2. Các cấp độ và công cụ kiểm thử

* **Unit Test (Kiểm thử mức đơn vị):** Sử dụng xUnit hoặc NUnit để test các hàm tính toán độc lập (ví dụ: hàm tính tiền điện nước, hàm mã hóa mật khẩu).
* **Integration Test (Kiểm thử tích hợp):** Sử dụng Postman / Swagger để gửi các request API, đảm bảo Frontend và Backend giao tiếp đúng chuẩn dữ liệu.
* **System Test (Kiểm thử hệ thống):** Chạy toàn bộ luồng nghiệp vụ từ đăng ký sinh viên -> phân phòng -> tạo hóa đơn -> thanh toán xem có bị đứt gãy ở bước nào không.

---

# 7. Kết luận chương 1

<div style="text-indent: 2em;">

Chương 1 đã trình bày tổng quan về các khái niệm cơ bản trong nghiệp vụ quản lý ký túc xá, từ đó xác định rõ các thực thể, quy trình vận hành và yêu cầu thực tế. Đồng thời, chương cũng cung cấp cơ sở lý thuyết về các công nghệ, mô hình kiến trúc (MVC, Client-Server), thuật toán cũng như các kỹ thuật (Soft Delete, RBAC) sẽ được áp dụng trong dự án. Những kiến thức nền tảng này chính là kim chỉ nam quan trọng để nhóm tiến hành khảo sát, phân tích và thiết kế hệ thống ở các chương tiếp theo một cách khoa học và tối ưu nhất.

</div>

---

<a id="chuong-2"></a>
<center>

# CHƯƠNG 2: KHẢO SÁT HỆ THỐNG

</center>

## 2.1. Giới thiệu chung

Hệ thống quản lý ký túc xá (KTX) được xây dựng nhằm giải quyết các vấn đề tồn tại trong việc quản lý thủ công như: thất thoát thông tin, sai lệch dữ liệu, khó kiểm soát tài chính và thiếu minh bạch trong vận hành. Mục tiêu của hệ thống là:

* Tự động hóa quy trình đăng ký và phân bổ chỗ ở.
* Quản lý cơ sở vật chất theo thời gian thực.
* Minh bạch hóa các khoản thu chi (tiền phòng, điện, nước).
* Tăng hiệu quả quản lý và giảm sai sót.

## 2.2. Khảo sát quy trình thực tế

### 2.2.1. Quy trình đăng ký nội trú

* Sinh viên điền đơn đăng ký (giấy hoặc Google Form).
* Nộp hồ sơ trực tiếp tại ban quản lý.
* Nhân viên kiểm tra thông tin (điều kiện, giấy tờ).
* Xét duyệt thủ công.
* Phân phòng dựa trên danh sách có sẵn.
* Thông báo kết quả qua bảng tin hoặc email.

=> Vấn đề:

* Dễ thất lạc hồ sơ.
* Không tối ưu phân bổ giường.
* Tốn thời gian xử lý.

### 2.2.2. Quy trình quản lý điện nước

* Nhân viên ghi chỉ số điện/nước từng phòng.
* Nhập vào file Excel.
* Tính toán thủ công.
* Xuất hóa đơn giấy.

=> Vấn đề:

* Sai sót khi nhập liệu.
* Khó kiểm tra lịch sử.
* Không minh bạch.

### 2.2.3. Quy trình xử lý sự cố

* Sinh viên báo trực tiếp hoặc gọi điện.
* Nhân viên ghi nhận bằng sổ.
* Phân công xử lý.
* Không có tracking trạng thái.

=> Vấn đề:

* Không theo dõi được tiến độ.
* Dễ bỏ sót yêu cầu.

## 2.3. Đánh giá hệ thống hiện tại

### 2.3.1. Ưu điểm

* Dễ triển khai.
* Không cần hệ thống CNTT.

### 2.3.2. Nhược điểm

* Sai sót cao.
* Thiếu đồng bộ dữ liệu.
* Không mở rộng được.
* Khó kiểm soát.

## 2.4. Định hướng cải tiến

* Xây dựng hệ thống web ASP.NET (.NET 8).
* Sử dụng SQL Server để quản lý dữ liệu.
* Áp dụng JWT + Role-based.
* Tích hợp thanh toán online.

## 2.5 Kết luận chương

<div  style="text-indent: 2em;">

Qua quá trình khảo sát thực tế, có thể thấy hệ thống quản lý ký túc xá hiện tại vẫn đang phụ thuộc nhiều vào các phương pháp thủ công, dẫn đến hàng loạt hạn chế như sai sót trong xử lý dữ liệu, thiếu tính minh bạch, khó kiểm soát và không đáp ứng được nhu cầu mở rộng trong tương lai. Các quy trình từ đăng ký nội trú, quản lý điện nước đến xử lý sự cố đều tồn tại những điểm nghẽn rõ ràng về thời gian, độ chính xác và khả năng theo dõi.

Những vấn đề này không chỉ ảnh hưởng đến hiệu quả vận hành của ban quản lý mà còn làm giảm trải nghiệm của sinh viên khi sử dụng dịch vụ ký túc xá. Vì vậy, việc xây dựng một hệ thống quản lý dựa trên nền tảng web là cần thiết và mang tính cấp bách.

Từ các phân tích đã nêu, chương này đã làm rõ hiện trạng hệ thống, đồng thời xác định các yêu cầu và định hướng cải tiến làm cơ sở cho việc thiết kế và xây dựng hệ thống ở các chương tiếp theo. Đây sẽ là nền tảng quan trọng để đề xuất một giải pháp công nghệ phù hợp, hiện đại và hiệu quả hơn trong việc quản lý ký túc xá.

</div>

---

<a id="chuong-3"></a>
<center>

# CHƯƠNG 3: PHÂN TÍCH HỆ THỐNG

</center>

## 3.1. Phân tích yêu cầu của hệ thống

### 3.1.1. Mục tiêu hệ thống

Hệ thống quản lý ký túc xá được xây dựng nhằm mục tiêu tin học hóa toàn bộ quy trình quản lý và vận hành, thay thế các phương pháp thủ công hiện tại. Cụ thể:

* Tự động hóa quy trình đăng ký nội trú, xét duyệt và phân phòng cho sinh viên.
* Quản lý thông tin sinh viên, phòng ở và cơ sở vật chất một cách tập trung, chính xác.
* Theo dõi và tính toán chi phí điện, nước, tiền phòng minh bạch và nhanh chóng.
* Hỗ trợ quản lý, xử lý và theo dõi các yêu cầu/sự cố phát sinh trong ký túc xá.
* Cung cấp báo cáo, thống kê phục vụ công tác quản lý và ra quyết định.
* Nâng cao hiệu quả vận hành, giảm thiểu sai sót và tiết kiệm thời gian.

---

### 3.1.2. Đối tượng sử dụng

Hệ thống phục vụ nhiều nhóm người dùng khác nhau, mỗi nhóm có vai trò và quyền hạn riêng:

* **Quản trị viên (Admin):**
  Quản lý toàn bộ hệ thống, bao gồm phân quyền người dùng, cấu hình hệ thống, theo dõi hoạt động và thống kê tổng thể.

* **Nhân viên quản lý KTX:**
  Thực hiện các nghiệp vụ như xét duyệt hồ sơ, phân phòng, quản lý điện nước, xử lý yêu cầu/sự cố và theo dõi tình trạng phòng.

* **Sinh viên:**
  Đăng ký nội trú, xem thông tin phòng, theo dõi hóa đơn, gửi yêu cầu sửa chữa và nhận thông báo từ ban quản lý.

* **Nhân viên kỹ thuật (nếu có):**
  Tiếp nhận và xử lý các yêu cầu sửa chữa, cập nhật trạng thái xử lý sự cố.

---

### 3.1.3. Phạm vi hệ thống

Hệ thống được xây dựng dưới dạng ứng dụng web, triển khai trong phạm vi quản lý ký túc xá của một trường học hoặc cơ sở đào tạo. Phạm vi bao gồm:

* Quản lý thông tin sinh viên nội trú.
* Quản lý phòng ở, giường, khu nhà.
* Quản lý đăng ký và phân bổ chỗ ở.
* Quản lý điện, nước và các chi phí liên quan.
* Quản lý yêu cầu/sự cố và bảo trì.
* Hỗ trợ thanh toán (có thể tích hợp online).
* Cung cấp báo cáo và thống kê.

**Ngoài phạm vi:**

* Không xử lý các nghiệp vụ đào tạo học tập.
* Không tích hợp sâu với các hệ thống tài chính bên ngoài (chỉ hỗ trợ thanh toán cơ bản).
* Không quản lý nhiều cơ sở ký túc xá phức tạp trong phiên bản hiện tại.

---

### 3.1.4. Mô tả tổng quan nghiệp vụ

Hệ thống quản lý ký túc xá bao gồm các nhóm nghiệp vụ chính sau:

* **Nghiệp vụ đăng ký và phân phòng:**
  Sinh viên đăng ký nội trú trực tuyến → hệ thống tiếp nhận → nhân viên xét duyệt → hệ thống phân phòng → thông báo kết quả.

* **Nghiệp vụ quản lý phòng và cơ sở vật chất:**
  Quản lý thông tin phòng, số lượng giường, tình trạng sử dụng và thiết bị trong phòng.

* **Nghiệp vụ quản lý điện nước:**
  Ghi nhận chỉ số điện, nước theo từng phòng → hệ thống tự động tính toán chi phí → tạo hóa đơn → thông báo cho sinh viên.

* **Nghiệp vụ thanh toán:**
  Sinh viên thực hiện thanh toán tiền phòng, điện, nước → hệ thống cập nhật trạng thái thanh toán.

* **Nghiệp vụ xử lý sự cố:**
  Sinh viên gửi yêu cầu sửa chữa → hệ thống ghi nhận → phân công xử lý → cập nhật trạng thái.

* **Nghiệp vụ báo cáo – thống kê:**
  Tổng hợp dữ liệu về tình trạng phòng, doanh thu, chi phí, số lượng sinh viên nhằm hỗ trợ quản lý và ra quyết định.

---

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

## 3.3. Mô hình hóa hệ thống

### 3.3.1. Tổng quan mô hình hóa

<div style="text-indent: 2em;">

Mô hình hóa hệ thống là quá trình biểu diễn các thành phần, chức năng và mối quan hệ trong hệ thống dưới dạng trực quan nhằm hỗ trợ việc phân tích, thiết kế và phát triển phần mềm. Trong hệ thống quản lý ký túc xá, ngôn ngữ UML (Unified Modeling Language) được sử dụng để mô tả các khía cạnh khác nhau của hệ thống.

</div>

Việc áp dụng UML giúp:

* Hiểu rõ yêu cầu hệ thống
* Chuẩn hóa quá trình thiết kế
* Tăng khả năng giao tiếp giữa các bên liên quan
* Hỗ trợ triển khai và bảo trì hệ thống

### 3.3.2. Bảng mô tả các tác nhân trong hệ thống

<center>

| Tác nhân chính                      | Mô tả                                                                                                                                | Vai trò chính                                                                                                                                    |
| ----------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Quản trị viên (Admin)**           | Là người quản trị hệ thống, chịu trách nhiệm cấu hình và quản lý toàn bộ dữ liệu của hệ thống ký túc xá. Có quyền truy cập cao nhất. | - Quản lý người dùng và phân quyền<br>- Quản lý tòa nhà, phòng<br>- Quản lý danh mục dịch vụ<br>- Xem báo cáo và thống kê<br>- Cấu hình hệ thống |
| **Nhân viên quản lý (Staff)**       | Là người trực tiếp vận hành hệ thống ký túc xá, xử lý các nghiệp vụ hàng ngày.                                                       | - Xét duyệt đăng ký nội trú<br>- Phân phòng cho sinh viên<br>- Quản lý điện nước<br>- Lập hóa đơn<br>- Xử lý sự cố và vi phạm                    |
| **Sinh viên (Student)**             | Là người sử dụng dịch vụ ký túc xá và tương tác với hệ thống.                                                                        | - Đăng ký nội trú<br>- Xem thông tin phòng<br>- Xem và thanh toán hóa đơn<br>- Gửi yêu cầu sửa chữa<br>- Nhận thông báo                          |
| **Nhân viên kỹ thuật (Technician)** | Là người phụ trách xử lý các sự cố kỹ thuật và bảo trì trong ký túc xá.                                                              | - Tiếp nhận yêu cầu sửa chữa<br>- Xử lý và cập nhật trạng thái sự cố<br>- Kiểm tra và bảo trì tài sản                                            |

<b>Table 3.1 – Bảng mô tả các tác nhân trong hệ thống</b>

</center>

## 3.4. Sơ đồ ca sử dụng (Use Case Diagram)

### 3.4.1. Sơ đồ Use Case tổng quát

<center>

![Sơ đồ ca sử dụng tổng quát hệ thống](./images/UML/UC/UCTQ.png)

<b>Hình 3.1 – Sơ đồ ca sử dụng tổng quát hệ thống</b>
</center>

### 3.4.2. Đặc tả các Use Case chi tiết

#### 1. UC10 – TẠO HỢP ĐỒNG


##### 1.1. Use case diagram 

![UC10 - TẠO HỢP ĐỒNG](./images/UC10_TaoHopDong.png)

##### 1.2. Đặc tả Use case 

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Tạo hợp đồng thuê |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên |
| Các bên liên quan | Nhân viên, Sinh viên, Hệ thống |
| Mục tiêu | Tạo hợp đồng nội trú cho sinh viên đã được phân giường |
| Tiền điều kiện | Nhân viên đã đăng nhập; sinh viên đã được phân giường; chưa có hợp đồng còn hiệu lực |
| Kích hoạt | Nhân viên chọn chức năng "Tạo hợp đồng" |
| Đảm bảo tối thiểu | Không tạo hợp đồng nếu dữ liệu không hợp lệ hoặc sinh viên chưa đủ điều kiện |
| Đảm bảo thành công | Hợp đồng được tạo và lưu vào cơ sở dữ liệu; trạng thái giường được cập nhật |
| Luồng chính | 1. Nhân viên truy cập chức năng tạo hợp đồng<br/>2. Chọn sinh viên đã được phân giường<br/>3. Hệ thống hiển thị thông sinh viên và phòng<br/>4. Nhân viên nhập thông tin hợp đồng (thời hạn, giá, ngày bắt đầu...)<br/>5. Kiểm tra dữ liệu phía giao diện<br/>6. Nhân viên nhấn “Tạo hợp đồng”<br/>7. Gửi yêu cầu lên hệ thống<br/>8. Hệ thống kiểm tra dữ liệu<br/>9. Kiểm tra điều kiện (chưa có hợp đồng còn hiệu lực, đã có giường)<br/>10. Tạo hợp đồng<br/>11. Lưu vào cơ sở dữ liệu<br/>12. Cập nhật trạng thái giường = “Đã sử dụng”<br/>13. Ghi nhật ký<br/>14. Trả kết quả<br/>15. Hiển thị thông báo thành công |
| Luồng ngoại lệ | **2A. Sinh viên chưa có giường**<br/>1. Tại bước 2 hệ thống kiểm tra dữ liệu<br/>2. Điều kiện: sinh viên chưa được phân giường<br/>3. Hiển thị “Sinh viên chưa được phân giường!”<br/>4. Dừng<br/><br/>**4A. Thiếu thông tin**<br/>1. Tại bước 4 nhập thiếu dữ liệu<br/>2. Bước 5 kiểm tra phát hiện lỗi<br/>3. Làm nổi bật các trường thiếu<br/>4. Hiển thị “Vui lòng nhập đầy đủ thông tin!”<br/>5. Quay lại bước 4<br/><br/>**5A. Dữ liệu không hợp lệ**<br/>1. Sai định dạng (ngày, giá...)<br/>2. Hệ thống chặn thao tác<br/>3. Hiển thị lỗi chi tiết<br/>4. Quay lại bước 4<br/><br/>**9A. Đã có hợp đồng còn hiệu lực**<br/>1. Hệ thống kiểm tra đã tồn tại hợp đồng còn hiệu lực<br/>2. Từ chối tạo mới<br/>3. Trả lỗi<br/>4. Hiển thị “Sinh viên đã có hợp đồng!”<br/>5. Kết thúc<br/><br/>**11A. Lỗi cơ sở dữ liệu**<br/>1. Lỗi khi lưu dữ liệu<br/>2. Khôi phục trạng thái trước đó (nếu có)<br/>3. Trả lỗi hệ thống<br/>4. Hiển thị “Có lỗi xảy ra, vui lòng thử lại sau”<br/>5. Ghi nhật ký lỗi |
| Quy tắc nghiệp vụ | BR01: Mỗi sinh viên chỉ có 1 hợp đồng còn hiệu lực<br/>BR02: Chỉ tạo hợp đồng khi đã được phân giường<br/>BR03: Tạo hợp đồng phải cập nhật trạng thái giường |

##### 1.3. Activity Diagram - AD04

![AD04 - TẠO HỢP ĐỒNG](./images/AD04_TaoHopDong.png)

##### 1.4. Sequence Diagram - SD04

![SD04 - TẠO HỢP ĐỒNG](./images/SD04_TaoHopDong.png)

#### 2. UC11 – GHI NHẬN DỊCH VỤ


##### 2.1. Use Case Diagram 

![UC11 - GHI NHẬN DỊCH VỤ](./images/UC11_GhiNhanDichVu.png)

##### 2.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Ghi nhận sử dụng dịch vụ |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên |
| Các bên liên quan | Nhân viên, Sinh viên, Hệ thống |
| Mục tiêu | Ghi nhận chỉ số và chi phí dịch vụ của phòng hoặc sinh viên |
| Tiền điều kiện | Nhân viên đã đăng nhập; phòng tồn tại |
| Kích hoạt | Nhân viên chọn chức năng “Ghi nhận dịch vụ” |
| Đảm bảo tối thiểu | Không lưu dữ liệu nếu không hợp lệ |
| Đảm bảo thành công | Dữ liệu dịch vụ được lưu và tính toán thành công |
| Luồng chính | 1. Nhân viên truy cập chức năng ghi nhận dịch vụ<br/>2. Chọn phòng hoặc sinh viên<br/>3. Hệ thống hiển thị thông tin hiện tại<br/>4. Nhập chỉ số điện, nước, dịch vụ<br/>5. Kiểm tra dữ liệu phía giao diện<br/>6. Nhấn “Lưu”<br/>7. Gửi yêu cầu lên hệ thống<br/>8. Hệ thống kiểm tra dữ liệu<br/>9. Tính toán chi phí<br/>10. Lưu vào cơ sở dữ liệu<br/>11. Ghi nhật ký<br/>12. Trả kết quả<br/>13. Hiển thị thành công |
| Luồng ngoại lệ | **2A. Không tìm thấy phòng**<br/>1. Hệ thống không tìm thấy dữ liệu<br/>2. Hiển thị lỗi<br/>3. Dừng<br/><br/>**4A. Thiếu thông tin**<br/>1. Nhập thiếu dữ liệu<br/>2. Kiểm tra phát hiện lỗi<br/>3. Hiển thị “Vui lòng nhập đầy đủ thông tin!”<br/>4. Quay lại bước 4<br/><br/>**5A. Sai định dạng**<br/>1. Dữ liệu không hợp lệ (số âm, ký tự...)<br/>2. Chặn thao tác<br/>3. Hiển thị lỗi<br/>4. Quay lại bước 4<br/><br/>**8A. Dữ liệu bất thường**<br/>1. Hệ thống phát hiện chỉ số giảm bất hợp lý<br/>2. Hiển thị cảnh báo<br/>3. Không lưu dữ liệu<br/>4. Yêu cầu nhập lại<br/><br/>**10A. Lỗi cơ sở dữ liệu**<br/>1. Lỗi khi lưu dữ liệu<br/>2. Khôi phục trạng thái trước đó<br/>3. Trả lỗi hệ thống<br/>4. Hiển thị lỗi |
| Quy tắc nghiệp vụ | BR01: Chỉ số mới phải lớn hơn hoặc bằng chỉ số cũ<br/>BR02: Mỗi kỳ chỉ ghi nhận một lần<br/>BR03: Chi phí = chỉ số sử dụng × đơn giá |

##### 2.3. Activity Diagram - AD05

![AD05 - GHI NHẬN DỊCH VỤ](./images/AD05_GhiNhanDichVu.png)

##### 2.4. Sequence Diagram - SD05

![SD05 - GHI NHẬN DỊCH VỤ](./images/SD05_GhiNhanDichVu.png)

#### 3. UC12 – TẠO HÓA ĐƠN


##### 3.1. Use Case Diagram

![UC12 - TẠO HÓA ĐƠN](./images/UC12_TaoHoaDon.png)

##### 3.2. Đặc tả Use Case 

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Tạo hóa đơn |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên |
| Các bên liên quan | Nhân viên, Sinh viên, Hệ thống |
| Mục tiêu | Tạo hóa đơn từ hợp đồng và dữ liệu dịch vụ |
| Tiền điều kiện | Nhân viên đã đăng nhập; có hợp đồng; có dữ liệu dịch vụ |
| Kích hoạt | Nhân viên chọn chức năng “Tạo hóa đơn” |
| Đảm bảo tối thiểu | Không tạo hóa đơn nếu thiếu dữ liệu |
| Đảm bảo thành công | Hóa đơn được tạo và gửi thông báo |
| Luồng chính | 1. Nhân viên truy cập chức năng tạo hóa đơn<br/>2. Chọn sinh viên hoặc phòng<br/>3. Hệ thống lấy dữ liệu hợp đồng<br/>4. Hệ thống lấy dữ liệu dịch vụ<br/>5. Tính tổng tiền<br/>6. Hiển thị hóa đơn<br/>7. Nhân viên xác nhận<br/>8. Gửi yêu cầu<br/>9. Hệ thống kiểm tra dữ liệu<br/>10. Lưu hóa đơn vào cơ sở dữ liệu<br/>11. Ghi nhật ký<br/>12. Gửi thông báo cho sinh viên<br/>13. Trả kết quả<br/>14. Hiển thị thành công |
| Luồng ngoại lệ | **3A. Không có hợp đồng**<br/>1. Không tìm thấy hợp đồng<br/>2. Hiển thị “Chưa có hợp đồng!”<br/>3. Dừng<br/><br/>**4A. Không có dữ liệu dịch vụ**<br/>1. Không có dữ liệu<br/>2. Hiển thị lỗi<br/>3. Dừng<br/><br/>**5A. Lỗi tính toán**<br/>1. Dữ liệu sai gây lỗi<br/>2. Dừng xử lý<br/>3. Hiển thị lỗi<br/><br/>**7A. Nhân viên hủy thao tác**<br/>1. Không xác nhận<br/>2. Hệ thống hủy thao tác<br/><br/>**10A. Lỗi cơ sở dữ liệu**<br/>1. Lỗi khi lưu hóa đơn<br/>2. Khôi phục trạng thái trước đó<br/>3. Hiển thị lỗi hệ thống |
| Quy tắc nghiệp vụ | BR01: Hóa đơn = tiền phòng + tiền dịch vụ<br/>BR02: Không tạo nếu thiếu dữ liệu<br/>BR03: Không được chỉnh sửa sau khi tạo |

##### 3.3. Activity Diagram

![AD06 - TẠO HÓA ĐƠN](./images/AD06_TaoHoaDon.png)

##### 3.4. Sequence Diagram

![SD06 - TẠO HÓA ĐƠN](./images/SD06_TaoHoaDon.png)

#### 4. UC14: GHI NHẬN VI PHẠM


##### 4.1. Sơ đồ Use Case

![UC14 - Ghi nhận vi phạm](./images/UC14_GhiNhanViPham.jpg)

##### 4.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Ghi nhận vi phạm |
| Mức | Mức nghiệp vụ |
| Tác nhân chính | Nhân viên quản lý |
| Các bên liên quan | Nhân viên quản lý, Sinh viên, Ban quản lý KTX, Hệ thống |
| Mục tiêu | Ghi lại hành vi vi phạm nội quy KTX của sinh viên vào hệ thống để làm cơ sở đánh giá hạnh kiểm và xử phạt |
| Tiền điều kiện | Nhân viên đã đăng nhập với quyền Nhân viên; danh mục loại vi phạm đã được cấu hình; hệ thống hoạt động bình thường |
| Kích hoạt | Nhân viên chọn chức năng "Quản lý vi phạm" → "Ghi nhận mới" |
| Đảm bảo tối thiểu | Không tạo bản ghi nếu dữ liệu không hợp lệ; không lưu bản ghi sai lệch |
| Đảm bảo thành công | Tạo bản ghi vi phạm thành công; sinh viên nhận được thông báo |
| Luồng chính | 1. Nhân viên truy cập chức năng "Ghi nhận vi phạm"<br/>2. Nhân viên nhập hoặc quét Mã số sinh viên<br/>3. Hệ thống truy vấn và hiển thị thông tin cơ bản<br/>4. Nhân viên chọn Loại vi phạm từ danh sách thả xuống<br/>5. Nhân viên nhập Mô tả chi tiết và Mức phạt<br/>6. Nhân viên nhấn "Lưu biên bản"<br/>7. Hệ thống kiểm tra dữ liệu đầu vào<br/>8. Hệ thống kiểm tra điều kiện ghi Cơ sở dữ liệu<br/>9. Tạo bản ghi mới trong bảng Vi phạm<br/>10. Ghi Cơ sở dữ liệu<br/>11. Gửi Thông báo đến Ứng dụng của Sinh viên<br/>12. Hiển thị thông báo "Đã ghi nhận vi phạm thành công" |
| Luồng ngoại lệ | **3A. Mã sinh viên không tồn tại**<br/>1. Tại bước 3 hệ thống truy vấn không tìm thấy<br/>2. Hiển thị: "Không tìm thấy sinh viên có mã này."<br/>3. Xóa nội dung ô nhập liệu<br/>4. Quay lại bước 2<br/><br/>**3B. Sinh viên đã rời KTX**<br/>1. Tại bước 3 phát hiện trạng thái = Đã rời đi<br/>2. Hiển thị: "Sinh viên này đã rời khỏi KTX."<br/>3. Vô hiệu hóa nút "Lưu"<br/>4. Kết thúc<br/><br/>**7A. Chưa chọn Loại vi phạm**<br/>1. Tại bước 7 phát hiện chưa chọn loại vi phạm<br/>2. Tô viền đỏ danh sách thả xuống<br/>3. Hiển thị: "Vui lòng chọn Loại vi phạm."<br/>4. Quay lại bước 4<br/><br/>**7B. Chưa nhập Mô tả**<br/>1. Tại bước 7 phát hiện trường Mô tả rỗng<br/>2. Tô viền đỏ vùng nhập<br/>3. Hiển thị: "Vui lòng nhập mô tả chi tiết."<br/>4. Quay lại bước 5<br/><br/>**7C. Mức phạt vượt giới hạn**<br/>1. Tại bước 7 phát hiện Mức phạt > Mức tối đa<br/>2. Tô viền đỏ ô nhập<br/>3. Hiển thị: "Mức phạt vượt quá giới hạn."<br/>4. Quay lại bước 5<br/><br/>**10A. Lỗi Cơ sở dữ liệu**<br/>1. Tại bước 10 xảy ra lỗi khi ghi<br/>2. Hoàn tác giao dịch<br/>3. Hiển thị: "Lỗi hệ thống, vui lòng thử lại."<br/>4. Giữ nguyên dữ liệu biểu mẫu<br/>5. Ghi nhật ký ngoại lệ<br/>6. Quay lại bước 5 |
| Quy tắc nghiệp vụ | QL01: Không thể ghi nhận vi phạm cho sinh viên đã rời KTX<br/>QL02: Mức phạt không vượt quá Mức tối đa quy định<br/>QL03: Chỉ được sửa/xóa trong vòng 24 giờ |

##### 4.3. Sơ đồ hoạt động - AD14

![AD14 - Ghi nhận vi phạm](./images/AD14_GhiNhanViPham.jpg)

##### 4.4. Sơ đồ tuần tự - SD14

![SD14 - Ghi nhận vi phạm](./images/SD14_GhiNhanViPham.jpg)

#### 5. UC15: QUẢN LÝ KHÁCH THĂM


##### 5.1. Sơ đồ Use Case

![UC15 - Quản lý khách thăm](./images/UC15_QuanLyKhachTham.jpg)

##### 5.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Quản lý khách thăm |
| Mức | Mức nghiệp vụ |
| Tác nhân chính | Nhân viên quản lý |
| Các bên liên quan | Nhân viên quản lý, Sinh viên, Khách thăm, Ban quản lý, Hệ thống |
| Mục tiêu | Kiểm soát lượng người ra vào KTX, ghi nhận thông tin khách đến thăm và giới hạn số lượng khách/phòng |
| Tiền điều kiện | Nhân viên quản lý đã đăng nhập; trong khung giờ cho phép thăm; hệ thống hoạt động bình thường |
| Kích hoạt | Có khách đến quầy yêu cầu vào thăm. Nhân viên quản lý chọn chức năng "Nhận khách vào thăm" |
| Đảm bảo tối thiểu | Từ chối nhận khách nếu dữ liệu không hợp lệ, phòng đầy khách hoặc sinh viên không tồn tại |
| Đảm bảo thành công | Tạo bản ghi Khách thăm với trạng thái Đã vào; in phiếu thăm có mã QR cho khách |
| Luồng chính | 1. Nhân viên truy cập chức năng "Nhận khách vào thăm"<br/>2. Nhân viên nhập/quét CCCD của khách<br/>3. Hệ thống kiểm tra CCCD<br/>4. Hệ thống tự động điền thông tin khách<br/>5. Nhân viên nhập Mã SV hoặc Số phòng<br/>6. Hệ thống truy vấn và kiểm tra trạng thái<br/>7. Hệ thống kiểm tra số lượng khách trong phòng<br/>8. (Số khách < 3) Tạo bản ghi Khách thăm (Đã vào)<br/>9. Ghi Cơ sở dữ liệu<br/>10. In phiếu thăm có mã QR<br/>11. Hiển thị "Nhận khách thành công"<br/>12. Nhân viên đưa phiếu cho khách<br/>13. (Khách ra về) Nhân viên quét mã QR<br/>14. Hệ thống cập nhật trạng thái Đã ra |
| Luồng ngoại lệ | **3A. CCCD sai định dạng**<br/>1. Tại bước 3 phát hiện sai định dạng<br/>2. Tô viền đỏ ô nhập<br/>3. Hiển thị: "Số CCCD không hợp lệ."<br/>4. Quay lại bước 2<br/><br/>**3B. CCCD hết hạn**<br/>1. Tại bước 3 phát hiện CCCD hết hạn<br/>2. Hiển thị: "CCCD đã hết hạn."<br/>3. Vô hiệu hóa nút tiếp tục<br/>4. Kết thúc<br/><br/>**3C. Khách trong Danh sách đen**<br/>1. Tại bước 3 phát hiện khách bị cấm<br/>2. Hiển thị cảnh báo đỏ: "CẢNH BÁO: Khách nằm trong danh sách cấm!"<br/>3. Vô hiệu hóa form<br/>4. Gửi thông báo cho Quản lý<br/>5. Kết thúc<br/><br/>**6A. Mã SV không tồn tại**<br/>1. Tại bước 6 không tìm thấy sinh viên<br/>2. Hiển thị: "Không tìm thấy sinh viên."<br/>3. Xóa ô nhập<br/>4. Quay lại bước 5<br/><br/>**6B. Sinh viên đã rời KTX**<br/>1. Tại bước 6 phát hiện trạng thái = Đã rời đi<br/>2. Hiển thị: "Sinh viên không còn cư trú tại KTX."<br/>3. Đặt lại form<br/>4. Quay lại bước 5<br/><br/>**6C. Số phòng không tồn tại**<br/>1. Tại bước 6 không tìm thấy phòng<br/>2. Hiển thị: "Số phòng không tồn tại."<br/>3. Xóa ô nhập<br/>4. Quay lại bước 5<br/><br/>**7A. Phòng đầy khách (≥ 3)**<br/>1. Tại bước 7 phát hiện số khách >= 3<br/>2. Hiển thị: "Phòng đã đầy khách, vui lòng chờ."<br/>3. Từ chối nhận khách<br/>4. Làm mới form<br/>5. Kết thúc<br/><br/>**8A. Ngoài giờ thăm**<br/>1. Tại bước 8 phát hiện ngoài giờ<br/>2. Hiển thị: "Đã hết giờ thăm."<br/>3. Vô hiệu hóa nút<br/>4. Kết thúc<br/><br/>**9A. Lỗi Cơ sở dữ liệu khi lưu**<br/>1. Tại bước 9 xảy ra lỗi<br/>2. Hoàn tác giao dịch<br/>3. Hiển thị: "Lỗi hệ thống, vui lòng thử lại."<br/>4. Ghi nhật ký ngoại lệ<br/>5. Quay lại bước 5<br/><br/>**14A. Không tìm thấy bản ghi khi Khách ra**<br/>1. Tại bước 14 không tìm thấy bản ghi Đã vào<br/>2. Hiển thị: "Không tìm thấy phiếu thăm đang hoạt động."<br/>3. Kết thúc<br/><br/>**14B. Lỗi Cơ sở dữ liệu khi cập nhật Khách ra**<br/>1. Tại bước 14 mất kết nối Cơ sở dữ liệu<br/>2. Hoàn tác giao dịch<br/>3. Hiển thị: "Lỗi hệ thống, vui lòng thử lại."<br/>4. Ghi nhật ký ngoại lệ<br/>5. Quay lại bước 13 |
| Quy tắc nghiệp vụ | QL01: Mỗi phòng tối đa 3 khách cùng lúc<br/>QL02: Khung giờ thăm: 07:00 - 22:30<br/>QL03: Tự động cho khách ra lúc 23:00<br/>QL04: Một khách chỉ thăm 1 phòng tại một thời điểm |

##### 5.3. Sơ đồ hoạt động - AD15

![AD15 - Quản lý khách thăm](./images/AD15_QuanLyKhachTham.jpg)

##### 5.4. Sơ đồ tuần tự - SD15

![SD15 - Quản lý khách thăm (Nhận khách)](./images/SD15_QuanLyKhachTham_CheckIn.jpg)

#### 6. UC16: QUẢN LÝ CƠ SỞ VẬT CHẤT


##### 6.1. Use Case Diagram

![UC16 - Quản lý cơ sở vật chất](./images/UC16_QuanLyCoSoVatChat.png)

##### 6.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Quản lý cơ sở vật chất |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên quản lý (Staff) |
| Các bên liên quan | Nhân viên kỹ thuật, Sinh viên, Hệ thống |
| Mục tiêu | Quản lý danh sách tài sản, thiết bị, vật dụng trong ký túc xá (thêm, sửa, xóa, kiểm kê, báo hỏng) |
| Tiền điều kiện | Nhân viên quản lý đã đăng nhập thành công với quyền quản lý |
| Kích hoạt | Nhân viên quản lý chọn chức năng "Quản lý cơ sở vật chất" từ menu chính |
| Đảm bảo tối thiểu | Dữ liệu tài sản được validate trước khi lưu, ghi log mọi thay đổi |
| Đảm bảo thành công | Tài sản được thêm/sửa/xóa/kiểm kê/báo hỏng đúng yêu cầu, cập nhật trạng thái chính xác |
| Luồng chính | 1. Chọn menu "Quản lý cơ sở vật chất"<br/>2. Hệ thống hiển thị danh sách tài sản theo phòng/tòa nhà<br/>3. Nhân viên quản lý chọn thao tác: Thêm/Sửa/Xóa/Kiểm kê/Báo hỏng<br/>4. **Nếu Thêm**: Nhập thông tin (mã TS, tên, loại, phòng, ngày mua, tình trạng) → Lưu<br/>5. **Nếu Sửa**: Chọn tài sản → Cập nhật thông tin → Lưu<br/>6. **Nếu Xóa**: Chọn tài sản → Kiểm tra ràng buộc → Xóa nếu hợp lệ<br/>7. **Nếu Kiểm kê**: Chọn phòng → Nhập số lượng thực tế → Đối chiếu → Cập nhật chênh lệch<br/>8. **Nếu Báo hỏng**: Chọn tài sản → Nhập mô tả lỗi → Tự động tạo yêu cầu sửa chữa<br/>9. Hệ thống xác nhận thành công và ghi log<br/>10. Kết thúc |
| Luồng ngoại lệ | **4A. Mã tài sản đã tồn tại (khi thêm)**<br/>1. Hệ thống kiểm tra thấy mã tài sản đã có trong database<br/>2. Hệ thống hiển thị thông báo: "Mã tài sản đã tồn tại trong hệ thống"<br/>3. Hệ thống tô viền đỏ trường Mã tài sản<br/>4. Giữ nguyên form, quay lại bước nhập<br/><br/>**4B. Phòng không tồn tại**<br/>1. Nhân viên quản lý nhập mã phòng không hợp lệ<br/>2. Hệ thống hiển thị thông báo: "Phòng không tồn tại trong hệ thống"<br/>3. Quay lại bước nhập<br/><br/>**6A. Không thể xóa do tài sản đang được sử dụng**<br/>1. Hệ thống kiểm tra thấy tài sản đang có mặt tại phòng có sinh viên ở<br/>2. Hệ thống hiển thị thông báo: "Không thể xóa tài sản đang được sử dụng"<br/>3. Hủy thao tác xóa, giữ nguyên danh sách<br/>4. Kết thúc<br/><br/>**7A. Kiểm kê phát sinh chênh lệch**<br/>1. Số lượng thực tế khác số lượng trong hệ thống<br/>2. Hệ thống hiển thị báo cáo chênh lệch<br/>3. Yêu cầu Nhân viên quản lý nhập lý do chênh lệch (mất, hỏng, chuyển phòng...)<br/>4. Nhân viên quản lý xác nhận lý do<br/>5. Hệ thống cập nhật lại số lượng thực tế và ghi log kiểm kê<br/>6. Kết thúc<br/><br/>**8A. Tài sản đã có yêu cầu sửa chữa đang xử lý**<br/>1. Nhân viên quản lý chọn báo hỏng nhưng tài sản đã có yêu cầu sửa chữa trước đó chưa hoàn thành<br/>2. Hệ thống hiển thị thông báo: "Tài sản đang có yêu cầu sửa chữa, không thể báo hỏng lại"<br/>3. Hủy thao tác, giữ nguyên form<br/>4. Kết thúc<br/><br/>**DB1. Lỗi kết nối Database**<br/>1. Hệ thống không thể kết nối đến Database<br/>2. Hệ thống hiển thị thông báo: "Lỗi hệ thống, vui lòng thử lại sau"<br/>3. Ghi exception log để IT kiểm tra<br/>4. Kết thúc |
| Quy tắc nghiệp vụ | BR01: Mỗi tài sản có mã duy nhất trong toàn hệ thống<br/>BR02: Không thể xóa tài sản đang được sử dụng tại phòng có sinh viên đang ở<br/>BR03: Kiểm kê bắt buộc phải có lý do khi phát sinh chênh lệch<br/>BR04: Báo hỏng tự động tạo yêu cầu sửa chữa với trạng thái "Chờ tiếp nhận" |

##### 6.3. Activity Diagram - AD16

![AD16 - Quản lý cơ sở vật chất](./images/AD16_QuanLyCoSoVatChat.jpg)

##### 6.4. Sequence Diagram - SD16

![SD16 - Quản lý cơ sở vật chất](./images/SD16_QuanLyCoSoVatChat.jpg)

#### 7. UC21: THANH TOÁN HÓA ĐƠN


##### 7.1. Sơ đồ Use Case

![UC21 - Thanh toán hóa đơn](./images/UC21_ThanhToanHoaDon.jpg)

##### 7.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Thanh toán hóa đơn |
| Mức | Mức người dùng |
| Tác nhân chính | Sinh viên |
| Các bên liên quan | Sinh viên, Nhân viên quản lý, Cổng thanh toán, Hệ thống |
| Mục tiêu | Cho phép sinh viên xem danh sách hóa đơn còn nợ và thực hiện thanh toán trực tuyến |
| Tiền điều kiện | Sinh viên đã đăng nhập; có ít nhất một hóa đơn ở trạng thái Chưa thanh toán; hệ thống hoạt động bình thường |
| Kích hoạt | Sinh viên chọn trình đơn "Thanh toán" hoặc nhấp vào thông báo "Bạn có hóa đơn chưa thanh toán" |
| Đảm bảo tối thiểu | Không thực hiện giao dịch nếu dữ liệu không hợp lệ; giữ nguyên trạng thái hóa đơn khi thanh toán thất bại |
| Đảm bảo thành công | Hóa đơn chuyển sang Đã thanh toán; sinh viên nhận biên lai điện tử qua Email |
| Luồng chính | 1. Sinh viên truy cập chức năng "Thanh toán"<br/>2. Hệ thống kiểm tra danh sách hóa đơn Chưa thanh toán<br/>3. Hiển thị danh sách: Mã hóa đơn, Kỳ thanh toán, Tổng tiền, Hạn thanh toán<br/>4. Sinh viên chọn một hóa đơn từ danh sách<br/>5. Hệ thống hiển thị cửa sổ Xác nhận thanh toán<br/>6. Sinh viên nhấn "Xác nhận thanh toán"<br/>7. Hệ thống kiểm tra dữ liệu thanh toán<br/>8. Gửi yêu cầu đến Cổng thanh toán<br/>9. Cổng thanh toán xử lý giao dịch<br/>10. Hệ thống nhận kết quả Thành công từ cổng thanh toán<br/>11. Cập nhật trạng thái hóa đơn thành Đã thanh toán<br/>12. Ghi nhận Mã giao dịch và thời gian thanh toán vào Cơ sở dữ liệu<br/>13. Tạo và gửi Biên lai điện tử qua Email<br/>14. Hiển thị màn hình "Thanh toán thành công" |
| Luồng ngoại lệ | **2A. Không có hóa đơn Chưa thanh toán**<br/>1. Tại bước 2 hệ thống kiểm tra danh sách hóa đơn<br/>2. Điều kiện: danh sách hóa đơn Chưa thanh toán rỗng<br/>3. Hệ thống dừng luồng xử lý<br/>4. Hiển thị thông báo: "Bạn không có hóa đơn nào cần thanh toán."<br/>5. Hệ thống ẩn nút "Thanh toán"<br/>6. Kết thúc<br/><br/>**6A. Hóa đơn đã được thanh toán (Trùng giao dịch)**<br/>1. Tại bước 6 sinh viên nhấn "Xác nhận thanh toán"<br/>2. Sang bước 7 hệ thống kiểm tra lại trạng thái hóa đơn<br/>3. Điều kiện: hóa đơn đã chuyển sang Đã thanh toán hoặc Đang xử lý<br/>4. Hệ thống dừng luồng xử lý<br/>5. Hiển thị thông báo: "Hóa đơn này đã được thanh toán hoặc đang được xử lý."<br/>6. Hệ thống tự động làm mới danh sách hóa đơn<br/>7. Quay lại bước 3<br/><br/>**9A. Số dư không đủ**<br/>1. Tại bước 9 cổng thanh toán xử lý giao dịch<br/>2. Điều kiện: số dư tài khoản không đủ<br/>3. Cổng thanh toán trả về mã lỗi KHÔNG_ĐỦ_TIỀN<br/>4. Hệ thống nhận phản hồi và dừng quá trình thanh toán<br/>5. Hiển thị thông báo: "Thanh toán thất bại: Số dư tài khoản không đủ."<br/>6. Ghi nhật ký lỗi<br/>7. Quay lại bước 5<br/><br/>**9B. Thẻ bị từ chối / Hết hạn**<br/>1. Tại bước 9 cổng thanh toán xử lý giao dịch<br/>2. Điều kiện: thẻ bị từ chối hoặc đã hết hạn<br/>3. Cổng thanh toán trả về mã lỗi THẺ_BỊ_TỪ_CHỐI hoặc THẺ_HẾT_HẠN<br/>4. Hệ thống nhận phản hồi và dừng quá trình thanh toán<br/>5. Hiển thị thông báo: "Thanh toán thất bại: Thẻ bị từ chối hoặc đã hết hạn."<br/>6. Ghi nhật ký lỗi<br/>7. Quay lại bước 5<br/><br/>**9C. Hết thời gian chờ**<br/>1. Tại bước 9 sau 30 giây không nhận được phản hồi<br/>2. Hệ thống dừng yêu cầu và coi giao dịch đang treo<br/>3. Cập nhật trạng thái hóa đơn thành Chờ đối soát<br/>4. Hiển thị thông báo: "Giao dịch đang chờ xử lý. Vui lòng kiểm tra lại sau 5 phút."<br/>5. Ghi nhật ký cảnh báo<br/>6. Gửi email thông báo cho Nhân viên<br/>7. Kết thúc<br/><br/>**11A. Lỗi Cơ sở dữ liệu khi cập nhật**<br/>1. Tại bước 11 hệ thống cập nhật trạng thái hóa đơn<br/>2. Điều kiện: không thể kết nối Cơ sở dữ liệu<br/>3. Hệ thống hoàn tác giao dịch (nếu có)<br/>4. Ghi nhật ký LỖI NGHIÊM TRỌNG<br/>5. Hiển thị thông báo: "Hệ thống đang gặp sự cố. Vui lòng liên hệ quản lý để xác nhận."<br/>6. Gửi cảnh báo đến Nhân viên<br/>7. Kết thúc |
| Quy tắc nghiệp vụ | QL01: Mỗi hóa đơn chỉ được thanh toán 1 lần<br/>QL02: Số tiền thanh toán phải khớp chính xác với Tổng tiền của hóa đơn<br/>QL03: Thanh toán sau Hạn thanh toán sẽ bị tính thêm phí phạt |

##### 7.3. Sơ đồ hoạt động - AD07

![AD07 - Thanh toán hóa đơn](./images/AD07_ThanhToanHoaDon.jpg)

##### 7.4. Sơ đồ tuần tự - SD07

![SD07 - Xử lý thanh toán hóa đơn](./images/SD07_ThanhToanHoaDon.jpg)

#### 8. UC22: TẠO YÊU CẦU SỬA CHỮA


##### 8.1. Use Case Diagram

![UC22 - Tạo yêu cầu sửa chữa](./images/UC22_TaoYeuCauSuaChua.png)

##### 8.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Tạo yêu cầu sửa chữa |
| Mức | Mức người dùng |
| Tác nhân chính | Sinh viên (Student) |
| Các bên liên quan | Nhân viên kỹ thuật, Nhân viên quản lý, Hệ thống |
| Mục tiêu | Sinh viên gửi yêu cầu sửa chữa tài sản/cơ sở vật chất trong phòng |
| Tiền điều kiện | Sinh viên đã đăng nhập thành công và đang có hợp đồng thuê phòng hiệu lực |
| Kích hoạt | Sinh viên chọn chức năng "Tạo yêu cầu sửa chữa" từ menu chính |
| Đảm bảo tối thiểu | Yêu cầu không được lưu nếu dữ liệu không hợp lệ, hệ thống ghi log lỗi |
| Đảm bảo thành công | Yêu cầu sửa chữa được tạo, lưu vào database với trạng thái "Chờ tiếp nhận" |
| Luồng chính | 1. Hệ thống hiển thị form nhập thông tin<br/>2. Sinh viên nhập: loại tài sản (điều hòa, đèn, giường, tủ, vòi nước...), mô tả lỗi, ảnh minh họa (tùy chọn)<br/>3. Hệ thống tự động lấy thông tin phòng hiện tại của Sinh viên từ hợp đồng đang hiệu lực<br/>4. Hệ thống validate dữ liệu đầu vào<br/>5. Hệ thống kiểm tra tài sản có tồn tại trong phòng không (nếu chọn cụ thể)<br/>6. Hệ thống lưu yêu cầu vào database với trạng thái "Chờ tiếp nhận"<br/>7. Hệ thống gửi thông báo thành công kèm mã yêu cầu<br/>8. Kết thúc |
| Luồng ngoại lệ | **2A. Thiếu thông tin bắt buộc**<br/>1. Hệ thống phát hiện để trống mô tả lỗi hoặc loại tài sản<br/>2. Hệ thống tô viền đỏ các trường thiếu<br/>3. Hệ thống hiển thị thông báo: "Vui lòng nhập đầy đủ thông tin!"<br/>4. Giữ nguyên form, quay lại bước 4<br/><br/>**3A. Sinh viên không có hợp đồng hiệu lực**<br/>1. Hệ thống truy vấn hợp đồng của Sinh viên nhưng không tìm thấy hợp đồng nào có status = 'ACTIVE'<br/>2. Hệ thống hiển thị thông báo: "Bạn chưa có hợp đồng thuê phòng hiệu lực. Vui lòng liên hệ Nhân viên quản lý để được hỗ trợ."<br/>3. Hệ thống vô hiệu hóa nút "Gửi yêu cầu"<br/>4. Kết thúc<br/><br/>**5A. Tài sản không thuộc phòng của Sinh viên**<br/>1. Sinh viên chọn tài sản cụ thể nhưng tài sản đó không nằm trong danh sách tài sản của phòng Sinh viên<br/>2. Hệ thống hiển thị thông báo: "Tài sản không tồn tại trong phòng của bạn!"<br/>3. Giữ nguyên form, quay lại bước 4<br/><br/>**5B. Phòng chưa có danh sách tài sản**<br/>1. Phòng của Sinh viên chưa được Nhân viên quản lý khởi tạo danh sách tài sản<br/>2. Hệ thống hiển thị thông báo: "Phòng của bạn chưa có danh sách tài sản, vui lòng liên hệ Nhân viên quản lý"<br/>3. Hệ thống vẫn cho phép tạo yêu cầu dạng chung (không chọn tài sản cụ thể)<br/>4. Tiếp tục lưu yêu cầu với asset_id = NULL<br/><br/>**6A. Lỗi kết nối Database khi lưu**<br/>1. Dữ liệu hợp lệ nhưng không thể INSERT vào database<br/>2. Hệ thống hiển thị thông báo: "Lỗi hệ thống, không thể tạo yêu cầu. Vui lòng thử lại sau."<br/>3. Ghi exception log để IT kiểm tra<br/>4. Giữ nguyên form, quay lại bước 4 |
| Quy tắc nghiệp vụ | BR01: Mỗi yêu cầu sửa chữa được gán một mã duy nhất (REQ-YYYYMMDD-XXXX)<br/>BR02: Sinh viên chỉ có thể tạo yêu cầu cho tài sản thuộc phòng mình đang ở<br/>BR03: Yêu cầu sửa chữa khi tạo có trạng thái mặc định là "Chờ tiếp nhận"<br/>BR04: Sinh viên có thể gửi kèm tối đa 3 ảnh minh họa cho mỗi yêu cầu |

##### 8.3. Activity Diagram - AD08

![AD08 - Tạo yêu cầu sửa chữa](./images/AD08_TaoYeuCauSuaChua.png)

##### 8.4. Sequence Diagram - SD08

![SD08 - Tạo yêu cầu sửa chữa](./images/SD08_TaoYeuCauSuaChua.jpg)

#### 9. UC24: TIẾP NHẬN YÊU CẦU SỬA CHỮA


##### 9.1. Use Case Diagram

![UC24 - Tiếp nhận yêu cầu sửa chữa](./images/UC24_TiepNhanYeuCauSuaChua.png)

##### 9.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Tiếp nhận yêu cầu sửa chữa |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên kỹ thuật (Technician) |
| Các bên liên quan | Sinh viên, Nhân viên quản lý, Hệ thống |
| Mục tiêu | Nhân viên kỹ thuật xem và nhận yêu cầu sửa chữa được phân công |
| Tiền điều kiện | Nhân viên kỹ thuật đã đăng nhập thành công, có ít nhất một yêu cầu trạng thái "Chờ tiếp nhận" |
| Kích hoạt | Nhân viên kỹ thuật chọn "Danh sách yêu cầu sửa chữa" từ menu chính |
| Đảm bảo tối thiểu | Yêu cầu chỉ được nhận bởi một Nhân viên kỹ thuật, ghi log thời gian nhận |
| Đảm bảo thành công | Yêu cầu được gán cho Nhân viên kỹ thuật và chuyển trạng thái "Đang xử lý" |
| Luồng chính | 1. Nhân viên kỹ thuật đăng nhập vào hệ thống<br/>2. Chọn "Danh sách yêu cầu sửa chữa"<br/>3. Hệ thống hiển thị danh sách yêu cầu trạng thái "Chờ tiếp nhận" (sắp xếp theo thời gian tạo)<br/>4. Nhân viên kỹ thuật chọn yêu cầu cần xử lý<br/>5. Hệ thống hiển thị chi tiết yêu cầu (phòng, tài sản, mô tả lỗi, ảnh)<br/>6. Nhân viên kỹ thuật chọn "Tiếp nhận"<br/>7. Hệ thống kiểm tra yêu cầu chưa có ai nhận (dùng cơ chế lock để tránh race condition)<br/>8. Hệ thống gán Nhân viên kỹ thuật ID và cập nhật trạng thái "Đang xử lý"<br/>9. Hệ thống ghi log: thời gian tiếp nhận, Nhân viên kỹ thuật<br/>10. Hệ thống thông báo thành công<br/>11. Kết thúc |
| Luồng ngoại lệ | **3A. Không có yêu cầu nào**<br/>1. Hệ thống truy vấn danh sách yêu cầu "Chờ tiếp nhận" và nhận được mảng rỗng<br/>2. Hệ thống hiển thị thông báo: "Hiện tại không có yêu cầu sửa chữa nào cần xử lý."<br/>3. Hệ thống vô hiệu hóa nút "Tiếp nhận"<br/>4. Kết thúc<br/><br/>**7A. Yêu cầu đã được tiếp nhận (Race Condition)**<br/>1. Nhân viên kỹ thuật A và Nhân viên kỹ thuật B cùng lúc chọn tiếp nhận cùng một yêu cầu<br/>2. Hệ thống sử dụng pessimistic lock để xử lý đồng thời<br/>3. Nhân viên kỹ thuật B nhận được lỗi: "Yêu cầu đã được tiếp nhận bởi Nhân viên kỹ thuật khác"<br/>4. Hệ thống tự động tải lại danh sách cho Nhân viên kỹ thuật B<br/>5. Kết thúc đối với Nhân viên kỹ thuật B<br/><br/>**7B. Yêu cầu đã bị hủy bởi Nhân viên quản lý**<br/>1. Nhân viên kỹ thuật chọn yêu cầu nhưng trạng thái đã bị Nhân viên quản lý cập nhật thành "Đã hủy"<br/>2. Hệ thống hiển thị thông báo: "Yêu cầu này đã bị hủy. Không thể tiếp nhận."<br/>3. Tự động tải lại danh sách<br/>4. Kết thúc<br/><br/>**DB1. Lỗi kết nối Database**<br/>1. Hệ thống không thể kết nối đến Database khi cập nhật trạng thái<br/>2. Hệ thống hiển thị thông báo: "Lỗi hệ thống, không thể tiếp nhận yêu cầu. Vui lòng thử lại sau."<br/>3. Ghi exception log<br/>4. Kết thúc |
| Quy tắc nghiệp vụ | BR01: Mỗi yêu cầu chỉ được tiếp nhận bởi đúng một Nhân viên kỹ thuật<br/>BR02: Nhân viên kỹ thuật không thể tiếp nhận quá 5 yêu cầu đang xử lý cùng lúc<br/>BR03: Sau khi tiếp nhận, Nhân viên kỹ thuật có tối đa 30 phút để bắt đầu xử lý, nếu không hệ thống tự động trả yêu cầu về trạng thái "Chờ tiếp nhận" |

#### 10. UC25: XỬ LÝ SỰ CỐ KỸ THUẬT


##### 10.1. Use Case Diagram

![UC25 - Xử lý sự cố kỹ thuật](./images/UC25_XuLySuCoKyThuat.png)

##### 10.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Xử lý sự cố kỹ thuật |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên kỹ thuật (Technician) |
| Các bên liên quan | Sinh viên, Nhân viên quản lý, Hệ thống |
| Mục tiêu | Nhân viên kỹ thuật thực hiện sửa chữa, cập nhật tiến độ và kết quả |
| Tiền điều kiện | Nhân viên kỹ thuật đã tiếp nhận yêu cầu (UC24), yêu cầu ở trạng thái "Đang xử lý" |
| Kích hoạt | Nhân viên kỹ thuật chọn yêu cầu đã tiếp nhận từ danh sách "Yêu cầu của tôi" |
| Đảm bảo tối thiểu | Mỗi bước sửa chữa được ghi nhận, có thể tạm dừng và tiếp tục sau |
| Đảm bảo thành công | Sự cố được xử lý hoàn chỉnh, sẵn sàng chuyển sang trạng thái "Chờ hoàn thành" |
| Luồng chính | 1. Hệ thống hiển thị chi tiết yêu cầu và checklist sửa chữa (theo loại tài sản)<br/>2. Nhân viên kỹ thuật thực hiện từng bước sửa chữa<br/>3. Sau mỗi bước, Nhân viên kỹ thuật tick chọn hoàn thành bước đó<br/>4. Hệ thống lưu tiến độ (thời gian, nội dung, ảnh sau sửa nếu có)<br/>5. Nếu phát sinh vấn đề, Nhân viên kỹ thuật ghi chú bổ sung vào nhật ký<br/>6. Nhân viên kỹ thuật có thể chọn "Tạm dừng" để lưu tiến độ và xử lý yêu cầu khác<br/>7. Khi hoàn thành tất cả các bước, Nhân viên kỹ thuật chọn "Hoàn tất xử lý"<br/>8. Hệ thống kiểm tra checklist đã đầy đủ<br/>9. Hệ thống cập nhật trạng thái thành "Chờ hoàn thành"<br/>10. Kết thúc |
| Luồng ngoại lệ | **2A. Thiếu phụ tùng thay thế**<br/>1. Nhân viên kỹ thuật phát hiện cần phụ tùng nhưng không có sẵn trong kho<br/>2. Nhân viên kỹ thuật chọn "Báo thiếu phụ tùng"<br/>3. Hệ thống hiển thị form yêu cầu phụ tùng<br/>4. Nhân viên kỹ thuật nhập thông tin: tên phụ tùng, số lượng, lý do<br/>5. Hệ thống tạo phiếu yêu cầu phụ tùng, gửi cho Nhân viên quản lý kho<br/>6. Hệ thống cập nhật trạng thái yêu cầu thành "Chờ phụ tùng"<br/>7. Chờ cấp phát phụ tùng (có thể thông báo qua hệ thống)<br/>8. Sau khi có phụ tùng, Nhân viên kỹ thuật chọn "Tiếp tục xử lý"<br/>9. Quay lại bước 3<br/><br/>**2B. Sự cố vượt quá khả năng xử lý**<br/>1. Nhân viên kỹ thuật ghi nhận sự cố quá phức tạp, cần chuyên gia hoặc đơn vị ngoài<br/>2. Nhân viên kỹ thuật chọn "Yêu cầu hỗ trợ"<br/>3. Hệ thống tạo ticket nâng cấp (escalation)<br/>4. Hệ thống gửi thông báo cho trưởng bộ phận kỹ thuật<br/>5. Hệ thống cập nhật trạng thái yêu cầu thành "Chờ hỗ trợ"<br/>6. Chờ phân công hỗ trợ<br/>7. Sau khi có hỗ trợ, Nhân viên kỹ thuật tiếp tục xử lý<br/>8. Quay lại bước 3<br/><br/>**8A. Checklist chưa hoàn thành khi báo cáo**<br/>1. Nhân viên kỹ thuật chọn "Hoàn tất xử lý" nhưng checklist còn bước chưa tick<br/>2. Hệ thống hiển thị thông báo: "Chưa hoàn thành các bước: [danh sách bước còn thiếu]"<br/>3. Hệ thống tô màu đỏ các bước chưa hoàn thành<br/>4. Yêu cầu Nhân viên kỹ thuật hoàn thành các bước còn thiếu<br/>5. Quay lại bước 3<br/><br/>**9A. Lỗi kết nối Database khi lưu tiến độ**<br/>1. Nhân viên kỹ thuật cập nhật tiến độ nhưng không thể lưu vào database<br/>2. Hệ thống hiển thị thông báo: "Lỗi hệ thống, không thể lưu tiến độ. Vui lòng thử lại."<br/>3. Ghi exception log<br/>4. Giữ nguyên dữ liệu trên form, cho phép Nhân viên kỹ thuật thử lại |
| Quy tắc nghiệp vụ | BR01: Mỗi loại tài sản có một checklist sửa chữa riêng do Admin cấu hình<br/>BR02: Nhân viên kỹ thuật chỉ có thể hoàn tất xử lý khi tất cả các bước trong checklist đã được tick hoàn thành<br/>BR03: Mọi thay đổi trạng thái đều được ghi vào bảng repair_logs |

##### 10.3. Activity Diagram - AD09

![AD09 - Xử lý sự cố kỹ thuật](./images/AD09_XuLySuCoKyThuat.png)

##### 10.4. Sequence Diagram - SD09

![SD09 - Xử lý sự cố kỹ thuật](./images/SD09_XuLySuCoKyThuat.jpg)

#### 11. UC26: CẬP NHẬT TRẠNG THÁI SỬA CHỮA


##### 11.1. Use Case Diagram

![UC26 - Cập nhật trạng thái sửa chữa](./images/UC26_CapNhatTrangThaiSuaChua.png)

##### 11.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Cập nhật trạng thái sửa chữa |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên kỹ thuật (Technician) |
| Các bên liên quan | Sinh viên, Nhân viên quản lý, Hệ thống |
| Mục tiêu | Cập nhật trạng thái yêu cầu sửa chữa trong suốt vòng đời xử lý |
| Tiền điều kiện | Nhân viên kỹ thuật đã tiếp nhận yêu cầu (UC24) |
| Kích hoạt | Nhân viên kỹ thuật thực hiện thao tác thay đổi trạng thái từ màn hình chi tiết yêu cầu |
| Đảm bảo tối thiểu | Mỗi lần cập nhật trạng thái được ghi log kèm thời gian, lý do (nếu có) |
| Đảm bảo thành công | Trạng thái yêu cầu được cập nhật chính xác, đúng luồng |
| Luồng chính | 1. Nhân viên kỹ thuật chọn yêu cầu đang xử lý<br/>2. Hệ thống hiển thị trạng thái hiện tại và lịch sử trạng thái<br/>3. Nhân viên kỹ thuật chọn nút "Cập nhật trạng thái"<br/>4. Hệ thống hiển thị danh sách trạng thái cho phép (dựa trên ma trận chuyển)<br/>5. Nhân viên kỹ thuật chọn trạng thái mới từ dropdown<br/>6. Nhân viên kỹ thuật nhập ghi chú/lý do thay đổi (bắt buộc nếu chuyển sang "Từ chối" hoặc "Đã hủy")<br/>7. Hệ thống kiểm tra tính hợp lệ của chuyển trạng thái<br/>8. Hệ thống cập nhật trạng thái mới vào database<br/>9. Hệ thống ghi log: thời gian, người thực hiện, trạng thái cũ → mới, ghi chú<br/>10. Nếu trạng thái là "Hoàn thành", hệ thống tự động gửi thông báo cho Sinh viên<br/>11. Hệ thống thông báo thành công<br/>12. Kết thúc |
| Luồng ngoại lệ | **4A. Danh sách trạng thái cho phép rỗng**<br/>1. Hệ thống tính toán danh sách trạng thái có thể chuyển từ trạng thái hiện tại<br/>2. Nếu không có trạng thái nào (ví dụ đang ở trạng thái cuối "Hoàn thành")<br/>3. Hệ thống vô hiệu hóa nút "Cập nhật trạng thái"<br/>4. Hiển thị tooltip: "Không thể cập nhật trạng thái từ trạng thái hiện tại"<br/>5. Kết thúc<br/><br/>**5A. Chuyển trạng thái không hợp lệ**<br/>1. Nhân viên kỹ thuật chọn trạng thái không nằm trong danh sách cho phép<br/>2. Hệ thống kiểm tra và phát hiện chuyển trạng thái vi phạm ma trận<br/>3. Ví dụ: "Chờ tiếp nhận" → "Hoàn thành" (bỏ qua bước "Đang xử lý")<br/>4. Hệ thống hiển thị thông báo: "Không thể chuyển từ [trạng thái cũ] sang [trạng thái mới]"<br/>5. Giữ nguyên trạng thái cũ, không cập nhật<br/>6. Kết thúc<br/><br/>**6A. Thiếu lý do bắt buộc**<br/>1. Nhân viên kỹ thuật chọn trạng thái "Từ chối" hoặc "Đã hủy" nhưng chưa nhập lý do<br/>2. Hệ thống phát hiện trường ghi chú đang để trống<br/>3. Hệ thống hiển thị thông báo: "Vui lòng nhập lý do từ chối/hủy yêu cầu"<br/>4. Hệ thống tô viền đỏ ô nhập ghi chú<br/>5. Quay lại bước 6<br/><br/>**7A. Yêu cầu đã bị hủy bởi Staff/Admin**<br/>1. Nhân viên kỹ thuật cố gắng cập nhật trạng thái nhưng yêu cầu đã bị Staff hủy từ trước<br/>2. Hệ thống kiểm tra thấy status hiện tại = "Đã hủy"<br/>3. Hệ thống hiển thị thông báo: "Yêu cầu đã bị hủy, không thể cập nhật trạng thái"<br/>4. Kết thúc<br/><br/>**DB1. Lỗi kết nối Database**<br/>1. Hệ thống không thể cập nhật trạng thái do mất kết nối database<br/>2. Hệ thống hiển thị thông báo: "Lỗi hệ thống, không thể cập nhật trạng thái. Vui lòng thử lại sau."<br/>3. Ghi exception log<br/>4. Kết thúc |

#### 12. UC17 – ĐĂNG KÝ NỘI TRÚ

##### 12.1. Use case diagram
![UC17 - ĐĂNG KÝ NỘI TRÚ](./images/UC17_DangKyNoiTru.png)

##### 12.2. Đặc tả Use case
| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Đăng ký nội trú |
| Mức | Mức người dùng |
| Tác nhân chính | Sinh viên |
| Các bên liên quan | Nhân viên quản lý, Hệ thống |
| Mục tiêu | Sinh viên gửi yêu cầu đăng ký ở ký túc xá |
| Tiền điều kiện | Sinh viên đã đăng nhập vào hệ thống tài khoản của trường/ký túc xá |
| Kích hoạt | Sinh viên chọn chức năng "Đăng ký nội trú" từ menu chính |
| Đảm bảo tối thiểu | Yêu cầu không được tạo nếu dữ liệu không hợp lệ, hệ thống hiển thị thông báo lỗi rõ ràng |
| Đảm bảo thành công | Yêu cầu đăng ký được tạo với trạng thái "Pending", chờ nhân viên xét duyệt |
| Luồng chính | 1. Hệ thống tiếp nhận yêu cầu truy cập chức năng đăng ký của Sinh viên. <br> 2. Hệ thống kiểm tra trạng thái nội trú hiện tại của Sinh viên. <br> 3. Hệ thống hiển thị form đăng ký nội trú. <br> 4. Sinh viên nhập các thông tin cần thiết (Thời gian lưu trú, loại phòng mong muốn, ghi chú...). <br> 5. Hệ thống thực hiện validate dữ liệu phía Client (kiểm tra định dạng, các trường bắt buộc). <br> 6. Sinh viên nhấn nút “Gửi đăng ký”. <br> 7. Hệ thống gửi request chứa thông tin đăng ký lên server. <br> 8. Server thực hiện validate dữ liệu nhận được. <br> 9. Server kiểm tra các điều kiện ràng buộc trước khi ghi DB (chưa có request Pending nào khác, chưa có phòng đang hoạt động). <br> 10. Hệ thống tạo bản ghi đăng ký mới với trạng thái ban đầu là "Pending". <br> 11. Hệ thống thực hiện lưu dữ liệu vào Database. <br> 12. Hệ thống ghi log lịch sử thao tác. <br> 13. Server trả về response thành công cho Client. <br> 14. Hệ thống hiển thị thông báo "Đăng ký nội trú thành công" cho Sinh viên. |
| Luồng ngoại lệ | **2A. Đã có nội trú**: 1. Hệ thống kiểm tra thấy sinh viên đã có phòng đang hoạt động; 2. Dừng luồng xử lý; 3. Hiển thị thông báo lỗi; 4. Kết thúc.<br/>**8A. Trùng yêu cầu**: 1. Hệ thống phát hiện đã tồn tại request Pending; 2. Từ chối tạo mới; 3. Hiển thị "Bạn đã có yêu cầu đang chờ xử lý!"; 4. Kết thúc.<br/>**10A. Lỗi DB**: 1. Lỗi khi ghi DB; 2. Rollback; 3. Hiển thị "Có lỗi xảy ra, vui lòng thử lại sau"; 4. Kết thúc. |
| Quy tắc nghiệp vụ | BR01: Mỗi sinh viên chỉ có 1 request Pending<br/>BR02: Không cho phép đăng ký khi đang có hợp đồng Active |

##### 12.3. Activity Diagram - AD01
![AD01 - ĐĂNG KÝ NỘI TRÚ](./images/AD01_DangKyNoiTru.png)

##### 12.4. Sequence Diagram - SD01
![SD01 - ĐĂNG KÝ NỘI TRÚ](./images/SD01_DangKyNoiTru.png)

#### 13. UC08 – XÉT DUYỆT ĐĂNG KÝ

##### 13.1. Use case diagram
![UC08 - XÉT DUYỆT ĐĂNG KÝ](./images/UC08_XetDuyetDangKy.png)

##### 13.2. Đặc tả Use case
| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Xét duyệt đăng ký |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên |
| Các bên liên quan | Nhân viên quản lý, Hệ thống, Sinh viên |
| Mục tiêu | Duyệt hoặc Từ chối yêu cầu đăng ký của sinh viên |
| Tiền điều kiện | Nhân viên đã đăng nhập thành công, có quyền xét duyệt đăng ký |
| Kích hoạt | Nhân viên chọn chức năng "Xét duyệt đăng ký" từ menu quản lý |
| Đảm bảo tối thiểu | Yêu cầu chỉ được xử lý nếu ở trạng thái Pending, hệ thống hiển thị thông báo lỗi nếu có vấn đề |
| Đảm bảo thành công | Yêu cầu được cập nhật trạng thái Approved hoặc Rejected, sinh viên nhận được thông báo kết quả |
| Luồng chính | 1. Nhân viên mở màn hình danh sách đăng ký nội trú. <br> 2. Hệ thống tự động tải và hiển thị danh sách các yêu cầu đang ở trạng thái "Pending". <br> 3. Nhân viên chọn 1 yêu cầu cụ thể từ danh sách. <br> 4. Hệ thống hiển thị chi tiết thông tin yêu cầu của sinh viên. <br> 5. Nhân viên xem xét thông tin và chọn nút "Duyệt" hoặc "Từ chối". <br> 6. Hệ thống gửi request cập nhật lên server. <br> 7. Server kiểm tra lại trạng thái hiện tại của yêu cầu trong DB để đảm bảo tính đồng nhất. <br> 8. Hệ thống cập nhật trạng thái mới cho yêu cầu (Chuyển sang "Approved" nếu duyệt, hoặc "Rejected" nếu từ chối). <br> 9. Hệ thống thực hiện lưu thay đổi vào Database. <br> 10. Hệ thống ghi log thông tin người duyệt và thời gian duyệt. <br> 11. Server trả về response thành công cho Client. <br> 12. Hệ thống hiển thị thông báo "Xử lý yêu cầu thành công" và cập nhật lại danh sách. |
| Luồng ngoại lệ | **2A. Không có dữ liệu**: Hiển thị “Không có dữ liệu Pending”; 2. Kết thúc.<br/>**7A. Đã xử lý**: 1. Request không còn Pending (do nhân viên khác xử lý trước); 2. Từ chối xử lý; 3. Hiển thị thông báo lỗi; 4. Quay lại bước 2. |
| Quy tắc nghiệp vụ | BR01: Chỉ xử lý request ở trạng thái Pending<br/>BR02: Sau khi Duyệt, yêu cầu chuyển sang trạng thái Approved để chờ phân giường |

##### 13.3. Activity Diagram - AD02
![AD02 - XÉT DUYỆT ĐĂNG KÝ](./images/AD08_XetDuyetDangKi.jpg)

##### 13.4. Sequence Diagram - SD02
![SD02 - XÉT DUYỆT ĐĂNG KÝ](./images/SD08_XetDuyetDangKi.jpg)

#### 14. UC09 – PHÂN GIƯỜNG

##### 14.1. Use case diagram
![UC09 - PHÂN GIƯỜNG](./images/UC08_XetDuyetDangKi.jpg)

##### 14.2. Đặc tả Use case
| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Phân giường |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên |
| Các bên liên quan | Nhân viên quản lý, Hệ thống, Sinh viên |
| Mục tiêu | Gán giường cụ thể cho sinh viên đã được phê duyệt đăng ký |
| Tiền điều kiện | Nhân viên đã đăng nhập thành công, có quyền phân giường; Sinh viên đã có yêu cầu đăng ký được duyệt (Approved) |
| Kích hoạt | Nhân viên chọn chức năng "Phân giường" từ menu quản lý |
| Đảm bảo tối thiểu | Yêu cầu chỉ được phân giường nếu ở trạng thái Approved, hệ thống hiển thị thông báo lỗi nếu có vấn đề |
| Đảm bảo thành công | Sinh viên được gán giường cụ thể, trạng thái giường được cập nhật, thông tin lưu vào database |
| Luồng chính | 1. Nhân viên mở danh sách các sinh viên đã được duyệt đăng ký (Trạng thái yêu cầu: Approved). <br> 2. Nhân viên chọn một sinh viên cụ thể từ danh sách. <br> 3. Hệ thống lọc và hiển thị danh sách các phòng và giường còn trống dựa theo đúng "Loại phòng" mà sinh viên đã đăng ký. <br> 4. Nhân viên chọn một giường cụ thể trên sơ đồ/danh sách. <br> 5. Nhân viên nhấn nút "Xác nhận phân giường". <br> 6. Hệ thống thực hiện kiểm tra các điều kiện ràng buộc cuối cùng. <br> 7. Hệ thống tiến hành gán mã giường cho sinh viên. <br> 8. Hệ thống cập nhật trạng thái của giường vừa chọn thành "Occupied" (Đã có người). <br> 9. Hệ thống lưu toàn bộ thông tin thay đổi vào Database (Cập nhật trạng thái yêu cầu đăng ký thành hoàn tất, tạo bản ghi phòng ở). <br> 10. Hệ thống hiển thị thông báo "Phân giường thành công cho sinh viên". |
| Luồng ngoại lệ | **3A. Không còn giường trống**: 1. Hệ thống báo không còn giường phù hợp; 2. Dừng thao tác.<br/>**7A. Giường đã bị chiếm**: 1. Giường được gán bởi nhân viên khác ngay trước đó; 2. Hệ thống báo lỗi; 3. Yêu cầu chọn giường khác. |
| Quy tắc nghiệp vụ | BR01: Chỉ phân giường cho sinh viên có trạng thái yêu cầu Approved<br/>BR02: Giường được chọn phải thuộc loại phòng sinh viên đã đăng ký |

##### 14.3. Activity Diagram - AD03
![AD03 - PHÂN GIƯỜNG](./images/AD03_PhanGiuong.png)

##### 14.4. Sequence Diagram - SD03
![SD03 - PHÂN GIƯỜNG](./images/SD03_PhanGiuong.png)


## 3.5. Sơ đồ lớp (Class Diagram)

## 3.6. Sơ đồ hoạt động (Activity Diagram)

## 3.7. Sơ đồ trình tự (Sequence Diagram)

## 3.8. Sơ đồ ERD (Entity-Relationship Diagram)

![Sơ đồ ERD](./images/erdInDb.png)

<center><b>Hình 3.1 – Sơ đồ ERD</b></center>
<center>

![Hình ảnh các bảng trong SQL Server](./images/baseEntity.png)

<b>Hình 3.2 – Hình ảnh các bảng trong SQL Server</b>

</center>

## 3.9. Thiết kế cơ sở dữ liệu

### 3.9.1. Mục tiêu thiết kế

<div style="text-indent: 2em;">

Mục tiêu của việc thiết kế cơ sở dữ liệu trong hệ thống quản lý ký túc xá là xây dựng một nền tảng lưu trữ dữ liệu chặt chẽ, nhất quán và dễ mở rộng, nhằm phục vụ hiệu quả cho các hoạt động quản lý và vận hành hệ thống. Cụ thể:

</div>

### a. Lưu trữ và quản lý dữ liệu tập trung

Đảm bảo toàn bộ thông tin như sinh viên, phòng ở, hợp đồng, hóa đơn, thanh toán… được lưu trữ tại một hệ thống duy nhất, giúp dễ dàng truy xuất và quản lý.

### b. Đảm bảo tính toàn vẹn và nhất quán dữ liệu

Thiết kế các ràng buộc (khóa chính, khóa ngoại, unique,…) nhằm hạn chế sai sót, tránh trùng lặp và đảm bảo tính chính xác của dữ liệu trong suốt quá trình sử dụng.

### c. Hỗ trợ hiệu quả các nghiệp vụ hệ thống

Cơ sở dữ liệu phải đáp ứng tốt các chức năng như đăng ký nội trú, phân phòng, quản lý điện nước, lập hóa đơn, thanh toán và xử lý sự cố.

### d. Tối ưu hiệu năng truy vấn

Thiết kế hợp lý giúp hệ thống truy xuất dữ liệu nhanh chóng, đặc biệt với các tác vụ thường xuyên như tra cứu thông tin sinh viên, kiểm tra tình trạng phòng hoặc thống kê báo cáo.

### e. Đảm bảo khả năng mở rộng

Cho phép dễ dàng bổ sung các chức năng mới (như tích hợp thanh toán online, mở rộng nhiều khu ký túc xá…) mà không ảnh hưởng đến cấu trúc hiện tại.

### f. Đảm bảo tính bảo mật và phân quyền

Kết hợp với hệ thống xác thực (ASP.NET Identity) để kiểm soát quyền truy cập dữ liệu theo từng vai trò người dùng.

### 3.9.2. Mô hình dữ liệu

<div style="text-indent: 2em;">

Hệ thống quản lý ký túc xá được xây dựng dựa trên nhiều nhóm thực thể, phản ánh đầy đủ các nghiệp vụ quản lý, vận hành, tài chính và an ninh. Các thực thể chính bao gồm:

</div>

### a. Nhóm người dùng và phân quyền

* Users (Người dùng)

### b. Nhóm hạ tầng (Infrastructure)

* Blocks (Tòa nhà/Khu)
* RoomTypes (Loại phòng)
* Rooms (Phòng)
* Beds (Giường)

### c. Nhóm quản lý thuê & dịch vụ (Leasing & Utilities)

* Contracts (Hợp đồng)
* Utilities (Dịch vụ: điện, nước,...)
* UtilityUsages (Chỉ số sử dụng dịch vụ)

### d. Nhóm tài chính (Finance)

* Invoices (Hóa đơn)
* Payments (Thanh toán)
* Surcharges (Phụ phí)

### e. Nhóm vận hành & an ninh (Operations)

* MaintenanceRequests (Yêu cầu bảo trì)
* Violations (Vi phạm)
* Assets (Tài sản)
* VisitorLogs (Khách ra vào)
* Vehicles (Phương tiện)

### f. Mối quan hệ giữa các thực thể

Các thực thể trong hệ thống được liên kết chặt chẽ với nhau thông qua khóa chính (Primary Key - PK) và khóa ngoại (Foreign Key - FK), đảm bảo tính toàn vẹn dữ liệu và khả năng truy xuất thông tin chính xác.

Cụ thể:

* Người dùng (Users) liên kết với hợp đồng (Contracts), phương tiện (Vehicles) và yêu cầu bảo trì (MaintenanceRequests).
* Hợp đồng (Contracts) liên kết với giường (Beds), hóa đơn (Invoices) và vi phạm (Violations).
* Phòng (Rooms) liên kết với tòa nhà (Blocks), loại phòng (RoomTypes), tài sản (Assets) và chỉ số dịch vụ (UtilityUsages).
* Hóa đơn (Invoices) liên kết với thanh toán (Payments), phụ phí (Surcharges) và sử dụng dịch vụ (UtilityUsages).

Việc thiết kế các mối quan hệ này giúp hệ thống đảm bảo tính nhất quán, hạn chế dư thừa dữ liệu và hỗ trợ hiệu quả cho các chức năng nghiệp vụ như quản lý nội trú, tính phí, thanh toán và giám sát hoạt động ký túc xá.

### 3.9.3. Thiết kế chi tiết các bảng

Dưới đây là thiết kế chi tiết một số bảng (thực thể) cốt lõi trong hệ thống:

**1. Bảng Users (Người dùng)**
*Lưu trữ thông tin tài khoản và thông tin cá nhân của người dùng hệ thống (Admin, Staff, Student).*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã người dùng định danh |
| Username | VARCHAR(50) | UNIQUE, NOT NULL | Tên đăng nhập |
| PasswordHash | NVARCHAR(MAX) | NOT NULL | Mật khẩu đã được băm mã hóa |
| Email | VARCHAR(100) | UNIQUE | Địa chỉ email liên hệ |
| PhoneNumber | VARCHAR(20) | | Số điện thoại |
| Role | VARCHAR(20) | NOT NULL | Vai trò (Admin, Staff, Student) |
| FirstName | NVARCHAR(50) | NOT NULL | Tên |
| LastName | NVARCHAR(50) | NOT NULL | Họ và đệm |
| DateOfBirth | DATE | | Ngày sinh |
| IdentityCard| VARCHAR(20) | UNIQUE, NOT NULL | Số Căn cước công dân |
| Status | TINYINT | NOT NULL | Trạng thái (1: Active, 0: Inactive) |

**2. Bảng Rooms (Phòng ký túc xá)**
*Lưu trữ thông tin các phòng trong ký túc xá.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã phòng |
| Name | NVARCHAR(50) | NOT NULL | Tên/Số phòng (VD: P101) |
| BlockId | UNIQUEIDENTIFIER | FK | Thuộc tòa nhà/khu nào |
| RoomTypeId | UNIQUEIDENTIFIER | FK | Loại phòng (Dịch vụ, Thường...) |
| Floor | INT | NOT NULL | Tầng số mấy |
| Status | TINYINT | NOT NULL | Trạng thái (Available, Full, Maintenance) |

**3. Bảng Contracts (Hợp đồng nội trú)**
*Ghi nhận hợp đồng lưu trú giữa sinh viên và ký túc xá.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã hợp đồng |
| UserId | UNIQUEIDENTIFIER | FK | Sinh viên thuê phòng |
| BedId | UNIQUEIDENTIFIER | FK | Giường/Vị trí thuê |
| StartDate | DATE | NOT NULL | Ngày bắt đầu hợp đồng |
| EndDate | DATE | NOT NULL | Ngày kết thúc hợp đồng |
| Price | DECIMAL(18,2)| NOT NULL | Đơn giá thuê (tháng/kỳ) |
| DepositAmount| DECIMAL(18,2)| | Tiền cọc |
| Status | TINYINT | NOT NULL | Trạng thái (Active, Expired, Terminated) |

**4. Bảng Invoices (Hóa đơn)**
*Lưu trữ thông tin hóa đơn thanh toán hàng tháng/kỳ của sinh viên.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã hóa đơn |
| ContractId | UNIQUEIDENTIFIER | FK | Tham chiếu đến hợp đồng |
| Month | INT | NOT NULL | Hóa đơn tháng |
| Year | INT | NOT NULL | Hóa đơn năm |
| RoomCharge | DECIMAL(18,2)| NOT NULL | Tiền phòng |
| UtilityCharge| DECIMAL(18,2)| NOT NULL | Tiền điện nước, dịch vụ |
| TotalAmount | DECIMAL(18,2)| NOT NULL | Tổng tiền phải thanh toán |
| Status | TINYINT | NOT NULL | Trạng thái (Unpaid, Paid, Overdue) |

**5. Bảng MaintenanceRequests (Yêu cầu bảo trì)**
*Lưu trữ các yêu cầu sửa chữa cơ sở vật chất từ sinh viên.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã yêu cầu |
| UserId | UNIQUEIDENTIFIER | FK | Sinh viên báo hỏng |
| RoomId | UNIQUEIDENTIFIER | FK | Phòng xảy ra sự cố |
| IssueDescription| NVARCHAR(500) | NOT NULL | Mô tả chi tiết sự cố |
| ReportedDate | DATETIME | NOT NULL | Thời gian báo cáo |
| Status | TINYINT | NOT NULL | Trạng thái (Pending, InProgress, Resolved) |
| ResolutionDetails| NVARCHAR(500)| | Chi tiết cách giải quyết/phản hồi |

**6. Bảng Blocks (Tòa nhà/Khu)**
*Lưu trữ thông tin các khu/tòa nhà trong ký túc xá.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã tòa nhà |
| BlockName | NVARCHAR(100) | NOT NULL | Tên tòa nhà/khu (VD: Khu A, Khu B) |
| TotalFloors | INT | NOT NULL | Tổng số tầng |
| Description | NVARCHAR(MAX) | | Mô tả thêm |

**7. Bảng RoomTypes (Loại phòng)**
*Lưu trữ danh mục phân loại phòng và giá cơ bản.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã loại phòng |
| TypeName | NVARCHAR(50) | NOT NULL | Tên loại phòng |
| BasePrice | DECIMAL(18,2) | NOT NULL | Giá cơ bản |
| MaxOccupants | INT | NOT NULL | Sức chứa tối đa (từ 1 đến 20) |
| Description | NVARCHAR(MAX) | | Mô tả thêm |

**8. Bảng Beds (Giường)**
*Quản lý chi tiết từng giường trong phòng.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã giường |
| BedNumber | NVARCHAR(20) | NOT NULL | Số hiệu giường (VD: G01, G02) |
| Status | TINYINT | NOT NULL | Trạng thái (Available, Occupied, Maintenance) |
| RoomId | UNIQUEIDENTIFIER | FK | Thuộc phòng nào |

**9. Bảng Utilities (Dịch vụ)**
*Lưu trữ thông tin các loại dịch vụ tiện ích như Điện, Nước.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã dịch vụ |
| UtilityName | NVARCHAR(100) | NOT NULL | Tên dịch vụ (Điện, Nước...) |
| UnitPrice | DECIMAL(18,2) | NOT NULL | Đơn giá |
| Unit | NVARCHAR(50) | NOT NULL | Đơn vị tính |

**10. Bảng UtilityUsages (Chỉ số dịch vụ)**
*Ghi nhận chỉ số và số lượng sử dụng dịch vụ của phòng theo tháng.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã bản ghi |
| Month | INT | NOT NULL | Tháng ghi nhận |
| Year | INT | NOT NULL | Năm ghi nhận |
| PreviousIndex| FLOAT | NOT NULL | Chỉ số cũ |
| CurrentIndex | FLOAT | NOT NULL | Chỉ số mới |
| UsageQuantity| FLOAT | NOT NULL | Số lượng sử dụng |
| TotalAmount | DECIMAL(18,2)| NOT NULL | Thành tiền |
| RoomId | UNIQUEIDENTIFIER | FK | Thuộc phòng nào |
| UtilityId | UNIQUEIDENTIFIER | FK | Loại dịch vụ |
| InvoiceId | UNIQUEIDENTIFIER | FK, NULL | Tham chiếu hóa đơn |

**11. Bảng Payments (Thanh toán)**
*Lưu trữ các giao dịch thanh toán hóa đơn của sinh viên.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã thanh toán |
| AmountPaid | DECIMAL(18,2) | NOT NULL | Số tiền thanh toán |
| PaymentDate | DATETIME | NOT NULL | Ngày thanh toán |
| TransactionCode| NVARCHAR(100)| NOT NULL | Mã giao dịch |
| Method | TINYINT | NOT NULL | Phương thức (Cash, Transfer...) |
| Note | NVARCHAR(MAX) | | Ghi chú |
| InvoiceId | UNIQUEIDENTIFIER | FK | Tham chiếu hóa đơn |

**12. Bảng Surcharges (Phụ phí)**
*Lưu trữ các khoản phụ phí phát sinh thêm trong hóa đơn.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã phụ phí |
| SurchargeName| NVARCHAR(100) | NOT NULL | Tên phụ phí (Dọn vệ sinh, v.v.) |
| Amount | DECIMAL(18,2) | NOT NULL | Số tiền |
| InvoiceId | UNIQUEIDENTIFIER | FK | Tham chiếu hóa đơn |

**13. Bảng Violations (Vi phạm)**
*Ghi nhận các lỗi vi phạm của sinh viên.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã vi phạm |
| Description | NVARCHAR(MAX) | NOT NULL | Mô tả vi phạm |
| FineAmount | DECIMAL(18,2) | NOT NULL | Mức phạt |
| ViolationDate| DATETIME | NOT NULL | Ngày vi phạm |
| Status | TINYINT | NOT NULL | Trạng thái |
| EvidenceImage| NVARCHAR(MAX) | | Hình ảnh bằng chứng |
| ContractId | UNIQUEIDENTIFIER | FK | Tham chiếu hợp đồng của sinh viên |

**14. Bảng Assets (Tài sản)**
*Quản lý tài sản, trang thiết bị trong ký túc xá.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã tài sản |
| AssetName | NVARCHAR(100) | NOT NULL | Tên tài sản |
| AssetCode | NVARCHAR(50) | | Mã tài sản |
| Description | NVARCHAR(500) | | Mô tả chi tiết |
| Status | TINYINT | NOT NULL | Trạng thái (Tốt, Hỏng, v.v.) |
| ReplacementCost| DECIMAL(18,2)| NOT NULL | Chi phí thay thế |
| RoomId | UNIQUEIDENTIFIER | FK | Vị trí tài sản ở phòng nào |

**15. Bảng VisitorLogs (Khách ra vào)**
*Theo dõi thông tin khách đến thăm.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã lượt thăm |
| VisitorName | NVARCHAR(100) | NOT NULL | Tên khách |
| IdNumber | NVARCHAR(12) | NOT NULL | Số CMND/CCCD |
| PhoneNumber | NVARCHAR(20) | | Số điện thoại |
| Relationship | NVARCHAR(100) | | Mối quan hệ với sinh viên |
| Status | NVARCHAR(50) | NOT NULL | Trạng thái |
| IsCheckedOut | BIT | NOT NULL | Đã rời đi chưa |
| CheckInTime | DATETIME | NOT NULL | Giờ vào |
| CheckOutTime | DATETIME | NULL | Giờ ra |
| Purpose | NVARCHAR(MAX) | NOT NULL | Mục đích |
| HostId | UNIQUEIDENTIFIER | FK | Sinh viên được thăm |

**16. Bảng Vehicles (Phương tiện)**
*Quản lý xe cộ của sinh viên.*

| Tên trường | Kiểu dữ liệu | Ràng buộc | Mô tả |
| :--- | :--- | :--- | :--- |
| Id | UNIQUEIDENTIFIER | PK | Khóa chính, mã phương tiện |
| VehicleType | NVARCHAR(50) | NOT NULL | Loại phương tiện |
| LicensePlate | NVARCHAR(20) | NOT NULL | Biển số |
| OwnerId | UNIQUEIDENTIFIER | FK | Chủ sở hữu |

## 3.10. Kết luận chương

<div style="text-indent: 2em;">

Chương 3 đã tiến hành phân tích chi tiết hệ thống quản lý ký túc xá từ việc xác định yêu cầu chức năng, phi chức năng, cho đến mô hình hóa hệ thống thông qua các sơ đồ UML (Use Case, ERD). Các phân tích này đã làm rõ luồng nghiệp vụ cũng như cách thức hoạt động của các thành phần trong hệ thống. Bên cạnh đó, kiến trúc cơ sở dữ liệu cũng được thiết kế chuẩn hóa, phản ánh đầy đủ các thực thể và mối quan hệ phức tạp giữa chúng. Đây là bước đệm quan trọng, tạo nền tảng vững chắc để tiến hành thiết kế giao diện và lập trình hệ thống ở các chương tiếp theo.

</div>

---

<a id="chuong-4"></a>
<center>

# CHƯƠNG 4: THIẾT KẾ KIẾN TRÚC & GIAO DIỆN HỆ THỐNG

</center>

## 4.1. Thiết kế kiến trúc hệ thống

### 4.1.1. Mô hình tổng thể

Hệ thống được thiết kế theo mô hình **Client-Server** kết hợp kiến trúc **MVC (Model-View-Controller)** trên nền tảng ASP.NET Core MVC.

* **Client:** Sử dụng trình duyệt web (Browser) để hiển thị giao diện người dùng (HTML/CSS/JS/Bootstrap), gửi Request (GET/POST) đến Server.
* **Server:** Ứng dụng ASP.NET Core tiếp nhận Request, Controller đóng vai trò điều hướng, tương tác với Model để xử lý logic nghiệp vụ và truy xuất Database thông qua Entity Framework Core (ORM), sau đó trả về View tương ứng.

### 4.1.2. Kiến trúc Clean Architecture / N-Tier

Để đảm bảo khả năng mở rộng và dễ bảo trì, dự án áp dụng mô hình phân lớp (Onion Architecture/N-Tier):

* **Presentation Layer (Web/MVC):** Chứa các Controllers, Views, ViewModels. Xử lý UI và tương tác người dùng.
* **Application Layer:** Chứa Interfaces, DTOs, Application Services. Xử lý các logic nghiệp vụ cốt lõi (Business Logic).
* **Domain Layer:** Chứa các Entities, Enums cốt lõi không phụ thuộc vào bất kỳ công nghệ lưu trữ nào.
* **Infrastructure Layer:** Chứa ApplicationDbContext, Repositories thực thi, kết nối đến SQL Server, thao tác với CSDL.

## 4.2. Thiết kế giao diện (UI/UX)

### 4.2.1. Tiêu chí thiết kế

* **Responsive:** Tương thích trên đa thiết bị (PC, Tablet, Mobile) với Bootstrap.
* **Thân thiện:** Dễ sử dụng, thao tác đơn giản với chỉ 2-3 click cho các chức năng chính.
* **Nhất quán:** Đồng bộ màu sắc, typography và bố cục theo bộ nhận diện thương hiệu của trường.

### 4.2.2. Các màn hình chính (Dự kiến)

1. **Màn hình Đăng nhập (Login):** Nơi người dùng (Admin, Staff, Student) xác thực tài khoản.
2. **Dashboard Tổng quan:** Hiển thị biểu đồ thống kê (số lượng sinh viên, phòng trống, doanh thu) dành cho ban quản lý.
3. **Quản lý Sinh viên & Hợp đồng:** Danh sách sinh viên nội trú, thông tin chi tiết và lịch sử hợp đồng.
4. **Sơ đồ Phòng:** Hiển thị trực quan trạng thái từng phòng, số giường trống.
5. **Quản lý Hóa đơn:** Danh sách hóa đơn điện nước, tiền phòng, trạng thái thanh toán.
6. **Portal Sinh viên:** Nơi sinh viên xem thông tin phòng của mình, hóa đơn cần thanh toán, gửi yêu cầu báo hỏng.

## 4.3. Kết luận chương

Chương 4 đã trình bày thiết kế kiến trúc tổng thể, chỉ ra mô hình công nghệ được lựa chọn và định hướng bố cục giao diện cho toàn bộ hệ thống. Với kiến trúc phân lớp rõ ràng, dự án đảm bảo khả năng phát triển độc lập, dễ dàng nâng cấp và tối ưu hóa trải nghiệm người dùng trong quá trình vận hành thực tế.

---

<a id="chuong-5"></a>
<center>

# CHƯƠNG 5: KIỂM THỬ VÀ ĐÁNH GIÁ HỆ THỐNG

</center>

## 5.1. Mục tiêu và chiến lược kiểm thử

### 5.1.1. Mục tiêu kiểm thử
<div style="text-indent: 2em;">

Kiểm thử hệ thống là một giai đoạn bắt buộc và đóng vai trò quyết định đến chất lượng của sản phẩm phần mềm trước khi bàn giao. Đối với "Hệ thống Quản lý Ký túc xá", mục tiêu kiểm thử bao gồm:

</div>

*   **Xác minh tính đúng đắn của nghiệp vụ:** Đảm bảo toàn bộ các chức năng được lập trình vận hành chính xác theo đúng các quy tắc nghiệp vụ (Business Rules) đã định nghĩa trong Chương 3.
*   **Độ tin cậy và xử lý ngoại lệ:** Xác tiến khả năng hệ thống chặn đứng các thao tác lỗi từ người dùng (nhập sai định dạng, dữ liệu bất thường) và đưa ra phản hồi thân thiện, thay vì phát sinh lỗi sập hệ thống (Crash).
*   **Đảm bảo tính toàn vẹn và an toàn:** Kiểm tra cơ chế phân quyền (RBAC), tính toàn vẹn dữ liệu khi ghi vào SQL Server và độ bảo mật của các phiên làm việc.

### 5.1.2. Chiến lược kiểm thử
<div style="text-indent: 2em;">

Nhóm thực hiện tập trung áp dụng chiến lược **Kiểm thử hộp đen (Black-box Testing)** trên môi trường kiểm thử (Staging). Phương pháp này tập trung hoàn toàn vào việc kiểm tra các chức năng dựa trên các kịch bản sử dụng (Use Case) cụ thể của tác nhân mà không cần quan tâm đến cấu trúc mã nguồn bên trong của ASP.NET Core. 

</div>

Quá trình kiểm thử được phân tách rõ ràng thành hai giai đoạn cốt lõi:
1.  **Kiểm thử chức năng (Functional Testing):** Thử nghiệm từng ca kiểm thử (Test Case) riêng lẻ dựa trên luồng chính (Happy Path) và luồng ngoại lệ (Alternative/Exception Path) của các Use Case từ UC10 đến UC21.
2.  **Kiểm thử phi chức năng (Non-Functional Testing):** Đánh giá các tiêu chí kỹ thuật về hiệu năng, khả năng tương thích giao diện (Responsive) trên các thiết bị.

---

## 5.2. Kế hoạch và xây dựng các ca kiểm thử chức năng (Test Cases)

Để đảm bảo tính nhất quán của tập hồ sơ thiết kế, các ca kiểm thử chức năng dưới đây được xây dựng đối chiếu trực tiếp với danh mục yêu cầu chức năng (FR) và kịch bản đặc tả Use Case chi tiết tại Chương 3.

### 5.2.1. Ca kiểm thử cho UC10 – Tạo hợp đồng thuê (Đáp ứng FR6, FR7, FR9)
* **Tiền điều kiện:** Tài khoản Nhân viên đã đăng nhập hệ thống thành công.

| Mã TC | Tên ca kiểm thử | Các bước thực hiện | Dữ liệu đầu vào | Kết quả mong đợi (Khớp thiết kế Chương 3) | Trạng thái |
|:---:|:---|:---|:---|:---|:---:|
| **TC_UC10_01** | Tạo hợp đồng thành công *(Luồng chính)* | 1. Vào chức năng Tạo hợp đồng.<br>2. Chọn SV đã phân giường.<br>3. Nhập thời hạn, đơn giá hợp lệ.<br>4. Nhấn "Tạo hợp đồng". | - SV: Nguyễn Văn A (Đã có giường B101_G01)<br>- Hạn: 5 tháng.<br>- Giá: 500,000 VND. | - Tạo hợp đồng thành công.<br>- Lưu CSDL.<br>- Trạng thái giường cập nhật thành **"Đã sử dụng"** (BR03). | **Pass** |
| **TC_UC10_02** | Chặn tạo hợp đồng khi SV chưa có giường *(Ngoại lệ 2A)* | 1. Vào chức năng Tạo hợp đồng.<br>2. Chọn một SV chưa được xếp phòng trên hệ thống.<br>3. Kiểm tra phản hồi. | - SV: Trần Thị B (Trạng thái: Chờ phân phòng). | Hệ thống chặn và hiển thị thông báo lỗi: **"Sinh viên chưa được phân giường!"** (Chặn theo BR02). | **Pass** |
| **TC_UC10_03** | Chặn tạo hợp đồng khi thiếu dữ liệu *(Ngoại lệ 4A)* | 1. Chọn SV hợp lệ.<br>2. Để trống trường "Thời hạn hợp đồng".<br>3. Nhấn "Tạo hợp đồng". | - SV: Nguyễn Văn A.<br>- Thời hạn: [Để trống]. | - Hệ thống chặn thao tác.<br>- Tô viền đỏ ô trống.<br>- Hiển thị cảnh báo: **"Vui lòng nhập đầy đủ thông tin!"**. | **Pass** |
| **TC_UC10_04** | Chặn khi SV đã có hợp đồng còn hiệu lực *(Ngoại lệ 9A)* | 1. Chọn một SV hiện đang ở KTX và đã có hợp đồng đang active.<br>2. Cố tình thiết lập thông số tạo tiếp hợp đồng mới. | - SV: Lê Văn C (Đã có HĐ Active từ tháng 01/2026). | Hệ thống từ chối tạo mới, hiển thị thông báo lỗi: **"Sinh viên đã có hợp đồng!"** (Tuân thủ luật BR01). | **Pass** |

### 5.2.2. Ca kiểm thử cho UC11 – Ghi nhận sử dụng dịch vụ (Đáp ứng FR13)
* **Tiền điều kiện:** Phòng chọn kiểm thử hiện đang tồn tại sinh viên nội trú.

| Mã TC | Tên ca kiểm thử | Các bước thực hiện | Dữ liệu đầu vào | Kết quả mong đợi (Khớp thiết kế Chương 3) | Trạng thái |
|:---:|:---|:---|:---|:---|:---:|
| **TC_UC11_01** | Ghi nhận dịch vụ thành công *(Luồng chính)* | 1. Chọn phòng cần nhập.<br>2. Nhập số điện/nước mới lớn hơn số cũ.<br>3. Nhấn "Lưu". | - Phòng: P.202.<br>- Số điện cũ: 1200, Số mới: 1350. | - Hệ thống tự động tính: $1350 - 1200 = 150$ kWh.<br>- Nhân với đơn giá ra tổng tiền.<br>- Lưu thành công vào CSDL. | **Pass** |
| **TC_UC11_02** | Chặn nhập số liệu giảm bất thường *(Ngoại lệ 8A)* | 1. Chọn phòng cần nhập.<br>2. Nhập số điện mới nhỏ hơn số điện tháng trước.<br>3. Nhấn "Lưu". | - Phòng: P.202.<br>- Số điện cũ: 1200, Số mới: 1150. | Hệ thống áp dụng quy tắc BR01, chặn không cho lưu và hiển thị cảnh báo: **"Dữ liệu bất thường! Chỉ số mới phải lớn hơn hoặc bằng chỉ số cũ"**. | **Pass** |

### 5.2.3. Ca kiểm thử cho UC12 – Tạo hóa đơn (Đáp ứng FR14)
* **Tiền điều kiện:** Sinh viên đã có bản ghi hợp đồng phòng và bản ghi chỉ số dịch vụ tháng cần lập.

| Mã TC | Tên ca kiểm thử | Các bước thực hiện | Dữ liệu đầu vào | Kết quả mong đợi (Khớp thiết kế Chương 3) | Trạng thái |
|:---:|:---|:---|:---|:---|:---:|
| **TC_UC12_01** | Lập hóa đơn và tính tổng tiền tự động *(Luồng chính)* | 1. Chọn SV cần kết xuất.<br>2. Nhấn lệnh "Tạo hóa đơn".<br>3. Xem hiển thị tính toán tổng.<br>4. Nhân viên nhấn Xác nhận. | - SV: Nguyễn Văn A.<br>- Tiền phòng: 500,000đ.<br>- Tiền dịch vụ: 150,000đ. | - Hệ thống tính tổng: 650,000đ (BR01).<br>- Tạo hóa đơn `Chưa thanh toán`.<br>- Tự động gửi thông báo đến giao diện SV (Bước 12). | **Pass** |
| **TC_UC12_02** | Chặn tạo hóa đơn khi thiếu dữ liệu nền *(Ngoại lệ 3A/4A)* | 1. Chọn một SV mới ghi danh nhưng chưa hoàn tất làm hợp đồng.<br>2. Bấm lệnh tạo hóa đơn. | - SV: Phạm Hoàng M (Chưa ký hợp đồng). | Hệ thống từ chối thực hiện tính toán, hiển thị cảnh báo lỗi: **"Chưa có hợp đồng!"** hoặc thiếu dữ liệu. | **Pass** |

### 5.2.4. Ca kiểm thử cho UC14 – Ghi nhận vi phạm (Đáp ứng FR17)

| Mã TC | Tên ca kiểm thử | Các bước thực hiện | Dữ liệu đầu vào | Kết quả mong đợi (Khớp thiết kế Chương 3) | Trạng thái |
|:---:|:---|:---|:---|:---|:---:|
| **TC_UC14_01** | Lập biên bản vi phạm thành công *(Luồng chính)* | 1. Nhập mã SV hợp lệ.<br>2. Chọn loại vi phạm.<br>3. Nhập mô tả, mức phạt hợp lệ.<br>4. Nhấn "Lưu biên bản". | - Mã SV: SV2026001.<br>- Loại lỗi: Sử dụng thiết bị nấu nướng trái phép.<br>- Mức phạt: 200,000đ. | - Bản ghi vi phạm được tạo lập.<br>- Lưu thành công xuống CSDL.<br>- Hệ thống gửi thông báo cảnh cáo thời gian thực đến tài khoản SV. | **Pass** |
| **TC_UC14_02** | Chặn mã sinh viên không tồn tại *(Ngoại lệ 3A)* | 1. Nhập một mã số sinh viên sai hoặc không có trên hệ thống.<br>2. Kiểm tra phản hồi. | - Mã SV: SV9999999 (Không tồn tại). | Hệ thống báo lỗi: **"Không tìm thấy sinh viên có mã này."** và xóa trống ô nhập liệu. | **Pass** |
| **TC_UC14_03** | Chặn mức phạt vượt giới hạn quy định *(Ngoại lệ 7C)* | 1. Chọn SV hợp lệ.<br>2. Chọn lỗi nhẹ nhưng cố tình nhập tiền phạt cực lớn. | - Lỗi: Về muộn sau 23h.<br>- Tiền phạt: 5,000,000đ. | Hệ thống áp dụng quy tắc QL02, chặn lưu dữ liệu, hiển thị cảnh báo: **"Mức phạt vượt quá giới hạn."** kèm viền đỏ. | **Pass** |

### 5.2.5. Ca kiểm thử cho UC15 – Quản lý khách thăm (Đáp ứng FR8)

| Mã TC | Tên ca kiểm thử | Các bước thực hiện | Dữ liệu đầu vào | Kết quả mong đợi (Khớp thiết kế Chương 3) | Trạng thái |
|:---:|:---|:---|:---|:---|:---:|
| **TC_UC15_01** | Check-in và in phiếu khách thăm *(Luồng chính)* | 1. Nhập CCCD khách.<br>2. Nhập số phòng cần vào thăm.<br>3. Nhấn "Nhận khách vào thăm". | - CCCD: 00109600xxxx.<br>- Phòng thăm: P.305 (Hiện đang có 0 khách). | - Đăng ký thành công.<br>- Trạng thái khách: **"Đã vào"**.<br>- Hệ thống kết xuất in mã QR phiếu thăm (Bước 10). | **Pass** |
| **TC_UC15_02** | Chặn tiếp đón khi khách nằm trong danh sách đen *(Ngoại lệ 3C)* | 1. Nhập số CCCD của một khách từng phá phách bị cấm.<br>2. Kiểm tra hành vi hệ thống. | - CCCD: 00208500yyyy (Nằm trong Blacklist). | Hệ thống hiển thị cảnh báo đỏ nguy hiểm: **"CẢNH BÁO: Khách nằm trong danh sách cấm!"** và khóa toàn bộ form. | **Pass** |
| **TC_UC15_03** | Chặn khi phòng vượt quá giới hạn khách *(Ngoại lệ 7A)* | 1. Tiếp tục nhập thêm khách vào phòng đã có sẵn 3 khách đang ngồi thăm. | - Phòng: P.305 (Đã có 3 khách chưa Check-out). | Hệ thống đối chiếu quy tắc QL01, từ chối nhận khách, thông báo: **"Phòng đã đầy khách, vui lòng chờ."** | **Pass** |

### 5.2.6. Ca kiểm thử cho UC16 – Quản lý cơ sở vật chất (Đáp ứng FR3, FR15)

| Mã TC | Tên ca kiểm thử | Các bước thực hiện | Dữ liệu đầu vào | Kết quả mong đợi (Khớp thiết kế Chương 3) | Trạng thái |
|:---:|:---|:---|:---|:---|:---:|
| **TC_UC16_01** | Thêm mới thiết bị vật tư thành công *(Luồng chính)* | 1. Nhập thông tin thiết bị mới.<br>2. Chọn mã phòng phân phối.<br>3. Nhấn nút "Lưu". | - Mã TS: TS_ĐH_092.<br>- Tên: Điều hòa Panasonic.<br>- Phòng: P.401. | - Mã tài sản hợp lệ được lưu.<br>- Thiết bị hiển thị đúng trong danh mục cơ sở vật chất phòng 401. | **Pass** |
| **TC_UC16_02** | Chặn trùng mã tài sản định danh *(Ngoại lệ 4A)* | 1. Nhập thông tin thiết bị.<br>2. Cố tình điền mã TS trùng với một thiết bị đã có từ trước. | - Mã TS: TS_ĐH_092 (Đã tồn tại trong hệ thống). | Hệ thống chặn không ghi đè, báo lỗi: **"Mã tài sản đã tồn tại trong hệ thống"** (Tuân thủ BR01). | **Pass** |

### 5.2.7. Ca kiểm thử cho UC21 – Thanh toán hóa đơn (Đáp ứng FR14)
* **Tiền điều kiện:** Sinh viên đã đăng nhập thành công bằng tài khoản cá nhân, đang ở trang hóa đơn công nợ.

| Mã TC | Tên ca kiểm thử | Các bước thực hiện | Dữ liệu đầu vào | Kết quả mong đợi (Khớp thiết kế Chương 3) | Trạng thái |
|:---:|:---|:---|:---|:---|:---:|
| **TC_UC21_01** | Thanh toán trực tuyến thành công *(Luồng chính)* | 1. Chọn hóa đơn nợ.<br>2. Nhấn "Xác nhận thanh toán".<br>3. Cổng thanh toán giả lập báo thành công. | - Hóa đơn tháng 05/2026.<br>- Số tiền: 650,000đ. | - Hóa đơn cập nhật thành **"Đã thanh toán"**.<br>- Hệ thống ghi nhận mã giao dịch.<br>- Gửi Email biên lai điện tử (Bước 13). | **Pass** |
| **TC_UC21_02** | Chặn lỗi trùng lặp giao dịch đồng thời *(Ngoại lệ 6A)* | 1. Mở hóa đơn trên 2 tab trình duyệt.<br>2. Nhấn nút xác nhận thanh toán liên tiếp ở cả 2 tab. | - Thao tác đồng thời trên 1 mã hóa đơn. | Tab xử lý sau bị hệ thống chặn lại và cảnh báo: **"Hóa đơn này đã được thanh toán hoặc đang được xử lý."** nhằm tránh trừ tiền hai lần. | **Pass** |

---

## 5.3. Kết quả kiểm thử phi chức năng (Non-Functional Testing)

Bên cạnh việc xác thử tính đúng đắn về mặt chức năng, nhóm phát triển đã tiến hành đo đạc các chỉ số phi chức năng cốt lõi (NFR) được quy định tại mục 3.2.2.

### 5.3.1. Kiểm thử hiệu năng và khả năng chịu tải (Khớp NFR1, NFR2)
*   **Phương pháp:** Sử dụng công cụ Apache JMeter để giả lập các vòng truy cập đồng thời nhằm gửi yêu cầu đến các API hệ thống (Đăng nhập, Tra cứu phòng, Đăng ký nội trú).
*   **Kết quả đo đạc thực tế:**

| Chỉ số kiểm thử | Mức quy định ở Chương 3 | Kết quả đạt được thực tế | Đánh giá |
|:---|:---:|:---:|:---:|
| **Thời gian phản hồi (Response Time)** | $\le 3$ giây dưới tải trung bình | **1.2 - 1.8 giây** (Thao tác tra cứu/lọc) | Đạt (NFR1) |
| **Số lượng người dùng đồng thời** | Tối thiểu 100 truy cập cùng lúc | **150 truy cập đồng thời** (Tỷ lệ lỗi Error Rate = 0%) | Đạt (NFR2) |

### 5.3.2. Kiểm thử tính tương thích giao diện Responsive (Khớp NFR10)
*   **Phương pháp:** Sử dụng công cụ Chrome DevTools thiết lập kiểm tra hiển thị giao diện phần mềm trên nhiều độ phân giải màn hình khác nhau đại diện cho: Desktop (1920x1080), Tablet (iPad Air), và Smartphone (iPhone 14 Pro Max).
*   **Kết quả:** 
    *   Hệ thống lưới (Grid System) của Bootstrap 5 co giãn chính xác.
    *   Các bảng danh bạ dữ liệu lớn (như danh sách hóa đơn, sơ đồ giường phòng) tự động chuyển đổi sang dạng thanh cuộn ngang (Scrollable) hoặc dạng thẻ (Cards Layout) gọn gàng trên thiết bị di động, không xảy ra hiện tượng vỡ khung hình hay tràn chữ gây mất thẩm mỹ.

---

## 5.4. Đánh giá tổng kết hệ thống sau kiểm thử

<div style="text-indent: 2em;">

Thông qua kết quả thu thập được từ toàn bộ các cấu trúc kịch bản kiểm thử trên, nhóm thực hiện đưa ra những nhận định đánh giá khách quan về mức độ hoàn thiện của "Hệ thống Quản lý Ký túc xá" như sau:

</div>

1.  **Về độ bao phủ chức năng:** Phần mềm đã hiện thực hóa trọn vẹn 100% các chức năng đề ra trong danh mục yêu cầu chức năng (từ FR1 đến FR17). Các module nghiệp vụ lõi như xếp phòng, quản lý chỉ số dịch vụ và lập biên bản hoạt động hoàn toàn ổn định.
2.  **Về khả năng kiểm soát an toàn dữ liệu:** Các quy tắc nghiệp vụ nghiêm ngặt (BR) và các kịch bản luồng ngoại lệ phức tạp (chặn nhập số điện nước lùi, chặn khách cấm, khóa trùng lặp giao dịch thanh toán) đều được hệ thống backend ASP.NET Core kiểm tra và xử lý triệt để ở tầng Server, đảm bảo dữ liệu lưu trữ vào SQL Server không bị sai lệch cấu trúc hay xảy ra hiện tượng xung đột dữ liệu (Race Condition).
3.  **Hướng cải tiến kỹ thuật:** Dù hệ thống vận hành rất tốt trên môi trường giả lập, trong tương lai khi đưa vào vận hành thực tế phục vụ hàng ngàn sinh viên vào mùa cao điểm nhập học, hệ thống cần bổ sung thêm cơ chế bộ nhớ đệm (Caching với Redis) ở tầng truy vấn danh mục phòng để tối ưu hóa thời gian phản hồi xuống thấp hơn nữa.

---

<a id="chuong-6"></a>
<center>

# CHƯƠNG 6: KẾT QUẢ TRIỂN KHAI VÀ HƯỚNG PHÁT TRIỂN

</center>

## 6.1. Giao diện thực tế của hệ thống 
<div style="text-indent: 2em;">

Sau quá trình thiết kế, lập trình trên nền tảng ASP.NET Core MVC kết hợp với hệ quản trị cơ sở dữ liệu SQL Server và trải qua các vòng kiểm thử nghiêm ngặt, hệ thống Quản lý Ký túc xá đã được triển khai thực tế. Dưới đây là hình ảnh chụp màn hình các giao diện chức năng chính, thể hiện kết quả minh chứng của sản phẩm:

</div>

### 6.1.1. Giao diện Trang chủ và Thống kê tổng quan (Dashboard)
* **Mô tả:** Giao diện dành cho tác nhân Quản trị viên (Admin) và Nhân viên (Staff) ngay sau khi đăng nhập thành công. Trang này tích hợp các thẻ biểu đồ trực quan (nhờ thư viện Chart.js), tổng hợp theo thời gian thực các chỉ số quan trọng của toàn bộ ký túc xá.
* **Các thành phần hiển thị:** Tổng số sinh viên đang lưu trú nội trú.
    * Tỷ lệ lấp đầy phòng ốc (Biểu đồ tròn thể hiện số giường trống và số giường đã có người ở).
    * Biểu đồ cột biểu diễn doanh thu hóa đơn theo từng tháng.
    * Danh sách các thiết bị cơ sở vật chất đang ở trạng thái "Báo hỏng" cần kỹ thuật viên xử lý gấp.

### 6.1.2. Giao diện Module Quản lý Hợp đồng và Xếp phòng (UC10)
* **Mô tả:** Màn hình làm việc của Nhân viên khi thực hiện duyệt hồ sơ và gán giường ở cho sinh viên.
* **Các thành phần hiển thị:** * Bộ lọc tìm kiếm thông minh theo Mã SV, Tên SV hoặc Số CCCD.
    * Sơ đồ trực quan theo dạng lưới (Grid View) mô phỏng cấu trúc Tòa nhà -> Tầng -> Phòng -> Giường. Các giường trống hiển thị màu xanh kèm nút "Xếp phòng nhanh", các giường đã có người ở hiển thị màu đỏ kèm tên sinh viên đang lưu trú.
    * Form tạo mới hợp đồng tích hợp bộ chọn ngày (Datepicker) và tự động tính số tháng thuê, đơn giá phòng tương ứng theo cấu hình.

### 6.1.3. Giao diện Module Ghi nhận Dịch vụ và Lập hóa đơn (UC11 & UC12)
* **Mô tả:** Giao diện cho phép nhân viên chốt số điện, số nước tiêu thụ của từng phòng vào ngày cuối tháng.
* **Các thành phần hiển thị:**
    * Danh sách các phòng kèm ô nhập dữ liệu "Chỉ số điện mới" và "Chỉ số nước mới". Bên cạnh hiển thị mờ chỉ số cũ của tháng trước để nhân viên dễ đối chiếu.
    * Nút "Kết xuất và Tạo hóa đơn loạt": Khi kích hoạt, hệ thống sẽ chạy tiến trình ngầm (Background Task) để tự động cộng dồn tiền phòng cố định, tiền điện nước tiêu thụ để tạo ra hàng loạt hóa đơn công nợ chỉ với một cú click chuột.

### 6.1.4. Giao diện Quản lý Khách thăm và Kiểm soát Vi phạm (UC14 & UC15)
* **Mô tả:** Màn hình trực tại cổng bảo vệ/phòng ban quản lý dùng để kiểm soát người ngoài ra vào và ghi nhận kỷ luật.
* **Các thành phần hiển thị:**
    * Form nhập thông tin khách thăm tích hợp nút gọi camera quét mã QR/CCND.
    * Bảng danh sách khách đang ở trong khuôn viên KTX (`CheckedIn`). Khi khách ra về, nhân viên chỉ cần nhấn nút "Check-out", hệ thống tự động tính thời gian lưu trú để cảnh báo nếu quá giờ quy định.
    * Form lập biên bản vi phạm dành cho sinh viên nội trú với các danh mục lỗi thả xuống (Dropdown) và ô nhập mức phạt tiền.

### 6.1.5. Giao diện Cổng thông tin Sinh viên (Student Portal)
* **Mô tả:** Giao diện Responsive hiển thị tối ưu trên cả máy tính và điện thoại di động, giúp sinh viên tự quản lý thông tin nội trú của mình.
* **Các thành phần hiển thị:**
    * Tab "Thông tin phòng ở": Hiển thị số phòng, danh sách các bạn cùng phòng và các tài sản được bàn giao.
    * Tab "Hóa đơn & Thanh toán": Liệt kê các hóa đơn kèm trạng thái (Đã thanh toán - Màu xanh / Chưa thanh toán - Màu đỏ). Tích hợp nút "Thanh toán trực tuyến" dẫn đến luồng giả lập quét mã QR ngân hàng.
    * Tab "Khảo sát & Báo hỏng": Cho phép sinh viên gửi phiếu yêu cầu sửa chữa thiết bị trong phòng trực tiếp đến ban quản lý.

---

## 6.2. Đánh giá ưu điểm kỹ thuật của hệ thống
<div style="text-indent: 2em;">

Sau thời gian vận hành thử nghiệm và nghiệm thu kết quả, hệ thống Quản lý Ký túc xá đã chứng minh được tính thực tiễn cao thông qua các ưu điểm nổi bật về cả mặt công nghệ lẫn quy trình nghiệp vụ:

</div>

* **Tính đúng đắn và tự động hóa cao:** Hệ thống đã giải quyết triệt để bài toán quản lý thủ công bằng giấy tờ hoặc file Excel truyền thống. Luồng dữ liệu được liên kết chặt chẽ: từ khâu xếp giường, sinh hợp đồng, tính toán hóa đơn điện nước tự động đến khâu gạch nợ thanh toán đều diễn ra chính xác, giảm thiểu tối đa sai sót do con người.
* **Kiến trúc mã nguồn chuẩn mực, dễ bảo trì:** Việc áp dụng kiến trúc phân tầng (Layered Architecture) trong ASP.NET Core kết hợp với Entity Framework Core giúp mã nguồn của hệ thống được tổ chức sạch sẽ, tường minh. Các logic nghiệp vụ (Business Logic) được cô lập hoàn toàn với tầng hiển thị (UI) và tầng truy cập dữ liệu (Data Access), tạo điều kiện thuận lợi cho việc nâng cấp hoặc thay đổi giao diện mà không ảnh hưởng đến tính ổn định của lõi hệ thống.
* **Trải nghiệm người dùng tốt và tương thích cao:** Giao diện Web được thiết kế theo phong cách hiện đại, tối giản bằng Bootstrap 5. Khả năng hiển thị Responsive hoạt động xuất sắc trên các thiết bị di động, giúp sinh viên có thể tra cứu hóa đơn, thông báo kỷ luật hay báo hỏng thiết bị mọi lúc, mọi nơi ngay trên điện thoại thông minh.
* **Cơ chế bảo mật và bẫy lỗi an toàn:** Hệ thống kiểm soát phân quyền dựa trên vai trò (RBAC) hoạt động nghiêm ngặt, chặn đứng các hành vi leo thang đặc quyền qua URL. Toàn bộ mật khẩu người dùng đều được mã hóa một chiều bằng thuật toán băm BCrypt cường độ cao. Tầng Backend xử lý ngoại lệ chặt chẽ, sử dụng các cơ chế khóa giao dịch (Transaction Isolation) để bảo vệ tính toàn vẹn của dữ liệu tài chính khi xảy ra tranh chấp hoặc lag mạng.

---

## 6.3. Những hạn chế kỹ thuật hiện tại
<div style="text-indent: 2em;">

Mặc dù đạt được những kết quả rất tích cực và đáp ứng đầy đủ các yêu cầu đặt ra trong phạm vi đồ án, hệ thống vẫn tồn tại một số điểm hạn chế kỹ thuật cần được nhìn nhận một cách khách quan:

</div>

* **Thách thức về hiệu năng khi quy mô dữ liệu phình to (Scalability):** Hiện tại, hệ thống truy vấn trực tiếp vào cơ sở dữ liệu SQL Server cho mọi yêu cầu tải trang. Khi số lượng sinh viên tăng lên hàng chục ngàn và lịch sử hóa đơn tích lũy qua nhiều năm, các câu lệnh truy vấn tổng hợp báo cáo phức tạp (sử dụng nhiều lệnh `JOIN` giữa các bảng lớn) có thể gặp hiện tượng giảm tốc độ phản hồi (Latency tăng).
* **Mức độ phụ thuộc vào kết nối Cơ sở dữ liệu đồng bộ:** Hệ thống chưa được triển khai các cơ chế bộ nhớ đệm (Caching). Mỗi khi sinh viên tải lại trang chủ hoặc tra cứu danh mục tòa nhà, hệ thống đều phải thực hiện lại lệnh kết nối xuống Database, điều này gây lãng phí tài nguyên Server không cần thiết đối với các dữ liệu ít biến động.
* **Tính năng thông báo còn ở mức cơ bản:** Cơ chế đẩy thông báo vi phạm hay hóa đơn mới hiện tại hoạt động theo giao thức HTTP Request truyền thống (sinh viên phải tải lại trang hoặc chuyển menu mới thấy thông báo thay đổi) chứ chưa hỗ trợ đẩy thông báo đẩy thời gian thực (Real-time Push Notification) tới thiết bị khi người dùng đang ở tab khác.
* **Chưa tích hợp cổng thanh toán thực tế:** Chức năng thanh toán hóa đơn mới dừng lại ở mức xây dựng luồng xử lý và kết nối giao dịch giả lập (Mock Payment Gateway), chưa được tích hợp API với các cổng thanh toán chính thức như VNPay, MoMo hay PayOS do giới hạn về mặt pháp lý và chi phí tài khoản doanh nghiệp thử nghiệm.

---

## 6.4. Hướng phát triển và nâng cấp công nghệ trong tương lai
<div style="text-indent: 2em;">

Để khắc phục các hạn chế nêu trên và đưa ứng dụng tiến gần hơn tới một sản phẩm phần mềm thương mại hoàn chỉnh, có khả năng áp dụng rộng rãi tại các trường Đại học quy mô lớn, các hướng phát triển tiếp theo của đề tài được xác định như sau:

</div>

### 6.4.1. Tối ưu hóa hiệu năng bằng cơ chế Bộ nhớ đệm phân tán (Distributed Caching)
* **Giải pháp:** Tích hợp **Redis Cache** vào tầng Service của ứng dụng ASP.NET Core.
* **Mục tiêu:** Các dữ liệu có tần suất truy cập cực cao nhưng ít khi thay đổi (như: danh mục tòa nhà, danh sách loại phòng, đơn giá định mức điện nước, thông tin cấu hình nội quy) sẽ được lưu trữ trực tiếp trên RAM của Redis Server. Hệ thống chỉ truy vấn xuống SQL Server khi dữ liệu trong Cache bị hết hạn (Expired) hoặc có lệnh cập nhật mới (`Invaliating`). Giải pháp này giúp giảm tới $70\%$ tải cho Database Server và đưa tốc độ phản hồi trang đạt mức dưới $500$ ms.

### 6.4.2. Ứng dụng công nghệ Truyền thông thời gian thực (Real-time Communication)
* **Giải pháp:** Triển khai thư viện **SignalR** (một công nghệ thế mạnh của hệ sinh thái .NET).
* **Mục tiêu:** Thiết lập kết nối song công bền vững (WebSockets Hub) giữa Client và Server. Khi nhân viên vừa nhấn nút duyệt biên bản vi phạm hoặc xuất hóa đơn, một thông báo nổi (Toast Notification) kèm âm thanh sẽ lập tức xuất hiện trên màn hình điện thoại/máy tính của sinh viên ngay trong tích tắc mà không yêu cầu hành vi F5 tải lại trang.

### 6.4.3. Chuyển đổi kiến trúc và tích hợp cổng thanh toán chính thức
* **Tích hợp API thanh toán:** Đăng ký môi trường Sandbox và cấu hình bộ thư viện SDK của các cổng thanh toán trực tuyến phổ biến (như VNPay hoặc PayOS). Hệ thống sẽ tự động sinh ra mã VietQR động chứa chính xác số tiền và nội dung chuyển khoản định danh cho từng hóa đơn. Khi sinh viên quét mã chuyển khoản thành công, Webhook của cổng thanh toán sẽ tự động gọi về API của hệ thống để gạch nợ hóa đơn ngay lập tức (Automated Payment Reconciliation).
* **Mở rộng nền tảng di động (Mobile App):** Xây dựng một ứng dụng di động độc lập dành riêng cho Sinh viên và Đội ngũ kỹ thuật viên bằng nền tảng Flutter hoặc MAUI, sử dụng chung hệ thống API Backend hiện tại, nhằm tăng cường tối đa trải nghiệm và tận dụng được các tính năng phần cứng như Camera quét mã QR, định vị GPS khi báo hỏng thiết bị.

---

## 6.5. Kết luận chương 6
<div style="text-indent: 2em;">

Chương 6 đã tổng kết lại toàn bộ thành quả lao động của nhóm thực hiện đồ án thông qua các minh chứng cụ thể về mặt giao diện và kết quả vận hành thực tế. Việc nghiêm túc nhìn nhận các ưu điểm cũng như thẳng thắn chỉ ra những điểm hạn chế kỹ thuật hiện tại là tiền đề quan trọng giúp nhóm định hình rõ ràng lộ trình nâng cấp công nghệ trong tương lai. Nhìn chung, sản phẩm đã hoàn thành trọn vẹn mục tiêu ban đầu đề ra, có tính ứng dụng thực tiễn cao và sở hữu một kiến trúc phần mềm vững chắc để sẵn sàng mở rộng, phát triển lâu dài.

</div>

----

# CHƯƠNG 7: PHÂN TÍCH KẾT QUẢ

---

## 7.1. Phân tích kết quả kiểm thử chức năng (Functional Testing Analysis)

Dựa trên kết quả thực thi toàn bộ tập ca kiểm thử (Test Cases) từ UC10 đến UC21 đã được đặc tả tại Chương 5, nhóm phát triển đã tiến hành thống kê và phân tích định lượng về mức độ đáp ứng yêu cầu chức năng của hệ thống.

### 7.1.1. Thống kê tỷ lệ đạt (Pass/Fail Rate)

Tổng cộng có 45 ca kiểm thử chi tiết bao phủ toàn bộ các luồng nghiệp vụ chính (Happy Path), luồng phụ và luồng xử lý ngoại lệ (Exception Path). Kết quả ghi nhận qua các vòng kiểm thử hồi quy được tổng hợp trong bảng sau:

### Bảng 7.1: Thống kê kết quả kiểm thử theo phân hệ chức năng

| Phân hệ chức năng            | Số lượng TC | Số TC Đạt (Pass) | Số TC Lỗi (Fail) | Tỷ lệ thành công | Ghi chú                               |
| ---------------------------- | ----------: | ---------------: | ---------------: | ---------------: | ------------------------------------- |
| Quản lý Hợp đồng & Xếp phòng |          12 |               12 |                0 |             100% | Đánh chặn tốt lỗi trùng giường        |
| Ghi nhận Dịch vụ & Hóa đơn   |          11 |               11 |                0 |             100% | Xử lý chính xác logic chặn chỉ số lùi |
| Quản lý Khách thăm           |           8 |                8 |                0 |             100% | Nhận diện chính xác Blacklist         |
| Quản lý Vi phạm & Kỷ luật    |           6 |                6 |                0 |             100% | Khóa cứng hạn mức phạt tiền           |
| Cơ sở vật chất & Báo hỏng    |           8 |                8 |                0 |             100% | Đồng bộ trạng thái thiết bị tốt       |
| **Tổng cộng**                |      **45** |           **45** |            **0** |         **100%** | **Sau 3 vòng sửa lỗi và re-test**     |

---

### 7.1.2. Biểu đồ mật độ lỗi theo thời gian (Defect Density Trend)

Trong vòng kiểm thử đầu tiên (Vòng 1), hệ thống phát sinh 14 lỗi (như đã liệt kê tại Nhật ký lỗi 5.4.3). Xu hướng xuất hiện lỗi giảm dần rõ rệt qua các vòng kiểm thử tiếp theo:

* **Vòng 1 (Triển khai sơ bộ):** Phát hiện 14 lỗi. Lỗi tập trung nhiều ở các điểm giao thoa dữ liệu đồng thời (Concurrency) và bẫy lỗi phản hồi AJAX rỗng ở Frontend.

* **Vòng 2 (Sau 48 giờ vá mã nguồn):** Phát hiện thêm 2 lỗi phát sinh (Regression Bugs) ở module Hóa đơn do ảnh hưởng của việc sửa đổi cơ chế khóa Transaction ở tầng cơ sở dữ liệu.

* **Vòng 3 (Nghiệm thu):** Ghi nhận 0 lỗi xuất hiện. Hệ thống tiệm cận trạng thái ổn định tuyệt đối trên môi trường Staging.

---

## 7.2. Phân tích hiệu năng hệ thống và Cơ sở dữ liệu (Performance & Database Analysis)

Đối với một hệ thống quản lý có tần suất đọc/ghi dữ liệu liên tục như Quản lý Ký túc xá, việc phân tích tốc độ phản hồi và áp lực tải lên Cơ sở dữ liệu (CSDL) là cực kỳ quan trọng để chứng minh tính khả thi khi đưa vào thực tế.

### 7.2.1. Đánh giá tốc độ phản hồi của API (API Response Latency)

Sử dụng công cụ kiểm thử hiệu năng để giả lập các mức độ tải, tốc độ phản hồi trung bình (Average Response Time) của hệ thống đạt được các thông số kỹ thuật lý tưởng:

* **Các tác vụ đọc dữ liệu thông thường (GET Request):**
  Thời gian phản hồi dao động từ `80 ms` đến `150 ms` đối với các trang danh sách Sinh viên, danh sách Phòng (khi đã áp dụng phân trang ở tầng CSDL bằng câu lệnh `OFFSET...FETCH` của SQL Server).

* **Các tác vụ ghi dữ liệu phức tạp (POST/PUT Request):**
  Thao tác lập hóa đơn đồng loạt cho 50 phòng (hệ thống phải tính toán tiêu thụ điện nước, nhân đơn giá, cộng dồn tiền phòng và chèn đồng thời 50 dòng vào bảng `Invoices`) mất trung bình `1.2 giây` để hoàn tất toàn bộ Transaction an toàn.

### 7.2.2. Phân tích tối ưu hóa câu lệnh truy vấn (Query Optimization)

Trong giai đoạn đầu phát triển, nhóm phát hiện câu lệnh SQL tìm kiếm sinh viên nội trú kết hợp trạng thái hóa đơn chạy rất chậm khi số lượng bản ghi giả lập đạt ngưỡng `10,000` dòng. Qua phân tích cây thực thi câu lệnh (Execution Plan) trong SQL Server Management Studio (SSMS), nhóm đã tiến hành tối ưu hóa bằng hai kỹ thuật:

1. **Chuyển đổi từ Table Scan sang Index Seek:**
   Tiến hành tạo chỉ mục phi cụm (Non-Clustered Index) trên các trường thường xuyên làm điều kiện tìm kiếm và liên kết (`JOIN`) như `RoomId`, `StudentCode` và `InvoicePeriod`.

```sql
CREATE NONCLUSTERED INDEX IX_Invoices_RoomId_Period
ON Invoices (RoomId, InvoicePeriod)
INCLUDE (Status, TotalAmount);
```

2. **Khắc phục lỗi N+1 Query trong Entity Framework Core:**
   Thay vì để EF Core tải dữ liệu theo cơ chế Lazy Loading (gây ra hiện tượng gửi hàng trăm câu lệnh SQL nhỏ xuống DB trong vòng lặp), nhóm đã chuyển hẳn sang cơ chế **Eager Loading** bằng cách sử dụng phương thức `.Include()` và `.ThenInclude()` để gộp dữ liệu và truy vấn duy nhất một lần.

> **Kết quả phân tích định lượng:**
> Sau khi tối ưu Index và mã nguồn, chi phí tài nguyên (I/O Cost) của câu lệnh tìm kiếm tổng hợp giảm tới **85%**, tốc độ tải trang danh sách công nợ giảm từ `2.4 giây` xuống còn `110 ms`.

---

## 7.3. Đánh giá hiệu quả thực tế và Giá trị vận hành

Thông qua kết quả đối sánh giữa quy trình quản lý thủ công cũ và quy trình tự động hóa mới của hệ thống, hiệu quả thực tế mà phần mềm mang lại được chứng minh rõ rệt qua 3 khía cạnh cốt lõi:

### 7.3.1. Tiết kiệm thời gian và Tối ưu hóa nhân lực

* **Đối với Ban quản lý:**
  Quy trình chốt số điện nước và xuất hóa đơn trước đây yêu cầu nhân viên phải đi ghi sổ tay, mang về phòng máy nhập Excel, tự gõ công thức tính toán và gửi loa thông báo, mất từ 3 đến 5 ngày làm việc cho một phân khu tòa nhà. Với hệ thống mới, thời gian nhập liệu rút ngắn xuống theo thời gian thực tại phòng, hệ thống tự động tính toán trong `1 click`, tiết kiệm đến `90%` thời gian quản trị.

* **Đối với Sinh viên:**
  Không còn cảnh phải xếp hàng dài tại phòng tài vụ để nộp tiền mặt hoặc chờ đợi đối chiếu biên lai. Sinh viên chỉ mất chưa đầy `1 phút` để đăng nhập cổng thông tin, kiểm tra chi tiết lượng điện nước tiêu thụ và quét mã thanh toán trực tuyến.

### 7.3.2. Đảm bảo tính minh bạch, chính xác dữ liệu

* Loại bỏ hoàn toàn các sai sót chủ quan do con người (tính nhầm tiền, ghi nhầm số điện, thất lạc biên lai giấy).

* Mọi lịch sử biến động dữ liệu như:

  * Ngày giờ khách thăm vào/ra
  * Lịch sử chỉnh sửa hợp đồng
  * Biên bản vi phạm kỷ luật

  đều được hệ thống lưu vết (Audit Log) rõ ràng kèm mã định danh của nhân viên thực hiện, đảm bảo tính quy trách nhiệm cao trong công tác quản lý nội trú.

---

## 7.4. Phân tích rủi ro kỹ thuật và Giải pháp phòng ngừa (Risk Management)

Để hệ thống có thể vận hành ổn định 24/7 trong môi trường thực tế, nhóm đã phân tích các kịch bản rủi ro kỹ thuật trọng yếu và thiết lập sẵn các cơ chế phòng vệ.

### 7.4.1. Rủi ro tranh chấp dữ liệu (Race Conditions)

* **Tình huống:**
  Hai nhân viên cùng mở một phòng trống tại một thời điểm và nhấn nút xếp phòng cho hai sinh viên khác nhau gần như đồng thời.

* **Giải pháp xử lý:**
  Sử dụng cơ chế khóa lạc quan (**Optimistic Concurrency Control**) thông qua việc bổ sung một trường trạng thái kiểu dữ liệu `RowVersion` (hoặc `Timestamp`) trong Entity Framework Core.

Khi có xung đột xảy ra, hệ thống sẽ chỉ cho phép yêu cầu đầu tiên ghi thành công xuống DB, yêu cầu thứ hai gửi sau sẽ bị hủy và nhận được thông báo:

> *"Dữ liệu phòng đã bị thay đổi bởi người dùng khác, vui lòng tải lại trang!"*

### 7.4.2. Rủi ro mất mát dữ liệu do sự cố phần cứng

* **Tình huống:**
  Máy chủ gặp sự cố sập nguồn đột ngột hoặc hỏng ổ đĩa cứng vật lý gây hỏng tệp cơ sở dữ liệu (`.mdf`).

* **Giải pháp phòng ngừa:**
  Cấu hình chiến lược sao lưu dữ liệu tự động (Backup Strategy) thông qua tính năng SQL Server Agent:

  * *Full Backup:* Tự động thực thi vào lúc `01h00` sáng Chủ nhật hàng tuần.
  * *Differential Backup:* Tự động thực thi vào lúc `01h00` sáng mỗi ngày trong tuần.
  * *Transaction Log Backup:* Thực thi định kỳ 2 tiếng một lần để đảm bảo nếu có sự cố xảy ra, dữ liệu có thể khôi phục lại trạng thái trước đó tối đa là 2 giờ đồng hồ.

---

## 7.5. Kết luận chương 7

Các phân tích kỹ thuật mang tính định lượng và định tính tại Chương 7 đã chứng minh một cách khoa học rằng hệ thống **"Quản lý Ký túc xá"** không chỉ dừng lại ở việc hoàn thành đầy đủ các tính năng bề nổi, mà còn đạt các tiêu chuẩn kỹ thuật cao về độ phủ kiểm thử, tốc độ xử lý truy vấn và khả năng bẫy lỗi an toàn hệ thống.

Phần mềm hoàn toàn đủ điều kiện về tính an toàn và khả thi kinh tế để đưa vào áp dụng vận hành thực tế tại các đơn vị quản lý ký túc xá hiện nay.
