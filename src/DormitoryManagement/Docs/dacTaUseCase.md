- Họ và tên: Lê Thị Cẩm Tú
- Mã sinh viên: 2321050008
======
## 1. UC-01: ĐĂNG NHẬP HỆ THỐNG

Tiêu đề	                        Nội dung
Tên Use case	                Đăng nhập hệ thống
Mức	                            Mức người dùng              
Tác nhân chính	                Sinh viên, Quản lý KTX, Nhân viên thu phí, Nhân viên bảo vệ, Nhân viên kỹ thuật, Quản trị hệ thống
Các bên liên quan và lợi ích	- Người dùng: Muốn truy cập vào hệ thống để sử dụng các chức năng theo vai trò
                                - Hệ thống: Đảm bảo chỉ người dùng hợp lệ mới được truy cập, bảo mật thông tin
Người chịu trách nhiệm	        Hệ thống quản lý ký túc xá
Tiền điều kiện	                Người dùng đã có tài khoản hợp lệ trong hệ thống. Hệ thống đang hoạt động bình thường.
Đảm bảo tối thiểu	            - Hệ thống ghi nhận lại lần đăng nhập thất bại (log)
                                - Không có phiên làm việc nào được tạo nếu đăng nhập thất bại
Đảm bảo thành công	            - Người dùng được xác thực thành công
                                - Hệ thống tạo phiên làm việc (session/token)
                                - Người dùng được chuyển đến giao diện tương ứng với vai trò

### Chuỗi sự kiện chính	
1. Người dùng mở giao diện đăng nhập của hệ thống
2. Hệ thống hiển thị biểu mẫu đăng nhập (Tên đăng nhập, Mật khẩu, nút "Đăng nhập", link "Quên mật khẩu")
3. Người dùng nhập tên đăng nhập và mật khẩu
4. Người dùng nhấn nút "Đăng nhập"
5. Hệ thống kiểm tra tên đăng nhập có tồn tại trong cơ sở dữ liệu không
6. Hệ thống kiểm tra mật khẩu nhập vào có khớp với mật khẩu đã mã hóa trong CSDL không
7. Hệ thống ghi nhận thời gian, địa chỉ IP, tạo phiên làm việc (session/token)
8. Hệ thống xác định vai trò (role) của người dùng
9. Hệ thống chuyển hướng đến trang chính tương ứng với vai trò
10. Kết thúc use case

### Ngoại lệ	
    2A. Người dùng chọn "Quên mật khẩu": Chuyển sang use case đặt lại mật khẩu
    5A. Tên đăng nhập không tồn tại: Thông báo lỗi, quay lại bước 2
    5B. Để trống tên hoặc mật khẩu: Thông báo lỗi, quay lại bước 2
    6A. Sai mật khẩu (lần 1-4): Thông báo còn X lần thử, quay lại bước 2
    6B. Sai mật khẩu (lần 5): Khóa tài khoản 15 phút, kết thúc
    7A. Tài khoản bị vô hiệu hóa: Thông báo liên hệ quản trị, kết thúc

## UC-02: ĐĂNG KÝ PHÒNG KÝ TÚC XÁ
Tiêu đề	                        Nội dung
Tên Use case	                Đăng ký phòng ký túc xá
Mức	                            Mức người dùng 
Tác nhân chính	                Sinh viên
Các bên liên quan và lợi ích	- Sinh viên: Muốn đăng ký được phòng ở ký túc xá theo nhu cầu
                                - Quản lý KTX: Muốn quản lý việc đăng ký phòng một cách minh bạch, tránh trùng lặp
                                - Hệ thống: Đảm bảo dữ liệu nhất quán, xử lý tranh chấp khi nhiều sinh viên đặt cùng phòng
Người chịu trách nhiệm	        Hệ thống quản lý ký túc xá
Tiền điều kiện	                Sinh viên đã đăng nhập thành công. Sinh viên chưa có phòng trong học kỳ hiện tại. Sinh viên đã đóng học phí theo quy định.
Đảm bảo tối thiểu	            - Phòng không bị khóa nếu đăng ký thất bại
                                - Hệ thống thông báo lỗi chi tiết cho sinh viên
                                - Không tạo phiếu đăng ký nếu có lỗi
Đảm bảo thành công	            - Tạo phiếu đăng ký với trạng thái "Chờ duyệt"
                                - Phòng bị khóa tạm trong 24h để tránh trùng lặp
                                - Gửi thông báo email cho sinh viên và quản lý

### Chuỗi sự kiện chính	
1. Sinh viên chọn chức năng "Đăng ký phòng" từ trang chủ
2. Hệ thống hiển thị danh sách các phòng trống với bộ lọc (tòa nhà, loại phòng, tầng, giá)
3. Sinh viên sử dụng bộ lọc để thu hẹp kết quả
4. Sinh viên chọn một phòng từ danh sách và nhấn nút "Đăng ký"
5. Hệ thống kiểm tra tính khả dụng của phòng (còn chỗ trống, chưa bị đặt tạm)
6. Hệ thống hiển thị form nhập thông tin bổ sung (thời gian ở dự kiến, thông tin người thân liên hệ)
7. Sinh viên nhập đầy đủ thông tin và nhấn "Gửi đăng ký"
8. Hệ thống kiểm tra thông tin hợp lệ
9. Hệ thống tạo phiếu đăng ký với trạng thái "Chờ duyệt"
10. Hệ thống khóa tạm phòng đã chọn trong vòng 24 giờ
11. Hệ thống gửi email thông báo cho sinh viên và quản lý
12. Kết thúc use case

### Ngoại lệ	
    1A. Sinh viên chọn "Đăng ký theo nhóm":
        1. Hệ thống hiển thị form nhập danh sách MSV muốn ở ghép
        2. Kiểm tra các MSV đều đủ điều kiện
        3. Chuyển đến bước 2 để chọn phòng chung cho cả nhóm
    3A. Sinh viên nhấn "Xem chi tiết phòng":
        1. Hệ thống hiển thị chi tiết phòng (ảnh, tiện ích, nội quy)
        2. Quay lại bước 2
        4A. Sinh viên đã có phòng:
        1. Hệ thống thông báo "Bạn đã có phòng ở [mã phòng]. Vui lòng liên hệ quản lý nếu muốn đổi phòng."
        2. Kết thúc use case
    4B. Sinh viên chưa đóng học phí:
        1. Hệ thống thông báo "Bạn cần hoàn tất nghĩa vụ học phí trước khi đăng ký ký túc xá."
        2. Kết thúc use case
    5A. Phòng không còn chỗ trống:
        1. Hệ thống thông báo "Phòng này vừa hết chỗ trống. Vui lòng chọn phòng khác."
        2. Quay lại bước 2
    5B. Phòng đang bị người khác đặt tạm:
        1. Hệ thống thông báo "Phòng này đang có người đăng ký trước. Vui lòng chọn phòng khác hoặc thử lại sau 24h."
        2. Quay lại bước 2
    5C. Hết thời hạn đăng ký:
        1. Hệ thống thông báo "Đã hết thời hạn đăng ký phòng học kỳ này."
        2. Kết thúc use case
    7A. Sinh viên nhấn "Hủy" thay vì "Gửi đăng ký":
        1. Hệ thống quay lại bước 2
        2. Kết thúc use case
    8A. Để trống thông tin bắt buộc:
        1. Hệ thống thông báo "Vui lòng nhập đầy đủ thông tin"
        2. Quay lại bước 6
