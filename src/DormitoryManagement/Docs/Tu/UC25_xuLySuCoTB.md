## UC25 – Xử lý sự cố kỹ thuật (Technician)
Thuộc tính	Nội dung
Tên Usecase	Xử lý sự cố kỹ thuật
Mức	Mức người dùng
Tác nhân chính	Technician
Các bên liên quan	Technician, Student, Staff, Hệ thống
Mục tiêu	Technician thực hiện sửa chữa, cập nhật tiến độ và kết quả
Tiền điều kiện	Technician đã tiếp nhận yêu cầu (UC24), yêu cầu ở trạng thái "Đang xử lý"
Kích hoạt	Technician chọn yêu cầu đã tiếp nhận và bắt đầu sửa
Đảm bảo tối thiểu	Mỗi bước sửa chữa được ghi nhận, có thể tạm dừng và tiếp tục sau
Đảm bảo thành công	Sự cố được xử lý hoàn chỉnh, sẵn sàng để hoàn thành
Luồng chính	1. Technician chọn yêu cầu đã tiếp nhận
2. Hệ thống hiển thị chi tiết yêu cầu và checklist sửa chữa (theo loại tài sản)
3. Technician thực hiện từng bước sửa chữa
4. Sau mỗi bước, Technician cập nhật trạng thái hoàn thành bước đó
5. Hệ thống lưu tiến độ (thời gian, nội dung, ảnh sau sửa nếu có)
6. Nếu phát sinh vấn đề, Technician ghi chú bổ sung vào nhật ký
7. Technician có thể tạm dừng và tiếp tục sau (lưu lại tiến độ)
8. Khi hoàn thành tất cả các bước, Technician nhập kết quả xử lý
9. Hệ thống kiểm tra checklist đã đầy đủ
10. Hệ thống xác nhận sẵn sàng chuyển sang hoàn thành
11. Kết thúc
Luồng ngoại lệ	3A. Thiếu phụ tùng thay thế
1. Technician phát hiện cần phụ tùng nhưng không có sẵn trong kho
2. Technician chọn "Báo thiếu phụ tùng"
3. Hệ thống hiển thị form yêu cầu phụ tùng
4. Technician nhập thông tin: tên phụ tùng, số lượng, lý do
5. Hệ thống tạo phiếu yêu cầu phụ tùng, gửi cho Staff kho
6. Hệ thống cập nhật trạng thái yêu cầu thành "Chờ phụ tùng"
7. Chờ cấp phát phụ tùng (có thể thông báo qua hệ thống)
8. Sau khi có phụ tùng, Technician chọn "Tiếp tục xử lý"
9. Quay lại bước 3

3B. Sự cố vượt quá khả năng xử lý
1. Technician ghi nhận sự cố quá phức tạp, cần chuyên gia hoặc đơn vị ngoài
2. Chọn "Yêu cầu hỗ trợ"
3. Hệ thống tạo ticket nâng cấp (escalation)
4. Gửi thông báo cho trưởng bộ phận kỹ thuật
5. Cập nhật trạng thái yêu cầu thành "Chờ hỗ trợ"
6. Chờ phân công hỗ trợ
7. Sau khi có hỗ trợ, tiếp tục xử lý
8. Quay lại bước 3

8A. Checklist chưa hoàn thành khi báo cáo
1. Technician cố gắng kết thúc xử lý nhưng checklist còn thiếu
2. Hệ thống thông báo: "Chưa hoàn thành các bước: [danh sách]"
3. Yêu cầu Technician hoàn thành các bước còn thiếu
4. Quay lại bước 3
