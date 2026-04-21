# ĐẶC TẢ USE CASE: HỆ THỐNG QUẢN LÝ KÝ TÚC XÁ - PHẦN BỔ SUNG - 17/04/2026

---

## 1. Use Case: Đăng nhập hệ thống

| Thuộc tính         | Nội dung                                                                                                                                                                                                                                                                     |
| :----------------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Tên Usecase        | Đăng nhập hệ thống                                                                                                                                                                                                                                                           |
| Mức                | Mức người dùng                                                                                                                                                                                                                                                               |
| Tác nhân chính     | Sinh viên, Quản lý                                                                                                                                                                                                                                                           |
| Các bên liên quan  | Người dùng, Hệ thống                                                                                                                                                                                                                                                         |
| Mục tiêu           | Xác thực và cấp quyền truy cập                                                                                                                                                                                                                                               |
| Tiền điều kiện     | Người dùng có tài khoản hợp lệ                                                                                                                                                                                                                                               |
| Kích hoạt          | Người dùng truy cập màn hình đăng nhập                                                                                                                                                                                                                                       |
| Đảm bảo tối thiểu  | Không truy cập nếu xác thực thất bại                                                                                                                                                                                                                                         |
| Đảm bảo thành công | Người dùng được chuyển vào hệ thống theo quyền                                                                                                                                                                                                                               |
| Luồng chính        | 1. Truy cập màn hình đăng nhập<br/>2. Kiểm tra session/token<br/>3. Hiển thị form<br/>4. Nhập thông tin<br/>5. Validate client<br/>6. Gửi API<br/>7. API kiểm tra user<br/>8. So sánh password<br/>9. Kiểm tra trạng thái<br/>10. Tạo JWT<br/>11. Lưu session<br/>12. Ghi log<br/>13. Trả response<br/>14. Redirect |
| Luồng ngoại lệ     | **5A. Trống dữ liệu**<br/>1. Hệ thống phát hiện người dùng để trống Tên đăng nhập hoặc Mật khẩu<br/>2. Hệ thống highlight trường thiếu và hiển thị thông báo: “Vui lòng nhập đầy đủ thông tin!”<br/>3. Hệ thống giữ người dùng ở lại form đăng nhập<br/>4. Quay lại bước nhập thông tin<br/><br/>**5B. Sai định dạng mật khẩu**<br/>1. Hệ thống kiểm tra mật khẩu đã nhập<br/>2. Điều kiện vi phạm: không đủ độ dài/thiếu ký tự yêu cầu<br/>3. Hệ thống dừng yêu cầu đăng nhập<br/>4. Thông báo: “Mật khẩu không hợp lệ”<br/>5. Giữ username, xóa password<br/>6. Kết thúc<br/><br/>**6A. Sai thông tin đăng nhập**<br/>1. Username/Password không khớp CSDL<br/>2. Thông báo: “Tên đăng nhập hoặc Mật khẩu không chính xác!”<br/>3. Ghi log thất bại<br/>4. Kết thúc<br/><br/>**7A. Tài khoản bị khóa**<br/>1. Tài khoản ở trạng thái Locked<br/>2. Thông báo: “Tài khoản đã bị khóa”<br/>3. Ghi log<br/>4. Kết thúc<br/><br/>**DB1. Lỗi hệ thống**<br/>1. Không kết nối được DB<br/>2. Thông báo lỗi hệ thống<br/>3. Ghi exception log<br/>4. Kết thúc 
| Quy tắc ngiệp vụ   | Password phải được hash, giới hạn số lần đăng nhập |

---

## 2. Use Case: Đăng ký phòng

| Thuộc tính         | Nội dung                                                                                                                                               |
| :----------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------- |
| Tên Usecase        | Đăng ký phòng                                                                                                                                          |
| Mức                | Mức người dùng                                                                                                                                         |
| Tác nhân chính     | Sinh viên                                                                                                                                              |
| Các bên liên quan  | Sinh viên, Hệ thống                                                                                                                                    |
| Mục tiêu           | Đăng ký phòng ký túc xá                                                                                                                                |
| Tiền điều kiện     | Sinh viên chưa đăng ký phòng                                                                                                                           |
| Kích hoạt          | Sinh viên chọn chức năng đăng ký                                                                                                                       |
| Đảm bảo tối thiểu  | Không tạo đăng ký nếu dữ liệu không hợp lệ                                                                                                             |
| Đảm bảo thành công | Tạo yêu cầu đăng ký thành công                                                                                                                         |
| Luồng chính        | 1. Mở chức năng<br/>2. Xem phòng trống<br/>3. Chọn phòng<br/>4. Nhập thông tin<br/>5. Validate<br/>6. Gửi request<br/>7. API kiểm tra<br/>8. Tạo record Pending<br/>9. Ghi log |
| Luồng ngoại lệ     | 5A. Thiếu dữ liệu<br/>1. Hệ thống phát hiện thiếu thông tin<br/>2. Hiển thị thông báo<br/>3. Dừng xử lý<br/>4. Quay lại bước nhập<br/><br/>6A. Đã đăng ký<br/><br/>1. Hệ thống phát hiện sinh viên đã có đăng ký<br/>2. Thông báo từ chối<br/>3. Kết thúc<br/><br/>7A. Phòng đầy<br/><br/>1. Kiểm tra số chỗ<br/>2. Không còn slot<br/>3. Thông báo<br/>4. Kết thúc|
| Quy tắc ngiệp vụ   | Mỗi sinh viên chỉ được 1 đăng ký hợp lệ |

