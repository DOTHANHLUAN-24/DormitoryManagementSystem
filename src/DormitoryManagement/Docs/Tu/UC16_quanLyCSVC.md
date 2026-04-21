## UC16 – Quản lý Cơ sở vật chất (Staff)
|   Thuộc tính          | |   Nội dung                                                                                          |
|-----------------------| |-----------------------------------------------------------------------------------------------------|
|   Tên Usecase	        | |  Quản lý Cơ sở vật chất                                                                             |
|   Mức	                | |  Mức người dùng                                                                                     |
|   Tác nhân chính	    | |  Staff                                                                                              |
|   Các bên liên quan	| |  Staff, Technician, Student, Hệ thống                                                               |
|   Mục tiêu	        | |  Quản lý danh sách tài sản, thiết bị, vật dụng trong ký túc xá (thêm, sửa, xóa, kiểm kê, báo hỏng)  |
|   Tiền điều kiện	    | |  Staff đã đăng nhập thành công                                                                      |
|   Kích hoạt	        | |  Staff chọn chức năng "Quản lý Cơ sở vật chất"                                                      |
|   Đảm bảo tối thiểu	| |  Dữ liệu tài sản được validate trước khi lưu, ghi log mọi thay đổi                                  |
|   Đảm bảo thành công	| |  Tài sản được thêm/sửa/xóa/kiểm kê đúng yêu cầu, cập nhật trạng thái chính xác                      |
|                       | |                                                                                                     |
|   Luồng chính	        | |  1. Staff đăng nhập                                                                                 |
|                       | |  2. Chọn mục "Quản lý phương tiện"                                                                  |
|                       | |  3. Hệ thống hiển thị danh sách tài sản theo phòng/tòa nhà                                          |
|                       | |  4. Staff chọn thao tác (Thêm/Sửa/Xóa/Kiểm kê/Báo hỏng)                                             |
|                       | |  5. Nếu Thêm: nhập thông tin (mã tài sản, tên, loại, phòng, ngày mua, tình trạng),lưu               |
|                       | |  6. Nếu Sửa: chọn tài sản, cập nhật thông tin, lưu                                                  |
|                       | |  7. Nếu Xóa: chọn tài sản, kiểm tra ràng buộc, xóa nếu hợp lệ                                       |
|                       | |  8. Nếu Kiểm kê: chọn phòng/tòa nhà, nhập số lượng thực tế, đối chiếu, cập nhật chênh lệch          |
|                       | |  9. Nếu Báo hỏng: chọn, nhập mô tả lỗi, chuyển trạng thái "Chờ sửa chữa" và tự động tạo yêu cầu sửa |
|                       | |  10.Hệ thống xác nhận thành công                                                                    |
|                       | |                                                                                                     |
|   Luồng ngoại lệ	    | |  5A. Mã tài sản đã tồn tại (khi thêm)                                                               |
|                       | |  1. Hệ thống kiểm tra thấy mã tài sản đã có                                                         |
|                       | |  2. Thông báo: "Mã tài sản đã tồn tại trong hệ thống"                                               |
|                       | |  3. Giữ nguyên form                                                                                 |
|                       | |  4. Quay lại bước nhập                                                                              |
|                       | |                                                                                                     |
|                       | |  5B. Phòng không tồn tại                                                                            |
|                       | |  1. Staff nhập mã phòng không hợp lệ                                                                |
|                       | |  2. Thông báo: "Phòng không tồn tại trong hệ thống"                                                 |
|                       | |  3. Quay lại bước nhập                                                                              |    
|                       | |                                                                                                     |
|                       | |  7A. Không thể xóa do tài sản đang được sử dụng                                                     |
|                       | |  1. Hệ thống kiểm tra thấy tài sản đang có mặt tại phòng có sinh viên ở                             |
|                       | |  2. Thông báo: "Không thể xóa tài sản đang được sử dụng"                                            |
|                       | |  3. Hủy thao tác xóa                                                                                |
|                       | |  4. Kết thúc                                                                                        |
|                       | |                                                                                                     |
|                       | |  8A. Kiểm kê phát sinh chênh lệch                                                                   |
|                       | |  1. Số lượng thực tế khác số lượng trong hệ thống                                                   |
|                       | |  2. Hệ thống hiển thị báo cáo chênh lệch                                                            |
|                       | |  3. Yêu cầu Staff nhập lý do chênh lệch (mất, hỏng, chuyển phòng...)                                |
|                       | |  4. Staff xác nhận lý do                                                                            |
|                       | |  5. Hệ thống cập nhật lại số lượng thực tế và ghi log kiểm kê                                       |
|                       | |  6. Kết thúc                                                                                        |
|                       | |                                                                                                     |
|                       | |  9A. Tài sản đã có yêu cầu sửa chữa đang xử lý                                                      |
|                       | |  1. Staff chọn báo hỏng nhưng tài sản đã có yêu cầu sửa chữa trước đó chưa hoàn thành               |
|                       | |  2. Thông báo: "Tài sản đang có yêu cầu sửa chữa, không thể báo hỏng lại"                           |
|                       | |  3. Hủy thao tác                                                                                    |
|                       | |  4. Kết thúc                                                                                        |
|                       | |  DB1. Lỗi hệ thống                                                                                  |
|                       | |  1. Không kết nối được database                                                                     |
|                       | |  2. Thông báo lỗi hệ thống                                                                          |
|                       | |  3. Ghi exception log                                                                               |
|                       | |  4. Kết thúc                                                                                        |