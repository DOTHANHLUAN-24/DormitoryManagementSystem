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

### Luồng chính	
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

### Luồng Ngoại Lệ	
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

### Luồng chính	
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

### Luồng Ngoại Lệ	
    1A. Chọn "Đăng ký theo nhóm": Nhập danh sách MSV, kiểm tra điều kiện, rồi chọn phòng
    3A. Sinh viên đã có phòng: Thông báo, kết thúc
    3B. Chưa đóng học phí: Thông báo, kết thúc
    4A. Phòng hết chỗ: Thông báo, quay lại bước 2
    4B. Phòng đang bị đặt tạm: Thông báo, quay lại bước 2
    4C. Hết hạn đăng ký: Thông báo, kết thúc
    6A. Để trống thông tin: Thông báo, quay lại bước 5
    7A. Lỗi hệ thống: Thông báo, không khóa phòng, kết thúc

## UC-03: TÍNH TIỀN ĐIỆN NƯỚC HÀNG THÁNG
Tiêu đề	                        Nội dung
Tên Use case	                Tính tiền điện nước hàng tháng
Mức	                            Mức hệ thống 
Tác nhân chính	                Quản lý KTX (kích hoạt), Hệ thống (tự động)
Các bên liên quan và lợi ích	- Nhân viên: Muốn tính nhanh, chính xác
                                - Sinh viên: Muốn hóa đơn minh bạch
                                - Hệ thống: Đảm bảo nhất quán dữ liệu
Người chịu trách nhiệm	        Hệ thống quản lý ký túc xá
Tiền điều kiện	                Đã có chỉ số điện/nước đầu kỳ và cuối kỳ. Đã có đơn giá.
Đảm bảo tối thiểu	            - Không tạo hóa đơn nếu có lỗi
                                - Ghi log lỗi để xử lý sau
Đảm bảo thành công	            - Tạo hóa đơn cho từng sinh viên
                                - Cập nhật công nợ
                                - Gửi thông báo email

### Luồng chính	
1. Kích hoạt "Tính hóa đơn tháng X"
2. Lấy danh sách phòng đang có sinh viên ở
3. Với mỗi phòng, lấy chỉ số đầu kỳ và cuối kỳ
4. Tính lượng tiêu thụ = cuối - đầu
5. Tính thành tiền = lượng × đơn giá
6. Chia đều cho số sinh viên trong phòng
7. Tạo hóa đơn, trạng thái "Chưa thanh toán"
8. Cập nhật công nợ và gửi thông báo
9. Kết thúc

### Luồng Ngoại Lệ	
    1A. Tính lại tháng cũ: Cảnh báo, xóa hóa đơn cũ, tính lại, ghi log
    3A. Chỉ số cuối < chỉ số đầu: Báo lỗi, bỏ qua phòng đó, báo cáo cuối đợt
    3B. Thiếu chỉ số cuối kỳ: Bỏ qua phòng, ghi vào danh sách lỗi
    3C. Phòng không có sinh viên: Bỏ qua, không tạo hóa đơn
    5A. Chưa có đơn giá: Thông báo, dừng toàn bộ
    7A. Lỗi CSDL: Rollback, thông báo, ghi log

## UC-04: XEM VÀ THANH TOÁN HÓA ĐƠN
Tiêu đề	                        Nội dung
Tên Use case	                Xem và thanh toán hóa đơn điện/nước
Mức	                            Mức người dùng (User-goal level)
Tác nhân chính	                Sinh viên
Tác nhân phụ	                Quản lý KTX
Các bên liên quan và lợi ích	- Sinh viên: Muốn xem và thanh toán hóa đơn tiện lợi
                                - Nhân viên: Muốn ghi nhận thanh toán chính xác
Người chịu trách nhiệm	        Hệ thống quản lý ký túc xá
Tiền điều kiện	                Sinh viên đã đăng nhập. Hóa đơn đã được tạo.
Đảm bảo tối thiểu	            - Hóa đơn không đổi trạng thái nếu thanh toán thất bại
                                - Thông báo lỗi rõ ràng
Đảm bảo thành công	            - Hóa đơn chuyển "Đã thanh toán"
                                - Cập nhật công nợ
                                - Tạo biên lai PDF và gửi email

### Luồng chính	
1. Sinh viên chọn "Hóa đơn điện/nước"
2. Hệ thống hiển thị danh sách hóa đơn
3. Sinh viên xem chi tiết hóa đơn
4. Sinh viên nhấn "Thanh toán"
5. Hệ thống hiển thị phương thức thanh toán
6. Sinh viên chọn phương thức và xác nhận
7. Hệ thống xử lý thanh toán
8. Hệ thống cập nhật trạng thái, tạo biên lai PDF
9. Hệ thống gửi email xác nhận
10. Kết thúc