---

## 3. Use Case: Tính tiền điện nước

| Thuộc tính         | Nội dung                                                                                                    |
| :----------------- | :---------------------------------------------------------------------------------------------------------- |
| Tên Usecase        | Tính tiền điện nước                                                                                         |
| Mức                | Mức hệ thống                                                                                                |
| Tác nhân chính     | Scheduler                                                                                                   |
| Các bên liên quan  | Hệ thống                                                                                                    |
| Mục tiêu           | Tính chi phí điện nước hàng tháng                                                                           |
| Tiền điều kiện     | Có dữ liệu công tơ                                                                                          |
| Kích hoạt          | Scheduler cuối tháng                                                                                        |
| Đảm bảo tối thiểu  | Bỏ qua dữ liệu lỗi                                                                                          |
| Đảm bảo thành công | Tạo hóa đơn đầy đủ                                                                                          |
| Luồng chính        | 1. Trigger<br/>2. Lấy dữ liệu<br/>3. Validate<br/>4. Tính tiêu thụ<br/>5. Áp giá<br/>6. Tạo hóa đơn<br/>7. Lưu DB<br/>8. Ghi log |
| Luồng ngoại lệ     | 3A. Thiếu dữ liệu<br/>1. Không có dữ liệu công tơ<br/>2. Bỏ qua phòng<br/>3. Ghi log<br/><br/>4A. Dữ liệu bất thường<br/><br/>1. Phát hiện dữ liệu bất thường<br/>2. Flag dữ liệu<br/>3. Không tính tiền<br/>4. Ghi log                                                                                           |
| Quy tắc ngiệp vụ   | Áp giá bậc thang theo quy định |

---

## 4. Use Case: Thanh toán hóa đơn

| Thuộc tính         | Nội dung                                                                                                                      |
| :----------------- | :---------------------------------------------------------------------------------------------------------------------------- |
| Tên Usecase        | Thanh toán hóa đơn                                                                                                            |
| Mức                | Mức người dùng                                                                                                                |
| Tác nhân chính     | Sinh viên                                                                                                                     |
| Các bên liên quan  | Sinh viên, Payment Gateway                                                                                                    |
| Mục tiêu           | Thanh toán hóa đơn                                                                                                            |
| Tiền điều kiện     | Có hóa đơn chưa thanh toán                                                                                                    |
| Kích hoạt          | Người dùng chọn thanh toán                                                                                                    |
| Đảm bảo tối thiểu  | Không thanh toán nếu lỗi                                                                                                      |
| Đảm bảo thành công | Hóa đơn được cập nhật Paid                                                                                                    |
| Luồng chính        | 1. Xem hóa đơn<br/>2. Chọn<br/>3. Chọn phương thức<br/>4. Gửi request<br/>5. Gọi gateway<br/>6. Callback<br/>7. Update Paid<br/>8. Lưu transaction |
| Luồng ngoại lệ     | 5A. Gateway <br/>1. Thanh toán thất bại<br/>2. Retry hoặc dừng<br/>3. Ghi log<br/><br/>6A. Hết hạn hóa đơn<br/><br/>1. Hóa đơn hết hạn<br/>2. Từ chối thanh toán<br/>3. Thông báo<br/>4. Kết thúc |
| Quy tắc ngiệp vụ   | Không thanh toán trùng |

---

## 5. Use Case: Duyệt đăng ký phòng

