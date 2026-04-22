# 📋 BÁO CÁO UML - NHÓM KỸ THUẬT & HỆ THỐNG PHỤ TRỢ
## Thành viên thực hiện: LÊ THỊ CẨM TÚ

---

## 📌 MỤC LỤC
1. [UC16: Quản lý cơ sở vật chất](#1-uc16-quản-lý-cơ-sở-vật-chất)
2. [UC22: Tạo yêu cầu sửa chữa](#2-uc22-tạo-yêu-cầu-sửa-chữa)
3. [UC24: Tiếp nhận yêu cầu sửa chữa](#3-uc24-tiếp-nhận-yêu-cầu-sửa-chữa)
4. [UC25: Xử lý sự cố kỹ thuật](#4-uc25-xử-lý-sự-cố-kỹ-thuật)
5. [UC26: Cập nhật trạng thái sửa chữa](#5-uc26-cập-nhật-trạng-thái-sửa-chữa)

---

# 1. UC16: QUẢN LÝ CƠ SỞ VẬT CHẤT

## 1.1. Use Case Diagram

![UC16 - Quản lý cơ sở vật chất](../images/UC16_QuanLyCoSoVatChat.png)

## 1.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Quản lý cơ sở vật chất |
| Mức | Mức nghiệp vụ |
| Tác nhân chính | Nhân viên quản lý (Staff) |
| Các bên liên quan | Staff, Technician, Student, Hệ thống |
| Mục tiêu | Quản lý danh sách tài sản, thiết bị, vật dụng trong ký túc xá (thêm, sửa, xóa, kiểm kê, báo hỏng) |
| Tiền điều kiện | Staff đã đăng nhập thành công với quyền STAFF |
| Kích hoạt | Staff chọn chức năng "Quản lý cơ sở vật chất" từ menu chính |
| Đảm bảo tối thiểu | Dữ liệu tài sản được validate trước khi lưu, ghi log mọi thay đổi |
| Đảm bảo thành công | Tài sản được thêm/sửa/xóa/kiểm kê/báo hỏng đúng yêu cầu, cập nhật trạng thái chính xác |
| Luồng chính | 1. Staff đăng nhập vào hệ thống<br/>2. Chọn menu "Quản lý cơ sở vật chất"<br/>3. Hệ thống hiển thị danh sách tài sản theo phòng/tòa nhà<br/>4. Staff chọn thao tác: Thêm/Sửa/Xóa/Kiểm kê/Báo hỏng<br/>5. **Nếu Thêm**: Nhập thông tin (mã TS, tên, loại, phòng, ngày mua, tình trạng) → Lưu<br/>6. **Nếu Sửa**: Chọn tài sản → Cập nhật thông tin → Lưu<br/>7. **Nếu Xóa**: Chọn tài sản → Kiểm tra ràng buộc → Xóa nếu hợp lệ<br/>8. **Nếu Kiểm kê**: Chọn phòng → Nhập số lượng thực tế → Đối chiếu → Cập nhật chênh lệch<br/>9. **Nếu Báo hỏng**: Chọn tài sản → Nhập mô tả lỗi → Tự động tạo yêu cầu sửa chữa<br/>10. Hệ thống xác nhận thành công và ghi log<br/>11. Kết thúc |
| Luồng ngoại lệ | **5A. Mã tài sản đã tồn tại (khi thêm)**<br/>1. Hệ thống kiểm tra thấy mã tài sản đã có trong database<br/>2. Hệ thống hiển thị thông báo: "Mã tài sản đã tồn tại trong hệ thống"<br/>3. Hệ thống tô viền đỏ trường Mã tài sản<br/>4. Giữ nguyên form, quay lại bước nhập<br/><br/>**5B. Phòng không tồn tại**<br/>1. Staff nhập mã phòng không hợp lệ<br/>2. Hệ thống hiển thị thông báo: "Phòng không tồn tại trong hệ thống"<br/>3. Quay lại bước nhập<br/><br/>**7A. Không thể xóa do tài sản đang được sử dụng**<br/>1. Hệ thống kiểm tra thấy tài sản đang có mặt tại phòng có sinh viên ở<br/>2. Hệ thống hiển thị thông báo: "Không thể xóa tài sản đang được sử dụng"<br/>3. Hủy thao tác xóa, giữ nguyên danh sách<br/>4. Kết thúc<br/><br/>**8A. Kiểm kê phát sinh chênh lệch**<br/>1. Số lượng thực tế khác số lượng trong hệ thống<br/>2. Hệ thống hiển thị báo cáo chênh lệch<br/>3. Yêu cầu Staff nhập lý do chênh lệch (mất, hỏng, chuyển phòng...)<br/>4. Staff xác nhận lý do<br/>5. Hệ thống cập nhật lại số lượng thực tế và ghi log kiểm kê<br/>6. Kết thúc<br/><br/>**9A. Tài sản đã có yêu cầu sửa chữa đang xử lý**<br/>1. Staff chọn báo hỏng nhưng tài sản đã có yêu cầu sửa chữa trước đó chưa hoàn thành<br/>2. Hệ thống hiển thị thông báo: "Tài sản đang có yêu cầu sửa chữa, không thể báo hỏng lại"<br/>3. Hủy thao tác, giữ nguyên form<br/>4. Kết thúc<br/><br/>**DB1. Lỗi kết nối Database**<br/>1. Hệ thống không thể kết nối đến Database<br/>2. Hệ thống hiển thị thông báo: "Lỗi hệ thống, vui lòng thử lại sau"<br/>3. Ghi exception log để IT kiểm tra<br/>4. Kết thúc |
| Quy tắc nghiệp vụ | BR01: Mỗi tài sản có mã duy nhất trong toàn hệ thống<br/>BR02: Không thể xóa tài sản đang được sử dụng tại phòng có sinh viên đang ở<br/>BR03: Kiểm kê bắt buộc phải có lý do khi phát sinh chênh lệch<br/>BR04: Báo hỏng tự động tạo yêu cầu sửa chữa với trạng thái "Chờ tiếp nhận" |

## 1.3. Activity Diagram - AD16

![AD16 - Quản lý cơ sở vật chất](../images/AD16_QuanLyCoSoVatChat.png)

## 1.4. Sequence Diagram - SD16

![SD16 - Quản lý cơ sở vật chất](../images/SD16_QuanLyCoSoVatChat.png)