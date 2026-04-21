## Mô hình RBAC
    RBAC (Role-Based Access Control) là mô hình kiểm soát truy cập dựa trên vai trò, trong đó quyền truy cập được gán cho vai trò (role), và người dùng được gán vào các vai trò tương ứng. Khi đó người dùng có tất cả quyền của vai trò đó.

         
 Người dùng ────▶    Vai trò   ────▶   Quyền hạn  
   (User)            (Role)             (Permission)      
                        │
                        ▼
                Người dùng có nhiều vai trò    
                    

## Áp dụng cho hệ thông Quản lý KTX
Mã vai trò	        Tên vai trò	    	    Quyền chính
STUDENT	            Sinh viên		        Đăng ký phòng, xem hóa đơn, thanh toán, xem thông báo
MANAGER	            Quản lý KTX		        Duyệt đăng ký, quản lý phòng, xem báo cáo, quản lý hợp đồng
CASHIER	            Nhân viên thu phí		Ghi nhận thanh toán, in hóa đơn, xem công nợ
SECURITY	        Nhân viên bảo vệ		Tra cứu sinh viên, đăng ký khách thăm
TECHNICIAN	        Nhân viên kỹ thuật		Xem và xử lý yêu cầu sửa chữa
ADMIN	            Quản trị hệ thống		Tạo tài khoản, phân quyền, sao lưu dữ liệu, CRUD mọi thứ

### Ưu điểm của RBAC
Ưu điểm                 	Mô tả
Dễ quản lý	                Gán quyền cho vai trò thay vì từng người dùng
Linh hoạt	                Một người có thể có nhiều vai trò
Bảo mật                 	Nguyên tắc "ít đặc quyền nhất" 
Tuân thủ	                Dễ dàng kiểm tra và đối chiếu quyền hạn
Dễ mở rộng	                Thêm vai trò mới mà không ảnh hưởng hệ thống

## Thuật toán phân phòng
Bước	            Mô tả
1.	                Sắp xếp danh sách sinh viên theo tiêu chí ưu tiên (diện chính sách, năm cũ, đăng ký sớm)
2.	                Duyệt từng sinh viên theo thứ tự đã sắp xếp
3.	                Với mỗi sinh viên, tìm phòng phù hợp nhất (đúng loại phòng, còn chỗ, gần bạn cùng khoa nếu có)
4.	                Xếp sinh viên vào phòng tìm được
5.	                Cập nhật số chỗ trống của phòng
6.	                Lặp lại bước 2-5 cho đến khi hết danh sách

* Độ phức tạp
Thời gian	    O(M × N) với M là số sinh viên, N là số phòng
Không gian	    O(M + N)

* Ưu và nhược điểm
Ưu điểm	                                Nhược điểm
Đơn giản, dễ cài đặt	                Chưa chắc tối ưu toàn cục
Thời gian thực thi nhanh	            Có thể sinh viên sau bị xếp phòng xa
Phù hợp với dữ liệu vừa và nhỏ	        Phụ thuộc vào thứ tự ưu tiên ban đầu

### Tính tiền điện nước
Công thức	                                                Mô tả
Lượng tiêu thụ = Chỉ số cuối kỳ - Chỉ số đầu kỳ	            Tính số điện/nước đã dùng
Thành tiền = Lượng tiêu thụ × Đơn giá	                    Tính số tiền phải trả
Tiền mỗi sinh viên = Tổng hóa đơn phòng / Số sinh viên	    Chia đều sinh viên mỗi phòng
Xử lý trường hợp đặc biệt:                                  Nếu chỉ số cuối < chỉ số đầu: Báo lỗi do nhập sai, yêu cầu nhập lại


### Tính công nợ
Công thức	                                                                                Mô tả
Công nợ hiện tại = Tổng hóa đơn chưa thanh toán - Tổng thanh toán đã thực hiện	            Tính số tiền sinh viên đang nợ
Công nợ mới = Công nợ cũ + Tiền hóa đơn mới - Thanh toán mới	                            Cập nhật công nợ

## SOFT DELETE (XÓA MỀM)
1. Khái niệm
Soft Delete là kỹ thuật không xóa dữ liệu vật lý khỏi cơ sở dữ liệu, mà chỉ đánh dấu bản ghi là "đã xóa" bằng một cờ (flag) hoặc thời gian xóa.
Nên dùng với:   Dữ liệu quan trọng cần lưu lịch sử
                Dữ liệu có thể cần khôi phục
                Quan hệ cha-con cần giữ toàn vẹn
2. Cấu trúc bảng có Soft Delete
Trường	        Kiểu dữ liệu	        Mô tả
id	            INT 	                Khóa chính
is_deleted	    BOOLEAN	                Cờ đánh dấu đã xóa (mặc định: FALSE)
deleted_at	    TIMESTAMP	            Thời điểm xóa (NULL nếu chưa xóa)
deleted_by	    VARCHAR	                Người thực hiện xóa

3. Nguyên tắc hoạt động
Thao tác	                Cách thực hiện
Xóa (DELETE)	            UPDATE table SET is_deleted = 1, deleted_at = NOW() WHERE id = ?
Truy vấn (SELECT)	        SELECT * FROM table WHERE is_deleted = 0 (luôn lọc bỏ bản ghi đã xóa)
Khôi phục	                UPDATE table SET is_deleted = 0, deleted_at = NULL WHERE id = ?
Xóa cứng	                DELETE FROM table WHERE id = ? (chỉ dùng cho dọn dẹp định kỳ)

4. Ưu và nhược điểm
Ưu điểm	                                        Nhược điểm
Dữ liệu không bị mất vĩnh viễn	                Tốn dung lượng lưu trữ
Có thể khôi phục khi cần	                    Mọi truy vấn đều phải thêm điều kiện is_deleted = 0
Phục vụ kiểm toán, truy xuất lịch sử	        Quên lọc sẽ lấy cả dữ liệu đã xóa
Tránh lỗi reference key	                        Cần index trên trường is_deleted