| Thuộc tính         | Nội dung                                                                                               |
| :----------------- | :----------------------------------------------------------------------------------------------------- |
| Tên Usecase        | Duyệt đăng ký phòng                                                                                    |
| Mức                | Mức người dùng                                                                                         |
| Tác nhân chính     | Admin                                                                                                  |
| Các bên liên quan  | Admin, Sinh viên                                                                                       |
| Mục tiêu           | Xử lý yêu cầu đăng ký                                                                                  |
| Tiền điều kiện     | Có yêu cầu Pending                                                                                     |
| Kích hoạt          | Admin truy cập danh sách                                                                               |
| Đảm bảo tối thiểu  | Không xử lý nếu dữ liệu sai                                                                            |
| Đảm bảo thành công | Cập nhật trạng thái                                                                                    |
| Luồng chính        | 1. Xem danh sách<br/>2. Chọn request<br/>3. Kiểm tra<br/>4. Approve/Reject<br/>5. Update<br/>6. Gửi thông báo<br/>7. Log |
| Luồng ngoại lệ     | 3A. Request không tồn tại <br/>1. Không tìm thấy request<br/>2. Thông báo lỗi<br/>3. Kết thúc<br/><br/>4A. Phòng đầy<br/><br/>1. Kiểm tra slot<br/>2. Không đủ chỗ<br/>3. Từ chối<br/>4. Kết thúc|
| Quy tắc ngiệp vụ   | Phải kiểm tra slot trước khi duyệt |

---

## 6. Use Case: Check-in

| Thuộc tính         | Nội dung                                                                           |
| :----------------- | :--------------------------------------------------------------------------------- |
| Tên Usecase        | Check-in                                                                           |
| Mức                | Mức người dùng                                                                     |
| Tác nhân chính     | Quản lý                                                                            |
| Các bên liên quan  | Sinh viên                                                                          |
| Mục tiêu           | Xác nhận vào ở                                                                     |
| Tiền điều kiện     | Đã được duyệt                                                                      |
| Kích hoạt          | Sinh viên đến KTX                                                                  |
| Đảm bảo tối thiểu  | Không check-in nếu chưa duyệt                                                      |
| Đảm bảo thành công | Cập nhật trạng thái ở                                                              |
| Luồng chính        | 1. Tra cứu SV<br/>2. Kiểm tra trạng thái<br/>3. Xác minh<br/>4. Check-in<br/>5. Update<br/>6. Log |
| Luồng ngoại lệ     | 2A. Không có đăng ký hợp lệ <br/>1. Không tìm thấy trạng thái Approved<br/>2. Từ chối check-in<br/>3. Thông báo<br/>4. Kết thúc|
| Quy tắc ngiệp vụ   | Chỉ check-in khi đã approved |

---

## 7. Use Case: Check-out

| Thuộc tính         | Nội dung                                                                          |
| :----------------- | :-------------------------------------------------------------------------------- |
| Tên Usecase        | Check-out                                                                         |
| Mức                | Mức người dùng                                                                    |
| Tác nhân chính     | Sinh viên                                                                         |
| Các bên liên quan  | Hệ thống                                                                          |
| Mục tiêu           | Trả phòng                                                                         |
| Tiền điều kiện     | Không có công nợ                                                                  |
| Kích hoạt          | Sinh viên gửi yêu cầu                                                             |
| Đảm bảo tối thiểu  | Không checkout nếu còn nợ                                                         |
| Đảm bảo thành công | Phòng được giải phóng                                                             |
| Luồng chính        | 1. Gửi request<br/>2. Kiểm tra nợ<br/>3. Kiểm tra phòng<br/>4. Xác nhận<br/>5. Update<br/>6. Log |
| Luồng ngoại lệ     | 2A. Còn công nợ<br/>1. Kiểm tra phát hiện còn nợ<br/>2. Từ chối checkout<br/>3. Thông báo<br/>4. Kết thúc |
| Quy tắc ngiệp vụ   | Phải thanh toán hết trước khi checkout |

---

## 8. Use Case: Chuyển phòng

| Thuộc tính         | Nội dung                                                                      |
| :----------------- | :---------------------------------------------------------------------------- |
| Tên Usecase        | Chuyển phòng                                                                  |
| Mức                | Mức người dùng                                                                |
| Tác nhân chính     | Sinh viên                                                                     |
| Các bên liên quan  | Admin                                                                         |
| Mục tiêu           | Chuyển sang phòng khác                                                        |
| Tiền điều kiện     | Đủ điều kiện chuyển                                                           |
| Kích hoạt          | Sinh viên gửi yêu cầu                                                         |
| Đảm bảo tối thiểu  | Không chuyển nếu không hợp lệ                                                 |
| Đảm bảo thành công | Cập nhật phòng mới                                                            |
| Luồng chính        | 1. Gửi yêu cầu<br/>2. Kiểm tra<br/>3. Xem phòng<br/>4. Chọn<br/>5. Admin duyệt<br/>6. Update |
| Luồng ngoại lệ     | 2A. Không đủ điều kiện <br/>1. Không thỏa điều kiện chuyển phòng<br/>2. Từ chối<br/>3. Thông báo<br/>4. Kết thúc<br/><br/>3A. Không có phòng<br/><br/>1. Không tìm thấy phòng phù hợp<br/>2. Thông báo<br/>3. Kết thúc |
| Quy tắc ngiệp vụ   | Phải được admin duyệt |

