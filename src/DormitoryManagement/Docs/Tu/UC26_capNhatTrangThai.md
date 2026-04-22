# 5. UC26: CẬP NHẬT TRẠNG THÁI SỬA CHỮA

## 5.1. Use Case Diagram

![UC26 - Cập nhật trạng thái sửa chữa](../images/UC26_CapNhatTrangThaiSuaChua.png)

## 5.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Cập nhật trạng thái sửa chữa |
| Mức | Mức người dùng |
| Tác nhân chính | Nhân viên kỹ thuật (Technician) |
| Các bên liên quan | Technician, Student, Staff, Hệ thống |
| Mục tiêu | Cập nhật trạng thái yêu cầu sửa chữa trong suốt vòng đời xử lý |
| Tiền điều kiện | Technician đã tiếp nhận yêu cầu (UC24) |
| Kích hoạt | Technician thực hiện thao tác thay đổi trạng thái từ màn hình chi tiết yêu cầu |
| Đảm bảo tối thiểu | Mỗi lần cập nhật trạng thái được ghi log kèm thời gian, lý do (nếu có) |
| Đảm bảo thành công | Trạng thái yêu cầu được cập nhật chính xác, đúng luồng |
| Luồng chính | 1. Technician chọn yêu cầu đang xử lý<br/>2. Hệ thống hiển thị trạng thái hiện tại và lịch sử trạng thái<br/>3. Technician chọn nút "Cập nhật trạng thái"<br/>4. Hệ thống hiển thị danh sách trạng thái cho phép (dựa trên ma trận chuyển)<br/>5. Technician chọn trạng thái mới từ dropdown<br/>6. Technician nhập ghi chú/lý do thay đổi (bắt buộc nếu chuyển sang "Từ chối" hoặc "Đã hủy")<br/>7. Hệ thống kiểm tra tính hợp lệ của chuyển trạng thái<br/>8. Hệ thống cập nhật trạng thái mới vào database<br/>9. Hệ thống ghi log: thời gian, người thực hiện, trạng thái cũ → mới, ghi chú<br/>10. Nếu trạng thái là "Hoàn thành", hệ thống tự động gửi thông báo cho Student<br/>11. Hệ thống thông báo thành công<br/>12. Kết thúc |
| Luồng ngoại lệ | **4A. Danh sách trạng thái cho phép rỗng**<br/>1. Hệ thống tính toán danh sách trạng thái có thể chuyển từ trạng thái hiện tại<br/>2. Nếu không có trạng thái nào (ví dụ đang ở trạng thái cuối "Hoàn thành")<br/>3. Hệ thống vô hiệu hóa nút "Cập nhật trạng thái"<br/>4. Hiển thị tooltip: "Không thể cập nhật trạng thái từ trạng thái hiện tại"<br/>5. Kết thúc<br/><br/>**5A. Chuyển trạng thái không hợp lệ**<br/>1. Technician chọn trạng thái không nằm trong danh sách cho phép<br/>2. Hệ thống kiểm tra và phát hiện chuyển trạng thái vi phạm ma trận<br/>3. Ví dụ: "Chờ tiếp nhận" → "Hoàn thành" (bỏ qua bước "Đang xử lý")<br/>4. Hệ thống hiển thị thông báo: "Không thể chuyển từ [trạng thái cũ] sang [trạng thái mới]"<br/>5. Giữ nguyên trạng thái cũ, không cập nhật<br/>6. Kết thúc<br/><br/>**6A. Thiếu lý do bắt buộc**<br/>1. Technician chọn trạng thái "Từ chối" hoặc "Đã hủy" nhưng chưa nhập lý do<br/>2. Hệ thống phát hiện trường ghi chú đang để trống<br/>3. Hệ thống hiển thị thông báo: "Vui lòng nhập lý do từ chối/hủy yêu cầu"<br/>4. Hệ thống tô viền đỏ ô nhập ghi chú<br/>5. Quay lại bước 6<br/><br/>**7A. Yêu cầu đã bị hủy bởi Staff/Admin**<br/>1. Technician cố gắng cập nhật trạng thái nhưng yêu cầu đã bị Staff hủy từ trước<br/>2. Hệ thống kiểm tra thấy status hiện tại = "Đã hủy"<br/>3. Hệ thống hiển thị thông báo: "Yêu cầu đã bị hủy, không thể cập nhật trạng thái"<br/>4. Kết thúc<br/><br/>**DB1. Lỗi kết nối Database**<br/>1. Hệ thống không thể cập nhật trạng thái do mất kết nối database<br/>2. Hệ thống hiển thị thông báo: "Lỗi hệ thống, không thể cập nhật trạng thái. Vui lòng thử lại sau."<br/>3. Ghi exception log<br/>4. Kết thúc |
