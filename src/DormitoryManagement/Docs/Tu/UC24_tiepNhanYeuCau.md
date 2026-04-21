Thuộc tính	Nội dung
Tên Usecase	Tiếp nhận yêu cầu sửa chữa
Mức	Mức người dùng
Tác nhân chính	Technician
Các bên liên quan	Technician, Student, Staff, Hệ thống
Mục tiêu	Technician xem và nhận yêu cầu sửa chữa được phân công
Tiền điều kiện	Technician đã đăng nhập, có yêu cầu sửa chữa trạng thái "Chờ tiếp nhận"
Kích hoạt	Technician chọn "Danh sách yêu cầu sửa chữa"
Đảm bảo tối thiểu	Yêu cầu chỉ được nhận bởi một Technician, ghi log thời gian nhận
Đảm bảo thành công	Yêu cầu được gán cho Technician và chuyển trạng thái "Đang xử lý"
Luồng chính	1. Technician đăng nhập
2. Chọn "Danh sách yêu cầu sửa chữa"
3. Hệ thống hiển thị danh sách yêu cầu trạng thái "Chờ tiếp nhận" (sắp xếp theo thời gian tạo)
4. Technician chọn yêu cầu cần xử lý
5. Hệ thống hiển thị chi tiết yêu cầu (phòng, tài sản, mô tả lỗi, ảnh)
6. Technician chọn "Tiếp nhận"
7. Hệ thống kiểm tra yêu cầu chưa có ai nhận (lock để tránh race condition)
8. Hệ thống gán Technician ID và cập nhật trạng thái "Đang xử lý"
9. Hệ thống ghi log: thời gian tiếp nhận, Technician
10. Hệ thống thông báo thành công
11. Kết thúc
Luồng ngoại lệ	3A. Không có yêu cầu nào
1. Hệ thống hiển thị thông báo "Hiện tại không có yêu cầu sửa chữa nào"
2. Kết thúc

7A. Yêu cầu đã được tiếp nhận
1. Hệ thống phát hiện yêu cầu đã có technician_id hoặc status != "Chờ tiếp nhận"
2. Thông báo: "Yêu cầu đã được tiếp nhận bởi technician khác"
3. Tải lại danh sách
4. Kết thúc

DB1. Lỗi hệ thống
1. Không kết nối được database
2. Thông báo lỗi hệ thống
3. Ghi exception log
4. Kết thúc