---

## 9. Use Case: Quản lý phòng

| Thuộc tính         | Nội dung                                                             |
| :----------------- | :------------------------------------------------------------------- |
| Tên Usecase        | Quản lý phòng                                                        |
| Mức                | Mức người dùng                                                       |
| Tác nhân chính     | Admin                                                                |
| Các bên liên quan  | Hệ thống                                                             |
| Mục tiêu           | Quản lý thông tin phòng                                              |
| Tiền điều kiện     | Admin đăng nhập                                                      |
| Kích hoạt          | Truy cập module                                                      |
| Đảm bảo tối thiểu  | Không lưu dữ liệu sai                                                |
| Đảm bảo thành công | Cập nhật thành công                                                  |
| Luồng chính        | 1. Truy cập<br/>2. CRUD<br/>3. Validate<br/>4. Lưu DB<br/>5. Clear cache<br/>6. Log |
| Luồng ngoại lệ     | 3A. Trùng mã phòng <br/>1. Phát hiện mã đã tồn tại<br/>2. Từ chối lưu<br/>3. Thông báo<br/><br/>4A. Phòng đang sử dụng<br/>1. Phòng có người<br/>2. Không cho xóa/sửa<br/>3. Thông báo | 
| Quy tắc ngiệp vụ   | Mã phòng phải duy nhất |

---

## 10. Use Case: Quản lý sinh viên

| Thuộc tính         | Nội dung                                                                 |
| :----------------- | :----------------------------------------------------------------------- |
| Tên Usecase        | Quản lý sinh viên                                                        |
| Mức                | Mức người dùng                                                           |
| Tác nhân chính     | Admin                                                                    |
| Các bên liên quan  | Hệ thống                                                                 |
| Mục tiêu           | Quản lý thông tin sinh viên                                              |
| Tiền điều kiện     | Admin đăng nhập                                                          |
| Kích hoạt          | Tìm kiếm sinh viên                                                       |
| Đảm bảo tối thiểu  | Không trả dữ liệu sai                                                    |
| Đảm bảo thành công | Hiển thị và cập nhật dữ liệu                                             |
| Luồng chính        | 1. Tìm kiếm<br/>2. Query<br/>3. Hiển thị<br/>4. Xem chi tiết<br/>5. Cập nhật<br/>6. Lưu |
| Luồng ngoại lệ     | 2A. Không có dữ liệu <br/>1. Query trả về rỗng<br/>2. Thông báo<br/>3. Kết thúc |
| Quy tắc ngiệp vụ   | Dữ liệu phải hợp lệ |

---

## 11. Use Case: Báo cáo sự cố

| Thuộc tính         | Nội dung                                                                                     |
| :----------------- | :------------------------------------------------------------------------------------------- |
| Tên Usecase        | Báo cáo sự cố                                                                                |
| Mức                | Mức người dùng                                                                               |
| Tác nhân chính     | Sinh viên                                                                                    |
| Các bên liên quan  | Admin, Kỹ thuật                                                                              |
| Mục tiêu           | Gửi và xử lý sự cố                                                                           |
| Tiền điều kiện     | Sinh viên đang ở KTX                                                                         |
| Kích hoạt          | Tạo ticket                                                                                   |
| Đảm bảo tối thiểu  | Không tạo nếu thiếu thông tin                                                                |
| Đảm bảo thành công | Ticket được xử lý                                                                            |
| Luồng chính        | 1. Tạo ticket<br/>2. Upload ảnh<br/>3. Lưu<br/>4. Assign<br/>5. Xử lý<br/>6. Update trạng thái<br/>7. Đánh giá |
| Luồng ngoại lệ     | 2A. Thiếu thông tin <br/>1. Không đủ dữ liệu ticket<br/>2. Từ chối tạo<br/>3. Thông báo<br/><br/>5A. Không xử lý được<br/><br/>1. Kỹ thuật không sửa được<br/>2. Chuyển trạng thái Waiting<br/>3. Thông báo |
| Quy tắc ngiệp vụ   | Phải theo dõi lifecycle ticket |

---


---

| Thuộc tính         | Nội dung                                                                                                                                                                                   |
| :----------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Tên Usecase        | |
| Mức                | |
| Tác nhân chính     | |
| Các bên liên quan  | |
| Mục tiêu           | |
| Tiền điều kiện     | |
| Kích hoạt          | |
| Đảm bảo tối thiểu  | |
| Đảm bảo thành công | |
| Luồng chính        | |
| Luồng ngoại lệ     | |
| Quy tắc ngiệp vụ   | |