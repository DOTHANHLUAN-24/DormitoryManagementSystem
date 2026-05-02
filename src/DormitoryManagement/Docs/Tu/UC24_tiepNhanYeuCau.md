# 3. UC24: TIẾP NHẬN YÊU CẦU SỬA CHỮA

## 3.1. Use Case Diagram

![UC24 - Tiếp nhận yêu cầu sửa chữa](../images/UC24_TiepNhanYeuCauSuaChua.png)

## 3.2. Đặc tả Use Case

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
