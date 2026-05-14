# 📋 Báo cáo Khảo sát Hệ thống Quản lý Ký túc xá

## 📊 I. Hiện trạng của quản lý ký túc xá

### 👨‍🎓 1. Sinh viên
- 📁 **Lưu trữ:** Quản lý sinh viên hiện nay sử dụng các sổ sách và file excel để lưu trữ dữ liệu.
- ⚠️ **Hạn chế:** Khó tra cứu khi hỏi phòng trống, dễ mất mát, tốn thời gian và có khi thông tin không kịp cập nhật.

### 🛏️ 2. Phòng
- 📝 **Quy trình:** Quy trình đăng ký và trả phòng hiện tại được thực hiện hoàn toàn qua giấy tờ.
- 🏢 **Thủ tục:** Sinh viên phải đến văn phòng quản lý để nộp đơn và nhận quyết định bằng văn bản.
- ❌ **Vấn đề:** Xảy ra tình trạng "1 giường 2 chủ" do chậm cập nhật; SV ở tỉnh xa không biết tình trạng phòng trước khi nhập học.

### 🔧 3. Dịch vụ
- ⚡ **Điện - Nước:** Đối với công tác quản lý điện nước, hiện nay nhân viên kỹ thuật hoặc bảo vệ sẽ đi ghi chỉ số đồng hồ tại từng phòng vào cuối tháng bằng sổ tay.
- 💾 **Xử lý dữ liệu:** Dữ liệu này sẽ nhập bằng tay để lưu trữ, tốn nhiều nhân lực và mất thời gian, đồng thời tiềm ẩn nguy cơ sai lệch số liệu cao do lỗi đánh máy hoặc đọc nhầm chỉ số đồng hồ.

### 🚨 4. Báo cáo
- 📢 **Kênh báo cáo:** Kênh báo cáo sự cố và yêu cầu sửa chữa cơ sở vật chất còn mang tính tự phát.
- 📞 **Cách thức:** Sinh viên thường phải gọi điện trực tiếp cho quản lý tầng hoặc đến phòng bảo vệ để trình bày vấn đề.
- ⏳ **Hậu quả:** Thường bị lãng quên hoặc mất nhiều ngày mới được xử lý.

### 📢 5. Thông báo
- 💬 **Phương tiện:** Việc trao đổi thông tin giữa ban quản lý và sinh viên chủ yếu diễn ra qua bảng thông báo giấy đặt ở sảnh hoặc qua các nhóm chat Zalo, Facebook.
- 📵 **Tác động:** Thường xuyên bỏ lỡ các thông báo quan trọng như lịch cắt điện, lịch kiểm tra phòng hay hạn cuối nộp tiền phòng.

---

## ✨ II. Nhu cầu đối với hệ thống mới

### 🎯 1. Nhu cầu từ phía Sinh viên

- **🌐 Kênh đăng ký và quản lý phòng trực tuyến**
    - 🗺️ Mong muốn có thể xem được sơ đồ phòng, bao gồm thông tin chi tiết về số giường còn trống, có máy lạnh hay không và giá thuê cụ thể.
    - 📲 Việc đăng ký hoặc yêu cầu chuyển phòng cũng cần được số hóa để sinh viên không phải đến văn phòng xếp hàng chờ đợi.

- **💰 Theo dõi tài chính cá nhân**
    - 📊 Muốn biết số điện, số nước tiêu thụ hàng ngày hoặc ít nhất là hàng tuần, thay vì phải đợi đến cuối tháng mới biết hóa đơn.
    - 💳 Thanh toán trực tuyến qua ví điện tử hoặc chuyển khoản ngân hàng.

- **🛠️ Quy trình báo sự cố và sửa chữa minh bạch**
    - 📸 Sinh viên mong muốn chỉ cần chụp ảnh hiện trạng hư hỏng và gửi lên ứng dụng.
    - 🔄 Hệ thống cần có khả năng cập nhật trạng thái xử lý (`Đã tiếp nhận` -> `Đang sửa` -> `Đã hoàn thành`) để sinh viên chủ động theo dõi.
    - 🔔 Không cần phải gọi điện hỏi lại nhiều lần, nếu như lâu không xử lý cần thông báo tới người quản lý.

### 🏢 2. Nhu cầu từ Ban quản lý và Nhân viên văn phòng

- **📈 Báo cáo tổng thể trực quan**
    - Nhu cầu là có được một bức tranh tổng thể về tình trạng ký túc xá qua các báo cáo trực quan.
    - Họ cần nắm được tỷ lệ lấp đầy của từng tòa nhà, danh sách sinh viên sắp hết hạn hợp đồng, cũng như dữ liệu về doanh thu và công nợ một cách tự động mà không phải lọc thủ công trên Excel.

- **🔋 Hiện đại hóa ghi chỉ số Điện - Nước**
    - Quy trình ghi chỉ số điện nước cũng cần được hiện đại hóa.
    - 📱 Nhân viên kỹ thuật mong muốn sử dụng điện thoại thông minh để quét mã QR trên công tơ, nhập số liệu và chụp ảnh bằng chứng tại chỗ.
    - 📄 Những dữ liệu này sẽ tự động được tính toán và tạo hóa đơn gửi đến cho sinh viên.

### 📣 3. Nhu cầu về Truyền thông
- **🔔 Thông báo đẩy (Push Notification)**
    - Để thay thế cho bảng tin giấy và các nhóm chat lộn xộn.
    - Đảm bảo mọi thông báo quan trọng đều đến được đúng sinh viên.

---

## 🔬 III. Phương pháp khảo sát đã được sử dụng

### 🎤 1. Phỏng vấn trực tiếp
- **📝 Mô tả:** Phỏng vấn trực tiếp theo kịch bản câu hỏi mở.
- **👥 Đối tượng:** Trưởng phòng CTSV, Bảo vệ trực ca đêm, Kế toán.
- **🎯 Kết quả:** Nắm được quy trình nghiệp vụ ngầm.

### 📋 2. Bảng hỏi khảo sát
- **📝 Mô tả:** Gửi Google Forms qua email sinh viên và các nhóm Zalo lớp.
- **👥 Đối tượng:** Sinh viên nội trú.
- **🎯 Kết quả:** Đánh giá mức độ hài lòng với dịch vụ hiện tại và thăm dò mức độ sẵn sàng sử dụng App di động.

### 👀 3. Quan sát thực địa
- **📝 Mô tả:** Đến KTX vào các khung giờ cao điểm và đầu tháng.
- **👥 Đối tượng:** Tại sảnh KTX và khu vực gửi xe.
- **🎯 Kết quả:** Phát hiện điểm nghẽn giao thông dữ liệu.

### 📂 4. Phân tích Tài liệu
- **📝 Mô tả:** Xin các mẫu biểu hiện tại đang dùng.
- **👥 Đối tượng:** File Excel quản lý phòng, Sổ trực ca, Hóa đơn giấy.
- **🎯 Kết quả:** Xác định cấu trúc dữ liệu cần lưu trữ và luồng luân chuyển chứng từ.