## UC26 – Cập nhật trạng thái sửa chữa (Technician)
Thuộc tính	Nội dung
Tên Usecase	Cập nhật trạng thái sửa chữa
Mức	Mức người dùng
Tác nhân chính	Technician
Các bên liên quan	Technician, Student, Staff, Hệ thống
Mục tiêu	Cập nhật trạng thái yêu cầu sửa chữa trong suốt vòng đời xử lý
Tiền điều kiện	Technician đã tiếp nhận yêu cầu (UC24)
Kích hoạt	Technician thực hiện thao tác thay đổi trạng thái
Đảm bảo tối thiểu	Mỗi lần cập nhật trạng thái được ghi log kèm thời gian, lý do (nếu có)
Đảm bảo thành công	Trạng thái yêu cầu được cập nhật chính xác, đúng luồng
Luồng chính	1. Technician chọn yêu cầu đang xử lý
2. Hệ thống hiển thị trạng thái hiện tại và lịch sử trạng thái
3. Technician chọn trạng thái mới từ danh sách cho phép
4. Technician nhập ghi chú/lý do thay đổi (nếu cần)
5. Hệ thống kiểm tra tính hợp lệ của chuyển trạng thái (theo ma trận)
6. Hệ thống cập nhật trạng thái mới vào database
7. Hệ thống ghi log: thời gian, người thực hiện, trạng thái cũ → mới, ghi chú
8. Nếu trạng thái là "Hoàn thành", hệ thống tự động gửi thông báo cho Student
9. Nếu trạng thái là "Từ chối", hệ thống yêu cầu nhập lý do từ chối (bắt buộc)
10. Hệ thống thông báo thành công
11. Kết thúc
Luồng ngoại lệ	3A. Chuyển trạng thái không hợp lệ
1. Hệ thống phát hiện chuyển trạng thái không nằm trong ma trận cho phép
2. Ví dụ: "Chờ tiếp nhận" → "Hoàn thành" (bỏ qua bước "Đang xử lý")
3. Thông báo: "Không thể chuyển từ [trạng thái cũ] sang [trạng thái mới]"
4. Giữ nguyên trạng thái cũ
5. Kết thúc

3B. Thiếu thông tin bắt buộc
1. Khi chuyển sang "Từ chối", Technician chưa nhập lý do từ chối
2. Thông báo: "Vui lòng nhập lý do từ chối"
3. Quay lại bước 3
4. Hoặc khi chuyển sang "Hoàn thành", chưa nhập kết quả xử lý
5. Thông báo: "Vui lòng nhập kết quả xử lý"
6. Quay lại bước 3

5A. Yêu cầu đã bị hủy bởi Staff/Admin
1. Hệ thống kiểm tra thấy yêu cầu đã bị hủy (status = "Đã hủy")
2. Thông báo: "Yêu cầu đã bị hủy, không thể cập nhật trạng thái"
3. Kết thúc
