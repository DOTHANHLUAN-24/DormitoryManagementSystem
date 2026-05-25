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
6. [Chương 1: Cơ sở lý thuyết](#chuong-1-co-so-ly-thuyet)

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

### 1.7.1. Trạng thái phòng

* Available
* Full
* Maintenance

### 1.7.2. Trạng thái hợp đồng

* Active
* Expired
* Terminated

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

<center>

![Sơ đồ ca sử dụng tổng quát hệ thống](./images/UML/UC/UCTQ.png)

<b>Hình 3.1 – Sơ đồ ca sử dụng tổng quát hệ thống</b>
</center>

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

*(Các bảng khác như Blocks, RoomTypes, Utilities, UtilityUsages, Payments, Assets, VisitorLogs, Vehicles được thiết kế tương tự với các ràng buộc khóa ngoại chặt chẽ để duy trì tính toàn vẹn tham chiếu).*

## 3.10. Kết luận chương

<div style="text-indent: 2em;">

Chương 3 đã tiến hành phân tích chi tiết hệ thống quản lý ký túc xá từ việc xác định yêu cầu chức năng, phi chức năng, cho đến mô hình hóa hệ thống thông qua các sơ đồ UML (Use Case, ERD). Các phân tích này đã làm rõ luồng nghiệp vụ cũng như cách thức hoạt động của các thành phần trong hệ thống. Bên cạnh đó, kiến trúc cơ sở dữ liệu cũng được thiết kế chuẩn hóa, phản ánh đầy đủ các thực thể và mối quan hệ phức tạp giữa chúng. Đây là bước đệm quan trọng, tạo nền tảng vững chắc để tiến hành thiết kế giao diện và lập trình hệ thống ở các chương tiếp theo.

</div>

---

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
