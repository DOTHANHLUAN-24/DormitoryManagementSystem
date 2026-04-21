## UC22 – Tạo yêu cầu sửa chữa (Student)
|   Thuộc tính          | |   Nội dung                                                                  |
|-----------------------| |-----------------------------------------------------------------------------|
|   Tên Usecase	        | |  Tạo yêu cầu sửa chữa                                                       |
|   Mức	                | |  Mức người dùng                                                             |
|   Tác nhân chính	    | |  Student                                                                    |
|   Các bên liên quan	| |  Student, Technician, Staff, Hệ thống                                       |
|   Mục tiêu	        | |  Ghi nhận và lưu trữ yêu cầu sửa chữa từ sinh viên                          |
|   Tiền điều kiện	    | |  Student đã đăng nhập thành công và đang có hợp đồng thuê phòng hiệu lực    |
|   Kích hoạt	        | |  Student chọn chức năng "Tạo yêu cầu sửa chữa"                              |
|   Đảm bảo tối thiểu	| |  Yêu cầu không được lưu nếu dữ liệu không hợp lệ, hệ thống ghi log lỗi      |
|   Đảm bảo thành công	| |  Yêu cầu sửa chữa được tạo, lưu vào database, gửi thông báo thành công      |
|                       | |                                                                             |
|   Luồng chính	        | |  1. Student đăng nhập vào hệ thống                                          |
|                       | |  2. Chọn mục "Tạo yêu cầu sửa chữa"                                         |
|                       | |  3. Hệ thống hiển thị form nhập thông tin                                   |
|                       | |  4. Student nhập: đồ vật, mô tả lỗi, ảnh minh họa (tùy chọn)                |
|                       | |  5. Hệ thống validate dữ liệu đầu vào                                       |
|                       | |  6. Hệ thống kiểm tra cơ sở hạ tầng có tồn tại trong danh sách của Student  |
|                       | |  7. Hệ thống lưu yêu cầu vào database với trạng thái "Chờ xử lý"            |
|                       | |  8. Hệ thống gửi thông báo thành công                                       |
|                       | |  9. Hệ thống hiển thị mã yêu cầu cho Student                                |
|                       | |                                                                             |
|   Luồng ngoại lệ	    | |  5A. Thiếu thông tin bắt buộc                                               |
|                       | |  1. Hệ thống phát hiện để trống đồ vật hoặc mô tả lỗi                       |
|                       | |  2. Highlight trường thiếu                                                  |
|                       | |  3. Thông báo: "Vui lòng nhập đầy đủ thông tin!"                            |
|                       | |  4. Giữ nguyên form, quay lại bước 4                                        |
|                       | |                                                                             |
|                       | |  6A. Cơ sở hạ tầng không tồn tại                                            |
|                       | |  1. Tài sản không thuộc danh sách cơ sở hạ tầng của Student                 |
|                       | |  2. Thông báo: "cơ sở hạ tầng chưa được đăng ký trong hệ thống!"            |
|                       | |  3. Giữ nguyên form                                                         |
|                       | |  4. Quay lại bước 4                                                         |
|                       | |                                                                             |
|                       | |  6B. cơ sở hạ tầng đang có yêu cầu sửa chữa chưa hoàn thành                 |
|                       | |  1. Hệ thống phát hiện yêu cầu trước đó chưa hoàn thành                     |
|                       | |  2. Thông báo: "cơ sở hạ tầng đang có yêu cầu sửa chữa đang xử lý!"         |
|                       | |  3. Kết thúc                                                                |
|                       | |                                                                             |
|                       | |  DB1. Lỗi hệ thống                                                          |
|                       | |  1. Không kết nối được database                                             |
|                       | |  2. Thông báo lỗi hệ thống                                                  |
|                       | |  3. Ghi exception log                                                       |
|                       | |  4. Kết thúc                                                                |