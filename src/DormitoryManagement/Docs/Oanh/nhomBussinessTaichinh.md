# 📋 BÁO CÁO UML - NHÓM BUSINESS & TÀI CHÍNH  
## Thành viên thực hiện: Vũ Thị Kim Oanh 

---

## 📌 MỤC LỤC
1. [UC10: Tạo hợp đồng thuê](#1-uc10-tạo-hợp-đồng-thuê)  
2. [UC11: Ghi nhận sử dụng dịch vụ](#2-uc11-ghi-nhận-sử-dụng-dịch-vụ)  
3. [UC12: Tạo hóa đơn](#3-uc12-tạo-hóa-đơn)  

---


## 1. UC10 – TẠO HỢP ĐỒNG

### 1.1. Use case diagram 

![UC10 - TẠO HỢP ĐỒNG](../images/UC10_TaoHopDong.png)

### 1.2. Đặc tả Use case 

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
| Luồng chính | 1. Nhân viên truy cập chức năng tạo hợp đồng<br/>2. Chọn sinh viên đã được phân giường<br/>3. Hệ thống hiển thị thông tin sinh viên và phòng<br/>4. Nhân viên nhập thông tin hợp đồng (thời hạn, giá, ngày bắt đầu...)<br/>5. Kiểm tra dữ liệu phía giao diện<br/>6. Nhân viên nhấn “Tạo hợp đồng”<br/>7. Gửi yêu cầu lên hệ thống<br/>8. Hệ thống kiểm tra dữ liệu<br/>9. Kiểm tra điều kiện (chưa có hợp đồng còn hiệu lực, đã có giường)<br/>10. Tạo hợp đồng<br/>11. Lưu vào cơ sở dữ liệu<br/>12. Cập nhật trạng thái giường = “Đã sử dụng”<br/>13. Ghi nhật ký<br/>14. Trả kết quả<br/>15. Hiển thị thông báo thành công |
| Luồng ngoại lệ | **2A. Sinh viên chưa có giường**<br/>1. Tại bước 2 hệ thống kiểm tra dữ liệu<br/>2. Điều kiện: sinh viên chưa được phân giường<br/>3. Hiển thị “Sinh viên chưa được phân giường!”<br/>4. Dừng<br/><br/>**4A. Thiếu thông tin**<br/>1. Tại bước 4 nhập thiếu dữ liệu<br/>2. Bước 5 kiểm tra phát hiện lỗi<br/>3. Làm nổi bật các trường thiếu<br/>4. Hiển thị “Vui lòng nhập đầy đủ thông tin!”<br/>5. Quay lại bước 4<br/><br/>**5A. Dữ liệu không hợp lệ**<br/>1. Sai định dạng (ngày, giá...)<br/>2. Hệ thống chặn thao tác<br/>3. Hiển thị lỗi chi tiết<br/>4. Quay lại bước 4<br/><br/>**9A. Đã có hợp đồng còn hiệu lực**<br/>1. Hệ thống kiểm tra đã tồn tại hợp đồng còn hiệu lực<br/>2. Từ chối tạo mới<br/>3. Trả lỗi<br/>4. Hiển thị “Sinh viên đã có hợp đồng!”<br/>5. Kết thúc<br/><br/>**11A. Lỗi cơ sở dữ liệu**<br/>1. Lỗi khi lưu dữ liệu<br/>2. Khôi phục trạng thái trước đó (nếu có)<br/>3. Trả lỗi hệ thống<br/>4. Hiển thị “Có lỗi xảy ra, vui lòng thử lại sau”<br/>5. Ghi nhật ký lỗi |
| Quy tắc nghiệp vụ | BR01: Mỗi sinh viên chỉ có 1 hợp đồng còn hiệu lực<br/>BR02: Chỉ tạo hợp đồng khi đã được phân giường<br/>BR03: Tạo hợp đồng phải cập nhật trạng thái giường |

### 1.3. Activity Diagram - AD04

![AD04 - TẠO HỢP ĐỒNG](../images/AD04_TaoHopDong.png)

### 1.4. Sequence Diagram - SD04

![SD04 - TẠO HỢP ĐỒNG](../images/SD04_TaoHopDong.png)

---

## 2. UC11 – GHI NHẬN DỊCH VỤ

### 2.1. Use Case Diagram 

![UC11 - GHI NHẬN DỊCH VỤ](../images/UC11_GhiNhanDichVu.png)

### 2.2. Đặc tả Use Case

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

### 2.3. Activity Diagram - AD05

![AD05 - GHI NHẬN DỊCH VỤ](../images/AD05_GhiNhanDichVu.png)

### 2.4. Sequence Diagram - SD05

![SD05 - GHI NHẬN DỊCH VỤ](../images/SD05_GhiNhanDichVu.png)

---

## 3. UC12 – TẠO HÓA ĐƠN

### 3.1. Use Case Diagram

![UC12 - TẠO HÓA ĐƠN](../images/UC12_TaoHoaDon.png)

### 3.2. Đặc tả Use Case 

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

### 3.3. Activity Diagram

![AD06 - TẠO HÓA ĐƠN](../images/AD06_TaoHoaDon.png)

### 3.4. Sequence Diagram

![SD06 - TẠO HÓA ĐƠN](../images/SD06_TaoHoaDon.png)

---
