# 📋 BÁO CÁO UML - NHÓM VẬN HÀNH & THANH TOÁN
## Thành viên thực hiện: ĐỖ QUANG HUY
## Ngày hoàn thành: 22/04/2026

---

## 📌 MỤC LỤC
1. [UC21: Thanh toán hóa đơn](#1-uc21-thanh-toán-hóa-đơn)
2. [UC14: Ghi nhận vi phạm](#2-uc14-ghi-nhận-vi-phạm)
3. [UC15: Quản lý khách thăm](#3-uc15-quản-lý-khách-thăm)
4. [Mã nguồn PlantUML](#4-mã-nguồn-plantuml)

---

# 1. UC21: THANH TOÁN HÓA ĐƠN

## 1.1. Sơ đồ Use Case

![UC21 - Thanh toán hóa đơn](../images/UC21_ThanhToanHoaDon.jpg)

## 1.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Thanh toán hóa đơn |
| Mức | Mức người dùng |
| Tác nhân chính | Sinh viên |
| Các bên liên quan | Sinh viên, Nhân viên quản lý, Cổng thanh toán, Hệ thống |
| Mục tiêu | Cho phép sinh viên xem danh sách hóa đơn còn nợ và thực hiện thanh toán trực tuyến |
| Tiền điều kiện | Sinh viên đã đăng nhập; có ít nhất một hóa đơn ở trạng thái Chưa thanh toán; hệ thống hoạt động bình thường |
| Kích hoạt | Sinh viên chọn trình đơn "Thanh toán" hoặc nhấp vào thông báo "Bạn có hóa đơn chưa thanh toán" |
| Đảm bảo tối thiểu | Không thực hiện giao dịch nếu dữ liệu không hợp lệ; giữ nguyên trạng thái hóa đơn khi thanh toán thất bại |
| Đảm bảo thành công | Hóa đơn chuyển sang Đã thanh toán; sinh viên nhận biên lai điện tử qua Email |
| Luồng chính | 1. Sinh viên truy cập chức năng "Thanh toán"<br/>2. Hệ thống kiểm tra danh sách hóa đơn Chưa thanh toán<br/>3. Hiển thị danh sách: Mã hóa đơn, Kỳ thanh toán, Tổng tiền, Hạn thanh toán<br/>4. Sinh viên chọn một hóa đơn từ danh sách<br/>5. Hệ thống hiển thị cửa sổ Xác nhận thanh toán<br/>6. Sinh viên nhấn "Xác nhận thanh toán"<br/>7. Hệ thống kiểm tra dữ liệu thanh toán<br/>8. Gửi yêu cầu đến Cổng thanh toán<br/>9. Cổng thanh toán xử lý giao dịch<br/>10. Hệ thống nhận kết quả Thành công từ cổng thanh toán<br/>11. Cập nhật trạng thái hóa đơn thành Đã thanh toán<br/>12. Ghi nhận Mã giao dịch và thời gian thanh toán vào Cơ sở dữ liệu<br/>13. Tạo và gửi Biên lai điện tử qua Email<br/>14. Hiển thị màn hình "Thanh toán thành công" |
| Luồng ngoại lệ | **2A. Không có hóa đơn Chưa thanh toán**<br/>1. Tại bước 2 hệ thống kiểm tra danh sách hóa đơn<br/>2. Điều kiện: danh sách hóa đơn Chưa thanh toán rỗng<br/>3. Hệ thống dừng luồng xử lý<br/>4. Hiển thị thông báo: "Bạn không có hóa đơn nào cần thanh toán."<br/>5. Hệ thống ẩn nút "Thanh toán"<br/>6. Kết thúc<br/><br/>**6A. Hóa đơn đã được thanh toán (Trùng giao dịch)**<br/>1. Tại bước 6 sinh viên nhấn "Xác nhận thanh toán"<br/>2. Sang bước 7 hệ thống kiểm tra lại trạng thái hóa đơn<br/>3. Điều kiện: hóa đơn đã chuyển sang Đã thanh toán hoặc Đang xử lý<br/>4. Hệ thống dừng luồng xử lý<br/>5. Hiển thị thông báo: "Hóa đơn này đã được thanh toán hoặc đang được xử lý."<br/>6. Hệ thống tự động làm mới danh sách hóa đơn<br/>7. Quay lại bước 3<br/><br/>**9A. Số dư không đủ**<br/>1. Tại bước 9 cổng thanh toán xử lý giao dịch<br/>2. Điều kiện: số dư tài khoản không đủ<br/>3. Cổng thanh toán trả về mã lỗi KHÔNG_ĐỦ_TIỀN<br/>4. Hệ thống nhận phản hồi và dừng quá trình thanh toán<br/>5. Hiển thị thông báo: "Thanh toán thất bại: Số dư tài khoản không đủ."<br/>6. Ghi nhật ký lỗi<br/>7. Quay lại bước 5<br/><br/>**9B. Thẻ bị từ chối / Hết hạn**<br/>1. Tại bước 9 cổng thanh toán xử lý giao dịch<br/>2. Điều kiện: thẻ bị từ chối hoặc đã hết hạn<br/>3. Cổng thanh toán trả về mã lỗi THẺ_BỊ_TỪ_CHỐI hoặc THẺ_HẾT_HẠN<br/>4. Hệ thống nhận phản hồi và dừng quá trình thanh toán<br/>5. Hiển thị thông báo: "Thanh toán thất bại: Thẻ bị từ chối hoặc đã hết hạn."<br/>6. Ghi nhật ký lỗi<br/>7. Quay lại bước 5<br/><br/>**9C. Hết thời gian chờ**<br/>1. Tại bước 9 sau 30 giây không nhận được phản hồi<br/>2. Hệ thống dừng yêu cầu và coi giao dịch đang treo<br/>3. Cập nhật trạng thái hóa đơn thành Chờ đối soát<br/>4. Hiển thị thông báo: "Giao dịch đang chờ xử lý. Vui lòng kiểm tra lại sau 5 phút."<br/>5. Ghi nhật ký cảnh báo<br/>6. Gửi email thông báo cho Nhân viên<br/>7. Kết thúc<br/><br/>**11A. Lỗi Cơ sở dữ liệu khi cập nhật**<br/>1. Tại bước 11 hệ thống cập nhật trạng thái hóa đơn<br/>2. Điều kiện: không thể kết nối Cơ sở dữ liệu<br/>3. Hệ thống hoàn tác giao dịch (nếu có)<br/>4. Ghi nhật ký LỖI NGHIÊM TRỌNG<br/>5. Hiển thị thông báo: "Hệ thống đang gặp sự cố. Vui lòng liên hệ quản lý để xác nhận."<br/>6. Gửi cảnh báo đến Nhân viên<br/>7. Kết thúc |
| Quy tắc nghiệp vụ | QL01: Mỗi hóa đơn chỉ được thanh toán 1 lần<br/>QL02: Số tiền thanh toán phải khớp chính xác với Tổng tiền của hóa đơn<br/>QL03: Thanh toán sau Hạn thanh toán sẽ bị tính thêm phí phạt |

## 1.3. Sơ đồ hoạt động - AD07

![AD07 - Thanh toán hóa đơn](../images/AD07_ThanhToanHoaDon.jpg)

## 1.4. Sơ đồ tuần tự - SD07

![SD07 - Xử lý thanh toán hóa đơn](../images/SD07_ThanhToanHoaDon.jpg)

# 2. UC14: GHI NHẬN VI PHẠM

## 2.1. Sơ đồ Use Case

![UC14 - Ghi nhận vi phạm](../images/UC14_GhiNhanViPham.jpg)

## 2.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Ghi nhận vi phạm |
| Mức | Mức nghiệp vụ |
| Tác nhân chính | Nhân viên quản lý |
| Các bên liên quan | Nhân viên quản lý, Sinh viên, Ban quản lý KTX, Hệ thống |
| Mục tiêu | Ghi lại hành vi vi phạm nội quy KTX của sinh viên vào hệ thống để làm cơ sở đánh giá hạnh kiểm và xử phạt |
| Tiền điều kiện | Nhân viên đã đăng nhập với quyền Nhân viên; danh mục loại vi phạm đã được cấu hình; hệ thống hoạt động bình thường |
| Kích hoạt | Nhân viên chọn chức năng "Quản lý vi phạm" → "Ghi nhận mới" |
| Đảm bảo tối thiểu | Không tạo bản ghi nếu dữ liệu không hợp lệ; không lưu bản ghi sai lệch |
| Đảm bảo thành công | Tạo bản ghi vi phạm thành công; sinh viên nhận được thông báo |
| Luồng chính | 1. Nhân viên truy cập chức năng "Ghi nhận vi phạm"<br/>2. Nhân viên nhập hoặc quét Mã số sinh viên<br/>3. Hệ thống truy vấn và hiển thị thông tin cơ bản<br/>4. Nhân viên chọn Loại vi phạm từ danh sách thả xuống<br/>5. Nhân viên nhập Mô tả chi tiết và Mức phạt<br/>6. Nhân viên nhấn "Lưu biên bản"<br/>7. Hệ thống kiểm tra dữ liệu đầu vào<br/>8. Hệ thống kiểm tra điều kiện ghi Cơ sở dữ liệu<br/>9. Tạo bản ghi mới trong bảng Vi phạm<br/>10. Ghi Cơ sở dữ liệu<br/>11. Gửi Thông báo đến Ứng dụng của Sinh viên<br/>12. Hiển thị thông báo "Đã ghi nhận vi phạm thành công" |
| Luồng ngoại lệ | **3A. Mã sinh viên không tồn tại**<br/>1. Tại bước 3 hệ thống truy vấn không tìm thấy<br/>2. Hiển thị: "Không tìm thấy sinh viên có mã này."<br/>3. Xóa nội dung ô nhập liệu<br/>4. Quay lại bước 2<br/><br/>**3B. Sinh viên đã rời KTX**<br/>1. Tại bước 3 phát hiện trạng thái = Đã rời đi<br/>2. Hiển thị: "Sinh viên này đã rời khỏi KTX."<br/>3. Vô hiệu hóa nút "Lưu"<br/>4. Kết thúc<br/><br/>**7A. Chưa chọn Loại vi phạm**<br/>1. Tại bước 7 phát hiện chưa chọn loại vi phạm<br/>2. Tô viền đỏ danh sách thả xuống<br/>3. Hiển thị: "Vui lòng chọn Loại vi phạm."<br/>4. Quay lại bước 4<br/><br/>**7B. Chưa nhập Mô tả**<br/>1. Tại bước 7 phát hiện trường Mô tả rỗng<br/>2. Tô viền đỏ vùng nhập<br/>3. Hiển thị: "Vui lòng nhập mô tả chi tiết."<br/>4. Quay lại bước 5<br/><br/>**7C. Mức phạt vượt giới hạn**<br/>1. Tại bước 7 phát hiện Mức phạt > Mức tối đa<br/>2. Tô viền đỏ ô nhập<br/>3. Hiển thị: "Mức phạt vượt quá giới hạn."<br/>4. Quay lại bước 5<br/><br/>**10A. Lỗi Cơ sở dữ liệu**<br/>1. Tại bước 10 xảy ra lỗi khi ghi<br/>2. Hoàn tác giao dịch<br/>3. Hiển thị: "Lỗi hệ thống, vui lòng thử lại."<br/>4. Giữ nguyên dữ liệu biểu mẫu<br/>5. Ghi nhật ký ngoại lệ<br/>6. Quay lại bước 5 |
| Quy tắc nghiệp vụ | QL01: Không thể ghi nhận vi phạm cho sinh viên đã rời KTX<br/>QL02: Mức phạt không vượt quá Mức tối đa quy định<br/>QL03: Chỉ được sửa/xóa trong vòng 24 giờ |

## 2.3. Sơ đồ hoạt động - AD14

![AD14 - Ghi nhận vi phạm](../images/AD14_GhiNhanViPham.jpg)

## 2.4. Sơ đồ tuần tự - SD14

![SD14 - Ghi nhận vi phạm](../images/SD14_GhiNhanViPham.jpg)


# 3. UC15: QUẢN LÝ KHÁCH THĂM

## 3.1. Sơ đồ Use Case

![UC15 - Quản lý khách thăm](../images/UC15_QuanLyKhachTham.jpg)

## 3.2. Đặc tả Use Case

| Thuộc tính | Nội dung |
|:---|:---|
| Tên Usecase | Quản lý khách thăm |
| Mức | Mức nghiệp vụ |
| Tác nhân chính | Nhân viên quản lý |
| Các bên liên quan | Nhân viên quản lý, Sinh viên, Khách thăm, Ban quản lý, Hệ thống |
| Mục tiêu | Kiểm soát lượng người ra vào KTX, ghi nhận thông tin khách đến thăm và giới hạn số lượng khách/phòng |
| Tiền điều kiện | Nhân viên quản lý đã đăng nhập; trong khung giờ cho phép thăm; hệ thống hoạt động bình thường |
| Kích hoạt | Có khách đến quầy yêu cầu vào thăm. Nhân viên quản lý chọn chức năng "Nhận khách vào thăm" |
| Đảm bảo tối thiểu | Từ chối nhận khách nếu dữ liệu không hợp lệ, phòng đầy khách hoặc sinh viên không tồn tại |
| Đảm bảo thành công | Tạo bản ghi Khách thăm với trạng thái Đã vào; in phiếu thăm có mã QR cho khách |
| Luồng chính | 1. Nhân viên truy cập chức năng "Nhận khách vào thăm"<br/>2. Nhân viên nhập/quét CCCD của khách<br/>3. Hệ thống kiểm tra CCCD<br/>4. Hệ thống tự động điền thông tin khách<br/>5. Nhân viên nhập Mã SV hoặc Số phòng<br/>6. Hệ thống truy vấn và kiểm tra trạng thái<br/>7. Hệ thống kiểm tra số lượng khách trong phòng<br/>8. (Số khách < 3) Tạo bản ghi Khách thăm (Đã vào)<br/>9. Ghi Cơ sở dữ liệu<br/>10. In phiếu thăm có mã QR<br/>11. Hiển thị "Nhận khách thành công"<br/>12. Nhân viên đưa phiếu cho khách<br/>13. (Khách ra về) Nhân viên quét mã QR<br/>14. Hệ thống cập nhật trạng thái Đã ra |
| Luồng ngoại lệ | **3A. CCCD sai định dạng**<br/>1. Tại bước 3 phát hiện sai định dạng<br/>2. Tô viền đỏ ô nhập<br/>3. Hiển thị: "Số CCCD không hợp lệ."<br/>4. Quay lại bước 2<br/><br/>**3B. CCCD hết hạn**<br/>1. Tại bước 3 phát hiện CCCD hết hạn<br/>2. Hiển thị: "CCCD đã hết hạn."<br/>3. Vô hiệu hóa nút tiếp tục<br/>4. Kết thúc<br/><br/>**3C. Khách trong Danh sách đen**<br/>1. Tại bước 3 phát hiện khách bị cấm<br/>2. Hiển thị cảnh báo đỏ: "CẢNH BÁO: Khách nằm trong danh sách cấm!"<br/>3. Vô hiệu hóa form<br/>4. Gửi thông báo cho Quản lý<br/>5. Kết thúc<br/><br/>**6A. Mã SV không tồn tại**<br/>1. Tại bước 6 không tìm thấy sinh viên<br/>2. Hiển thị: "Không tìm thấy sinh viên."<br/>3. Xóa ô nhập<br/>4. Quay lại bước 5<br/><br/>**6B. Sinh viên đã rời KTX**<br/>1. Tại bước 6 phát hiện trạng thái = Đã rời đi<br/>2. Hiển thị: "Sinh viên không còn cư trú tại KTX."<br/>3. Đặt lại form<br/>4. Quay lại bước 5<br/><br/>**6C. Số phòng không tồn tại**<br/>1. Tại bước 6 không tìm thấy phòng<br/>2. Hiển thị: "Số phòng không tồn tại."<br/>3. Xóa ô nhập<br/>4. Quay lại bước 5<br/><br/>**7A. Phòng đầy khách (≥ 3)**<br/>1. Tại bước 7 phát hiện số khách >= 3<br/>2. Hiển thị: "Phòng đã đầy khách, vui lòng chờ."<br/>3. Từ chối nhận khách<br/>4. Làm mới form<br/>5. Kết thúc<br/><br/>**8A. Ngoài giờ thăm**<br/>1. Tại bước 8 phát hiện ngoài giờ<br/>2. Hiển thị: "Đã hết giờ thăm."<br/>3. Vô hiệu hóa nút<br/>4. Kết thúc<br/><br/>**9A. Lỗi Cơ sở dữ liệu khi lưu**<br/>1. Tại bước 9 xảy ra lỗi<br/>2. Hoàn tác giao dịch<br/>3. Hiển thị: "Lỗi hệ thống, vui lòng thử lại."<br/>4. Ghi nhật ký ngoại lệ<br/>5. Quay lại bước 5<br/><br/>**14A. Không tìm thấy bản ghi khi Khách ra**<br/>1. Tại bước 14 không tìm thấy bản ghi Đã vào<br/>2. Hiển thị: "Không tìm thấy phiếu thăm đang hoạt động."<br/>3. Kết thúc<br/><br/>**14B. Lỗi Cơ sở dữ liệu khi cập nhật Khách ra**<br/>1. Tại bước 14 mất kết nối Cơ sở dữ liệu<br/>2. Hoàn tác giao dịch<br/>3. Hiển thị: "Lỗi hệ thống, vui lòng thử lại."<br/>4. Ghi nhật ký ngoại lệ<br/>5. Quay lại bước 13 |
| Quy tắc nghiệp vụ | QL01: Mỗi phòng tối đa 3 khách cùng lúc<br/>QL02: Khung giờ thăm: 07:00 - 22:30<br/>QL03: Tự động cho khách ra lúc 23:00<br/>QL04: Một khách chỉ thăm 1 phòng tại một thời điểm |
## 3.3. Sơ đồ hoạt động - AD15

![AD15 - Quản lý khách thăm](../images/AD15_QuanLyKhachTham.jpg)

## 3.4. Sơ đồ tuần tự - SD15

![SD15 - Quản lý khách thăm (Nhận khách)](../images/SD15_QuanLyKhachTham_CheckIn.jpg)