### Luồng Ngoại Lệ	
    1A. Thanh toán nhiều hóa đơn: Chọn nhiều hóa đơn, thanh toán gộp một lần
    3A. Hóa đơn đã thanh toán: Vô hiệu nút thanh toán, thông báo
    4A. Hóa đơn quá hạn: Tự động tính phí phạt, hiển thị tổng tiền
    6A. Thanh toán online thất bại: Thông báo lỗi, không cập nhật, quay lại bước 4

## UC-05: DUYỆT ĐĂNG KÝ PHÒNG (ADMIN)
Tiêu đề	                                Nội dung
Tên Use case	                        Duyệt đăng ký phòng
Mức	                                    Mức người dùng 
Tác nhân chính	                        Quản lý KTX (Admin)
Các bên liên quan và lợi ích	        - Quản lý: Muốn phê duyệt đơn nhanh chóng, chính xác
                                        - Sinh viên: Muốn đơn được xử lý kịp thời
Người chịu trách nhiệm	                Hệ thống quản lý ký túc xá
Tiền điều kiện	                        Quản lý đã đăng nhập. Có đơn ở trạng thái "Chờ duyệt".
Đảm bảo tối thiểu	                    - Đơn không đổi trạng thái nếu duyệt thất bại
                                        - Thông báo lỗi cho quản lý
Đảm bảo thành công	                    - Phê duyệt: tạo hợp đồng, cập nhật phòng, thông báo sinh viên
                                        - Từ chối: mở khóa phòng, thông báo kèm lý do

### Chuỗi sự kiện chính	
1. Quản lý chọn "Duyệt đăng ký phòng"
2. Hệ thống hiển thị danh sách đơn "Chờ duyệt"
3. Quản lý chọn đơn để xem chi tiết
4. Quản lý chọn "Phê duyệt"
5. Hệ thống kiểm tra lại phòng
6. Hệ thống tạo hợp đồng, cập nhật phòng
7. Hệ thống gửi email thông báo
8. Kết thúc

### Ngoại lệ	
    1A. Duyệt hàng loạt: Chọn nhiều đơn, xử lý lần lượt, báo cáo kết quả
    3A. Chọn "Từ chối": Nhập lý do, mở khóa phòng, gửi email, kết thúc
    3B. Chọn "Yêu cầu bổ sung": Nhập yêu cầu, cập nhật trạng thái "Chờ bổ sung", gửi thông báo
    5A. Phòng hết chỗ: Thông báo, đề xuất phòng thay thế
    5B. Sinh viên đã có phòng: Thông báo, tự động từ chối

## UC-06: CHECK-IN / NHẬN PHÒNG
Tiêu đề	                                Nội dung
Tên Use case	                        Check-in / Nhận phòng
Mức	                                    Mức người dùng 
Tác nhân chính	                        Nhân viên tiếp tân
Tác nhân phụ	                        Sinh viên
Các bên liên quan và lợi ích	        - Sinh viên: Muốn nhận phòng nhanh chóng
                                        - Nhân viên: Muốn thủ tục chính xác, tránh sai sót
Người chịu trách nhiệm	                Hệ thống quản lý ký túc xá
Tiền điều kiện	                        Sinh viên có đơn được duyệt. Hợp đồng đã tạo. Chưa check-in.
Đảm bảo tối thiểu	                    - Không cập nhật phòng nếu check-in thất bại
                                        - Thông báo lỗi cho nhân viên
Đảm bảo thành công	                    - Sinh viên chính thức ở phòng
                                        - Cập nhật danh sách phòng
                                        - Phát thẻ từ/chìa khóa, in phiếu nhận phòng

### Chuỗi sự kiện chính	
1. Sinh viên xuất trình MSSV và CMND
2. Nhân viên chọn "Check-in", nhập MSSV
3. Hệ thống hiển thị thông tin sinh viên và đơn đã duyệt
4. Nhân viên kiểm tra giấy tờ và xác nhận
5. Nhân viên nhấn "Xác nhận nhận phòng"
6. Hệ thống cập nhật trạng thái sinh viên và phòng
7. Nhân viên phát chìa khóa/thẻ từ, in phiếu
8. Kết thúc
### Ngoại lệ	
    1A. Check-in theo nhóm: Check-in đồng loạt cho cả phòng
    3A. Chưa có đơn duyệt: Thông báo, không cho check-in
    3B. Đã check-in trước đó: Thông báo, kiểm tra trùng lặp
    3C. Phòng đã đủ người: Thông báo lỗi, kiểm tra lại dữ liệu
    4A. Thiếu giấy tờ: Yêu cầu bổ sung, tạm dừng

