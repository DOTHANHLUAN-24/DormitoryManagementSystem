## Mô hình RBAC
    RBAC (Role-Based Access Control) là mô hình kiểm soát truy cập dựa trên vai trò, trong đó quyền truy cập được gán cho vai trò (role), và người dùng được gán vào các vai trò tương ứng. Khi đó người dùng có tất cả quyền của vai trò đó.

## Áp dụng cho hệ thông Quản lý KTX
Mã vai trò	        Tên vai trò	    	    Quyền chính
ROLE_STUDENT	    Sinh viên		        Đăng ký phòng, xem hóa đơn, thanh toán, xem thông báo
ROLE_MANAGER	    Quản lý KTX		        Duyệt đăng ký, quản lý phòng, xem báo cáo, quản lý hợp đồng
ROLE_CASHIER	    Nhân viên thu phí		Ghi nhận thanh toán, in hóa đơn, xem công nợ
ROLE_SECURITY	    Nhân viên bảo vệ		Tra cứu sinh viên, đăng ký khách thăm
ROLE_TECHNICIAN	    Nhân viên kỹ thuật		Xem và xử lý yêu cầu sửa chữa
ROLE_ADMIN	        Quản trị hệ thống		Tạo tài khoản, phân quyền, sao lưu dữ liệu

## Thuật toán phân phòng
Bước	                        Mô tả
1. Thu thập dữ liệu         	Khảo sát sinh viên về thói quen: giờ ngủ, giờ thức, tính cách, mức chi tiêu, sở thích
2. Tính điểm tương thích	    Tính điểm match giữa các cặp sinh viên dựa trên câu trả lời khảo sát
3. Sắp xếp ưu tiên	            Sắp xếp danh sách sinh viên theo thứ tự ưu tiên (chính sách, năm học, thời gian đăng ký)
4. Phân phòng tham lam	        Duyệt từng sinh viên, tìm phòng tốt nhất hiện có để xếp

### Tính tiền điện nước
Công thức	                                                Mô tả
Lượng tiêu thụ = Chỉ số cuối kỳ - Chỉ số đầu kỳ	            Tính số điện/nước đã dùng
Thành tiền = Lượng tiêu thụ × Đơn giá	                    Tính số tiền phải trả
Xử lý trường hợp đặc biệt:                                  Nếu chỉ số cuối < chỉ số đầu: Báo lỗi do nhập sai, yêu cầu nhập lại
                                                            Nếu phòng có nhiều sinh viên: Chia đều thành tiền cho các sinh viên

### Tính công nợ
Công thức	                                                                                Mô tả
Công nợ hiện tại = Tổng hóa đơn chưa thanh toán - Tổng thanh toán đã thực hiện	            Tính số tiền sinh viên đang nợ
Công nợ mới = Công nợ cũ + Tiền hóa đơn mới - Thanh toán mới	                            Cập nhật công nợ