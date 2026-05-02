# 2. UC22: TẠO YÊU CẦU SỬA CHỮA

## 2.1. Use Case Diagram

![UC22 - Tạo yêu cầu sửa chữa](../images/UC22_TaoYeuCauSuaChua.png)

## 2.2. Đặc tả Use Case

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

## 2.3. Activity Diagram - AD08

![AD08 - Tạo yêu cầu sửa chữa](../images/AD08_TaoYeuCauSuaChua.png)

## 2.4. Sequence Diagram - SD08

![SD08 - Tạo yêu cầu sửa chữa](../images/SD08_TaoYeuCauSuaChua.jpg)