## UC-07: CHECK-OUT / TRẢ PHÒNG
Tiêu đề	                                        Nội dung
Tên Use case	                                Check-out / Trả phòng
Mức	                                            Mức người dùng 
Tác nhân chính	                                Nhân viên tiếp tân
Tác nhân phụ	                                Sinh viên
Các bên liên quan và lợi ích	                - Sinh viên: Muốn trả phòng nhanh, nhận lại cọc (nếu có)
                                                - Nhân viên: Muốn kiểm tra tài sản và thanh lý chính xác
Người chịu trách nhiệm	                        Hệ thống quản lý ký túc xá
Tiền điều kiện	                                Sinh viên đang ở phòng. Hợp đồng còn hiệu lực hoặc đã kết thúc.
Đảm bảo tối thiểu	                            - Không cập nhật trạng thái nếu check-out thất bại
                                                - Thông báo lỗi cho nhân viên
Đảm bảo thành công	                            - Sinh viên không còn ở phòng
                                                - Cập nhật phòng (tăng chỗ trống)
                                                - Xử lý tiền cọc và in phiếu thanh lý

### Chuỗi sự kiện chính	
1. Sinh viên đến quầy thông báo trả phòng
2. Nhân viên chọn "Check-out", nhập MSV
3. Hệ thống hiển thị thông tin sinh viên, phòng, công nợ, tiền cọc
4. Nhân viên kiểm tra phòng (tài sản hư hỏng)
5. Nhân viên nhập kết quả kiểm tra
6. Hệ thống tính toán thanh lý (công nợ, cọc hoàn trả)
7. Sinh viên thanh toán hoặc nhận lại tiền
8. Nhân viên nhấn "Xác nhận trả phòng"
9. Hệ thống cập nhật trạng thái, thu lại chìa khóa
10. In phiếu thanh lý, kết thúc

### Ngoại lệ	
    1A. Check-out theo nhóm: Xử lý chung tiền cọc và công nợ cho cả phòng
    3A. Check-out sớm trước hạn: Tính phí phạt (nếu có quy định)
    5A. Còn nợ tiền: Yêu cầu thanh toán hết nợ trước khi check-out
    5B. Hư hỏng tài sản: Tính chi phí sửa chữa, khấu trừ cọc, lập biên bản
    5C. Mất chìa khóa/thẻ từ: Thu phí làm lại, khấu trừ cọc

## UC-08: CHUYỂN PHÒNG
Tiêu đề	                                        Nội dung
Tên Use case	                                Chuyển phòng
Mức	                                            Mức người dùng
Tác nhân chính	                                Sinh viên
Tác nhân phụ	                                Quản lý KTX
Các bên liên quan và lợi ích	                - Sinh viên: Muốn chuyển sang phòng khác khi có nhu cầu (xung đột, hỏng phòng, nâng cấp)
                                                - Quản lý: Muốn kiểm soát việc chuyển phòng hợp lý
Người chịu trách nhiệm	                        Hệ thống quản lý ký túc xá
Tiền điều kiện	                                Sinh viên đang ở phòng hiện tại. Có phòng đích còn trống.
Đảm bảo tối thiểu	                            - Sinh viên vẫn ở phòng cũ nếu chuyển thất bại
                                                - Thông báo lỗi chi tiết
Đảm bảo thành công	                            - Sinh viên chuyển sang phòng mới
                                                - Cập nhật phòng cũ (tăng chỗ trống)
                                                - Cập nhật phòng mới (giảm chỗ trống)

### Chuỗi sự kiện chính	
1. Sinh viên gửi yêu cầu chuyển phòng
2. Quản lý xem xét và chấp thuận (hoặc sinh viên tự chọn nếu được phép)
3. Hệ thống hiển thị danh sách phòng trống
4. Sinh viên/Quản lý chọn phòng đích
5. Hệ thống kiểm tra phòng đích còn trống
6. Hệ thống cập nhật: xóa sinh viên khỏi phòng cũ, thêm vào phòng mới
7. Hệ thống ghi nhận lịch sử chuyển phòng
8. Gửi thông báo xác nhận cho sinh viên
9. Kết thúc

### Ngoại lệ	
    1A. Chuyển phòng do hỏng hóc: Ưu tiên xử lý nhanh, không tính phí (nếu có)
    4A. Phòng đích không còn trống: Thông báo, đề xuất phòng khác, quay lại bước 3
    4B. Sinh viên còn nợ tiền: Yêu cầu thanh toán nợ trước khi chuyển
    5A. P`hòng đích đang sửa chữa: Thông báo, không cho chuyển, đề xuất phòng khác



