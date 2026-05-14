# 🧠 Về Lý Thuyết Nền Tảng

- 📌 Để xây dựng một hệ thống phần mềm quản lý ký túc xá có khả năng vận hành ổn định, phục vụ đồng thời nhiều đối tượng người dùng khác nhau từ sinh viên, cán bộ quản lý đến nhân viên bảo vệ, chúng ta không thể chỉ tập trung vào việc viết mã lệnh đơn thuần.
- 📌 Cần có một sự am hiểu sâu sắc về các nguyên lý kiến trúc phần mềm nền tảng.
- 📌 Ba trụ cột lý thuyết chính tạo nên bộ khung vững chắc cho hệ thống này bao gồm: **Kiến trúc phân tán Client-Server**, **Mẫu kiến trúc phần mềm MVC**, và **Mô hình dữ liệu quan hệ**.
- 📌 Sự kết hợp hài hòa giữa ba yếu tố này quyết định:
    - ⚡ Tính hiệu quả
    - 🔧 Khả năng bảo trì
    - ⏳ Tuổi thọ của sản phẩm phần mềm

---

# 🌐 Về Client - Server

### 💻 Về Client
- Trong mô hình này, phía máy khách hay còn gọi là Client, thường là trình duyệt web trên máy tính của phòng quản lý đào tạo, điện thoại thông minh của sinh viên, hoặc máy tính bảng của nhân viên bảo vệ tại cổng.
- 🎯 Nhiệm vụ duy nhất của Client là **gửi yêu cầu** và **hiển thị kết quả trả về**.

### 🖥️ Về Server
- Phía máy chủ hay còn gọi là Server, là một máy tính mạnh mẽ được đặt tại trung tâm dữ liệu của trường hoặc trên nền tảng đám mây.
- 🎯 Có nhiệm vụ **lắng nghe** các yêu cầu từ mọi Client, **xử lý logic nghiệp vụ** phức tạp và **truy xuất dữ liệu**.

### 🔄 Cơ Chế Hoạt Động
- Diễn ra theo vòng lặp **yêu cầu** và **phản hồi**.
- 📋 Giả sử một sinh viên muốn đăng ký ở ký túc xá từ xa:
    - 📱 Sinh viên đó sẽ mở trình duyệt trên điện thoại và truy cập vào địa chỉ website của nhà trường.
    - 📤 Trình duyệt đóng vai trò Client sẽ gửi một gói tin yêu cầu HTTP đến Server.
    - 🔐 Server sau khi nhận được yêu cầu sẽ kiểm tra xem sinh viên đã đăng nhập hay chưa.
    - 🗄️ Sau đó thực hiện truy vấn xuống cơ sở dữ liệu để kiểm tra xem còn phòng trống hay không.
    - 📄 Sau khi có kết quả, Server tạo ra một trang web hiển thị danh sách các phòng còn chỗ.
    - 📨 Gửi toàn bộ mã nguồn giao diện đó về cho Client.
    - 🖼️ Trình duyệt của sinh viên hiển thị giao diện đẹp mắt đó lên màn hình.

---

# 🏛️ Về Mô Hình MVC (Model, View, Controller)

### 🗃️ Về Model
- Nó đại diện cho **dữ liệu và logic nghiệp vụ cốt lõi** của bài toán ký túc xá.
- 🚫 Nó không hề biết gì về màu sắc hay bố cục của trang web, nó chỉ quan tâm đến việc định nghĩa một sinh viên bao gồm những thuộc tính gì.
- 🔗 Model là tầng **duy nhất** trong hệ thống được phép tương tác trực tiếp với cơ sở dữ liệu quan hệ bên dưới.

### 🎨 Về View
- Nó chịu trách nhiệm về **mọi thứ mà người dùng nhìn thấy** trên màn hình.
- 📁 Là tầng chứa các tệp tin mẫu giao diện HTML kết hợp với CSS để tạo ra các bảng biểu, nút bấm và biểu mẫu nhập liệu.
- 🚫 View tuyệt đối **không được chứa các phép tính toán logic phức tạp**.
- 📥 Nhiệm vụ là nhận dữ liệu đã được chuẩn bị sẵn từ Controller và Model gửi sang, rồi hiển thị chúng lên đúng vị trí đã định sẵn trên giao diện.

### 🎛️ Về Controller
- Controller là **trung tâm điều phối chính**.
- 🚦 Nó hoạt động như một người quản lý giao thông thông minh.
- 📞 Khi nhận được một yêu cầu từ Client, chẳng hạn như đường dẫn yêu cầu xem danh sách phòng trống -> Controller sẽ đứng ra tiếp nhận.
- 🔑 Nó sẽ kiểm tra quyền hạn của người dùng -> gọi đến các hàm tương ứng trong tầng Model để lấy dữ liệu từ cơ sở dữ liệu.
- 📦 Khi đã có dữ liệu thô trong tay, Controller sẽ lựa chọn View phù hợp nhất -> đóng gói dữ liệu vào đó và gửi trả toàn bộ về cho Client.

### ✅ Kết Luận về MVC
- Điều này có nghĩa là chúng ta sẽ tách biệt rạch ròi phần xử lý dữ liệu, phần giao diện người dùng và phần điều khiển luồng hoạt động.
- 🛠️ Nhờ sự phân tách này, khi hệ thống ký túc xá cần thay đổi hay nâng cấp, lập trình viên sẽ không phải đau đầu sửa chữa toàn bộ hệ thống.

---

# 🗄️ Về Cơ Sở Dữ Liệu

- 💾 Nó là nơi lưu trữ thông tin, dữ liệu của tất cả các tác nhân một cách logic và chặt chẽ, sử dụng hệ quản trị cơ sở dữ liệu quan hệ.
- 🔗 Nguyên lý hoạt động của mô hình dựa trên tổ chức thông tin thành các bảng dữ liệu hai chiều liên kết với nhau qua các khóa.
- 🛡️ Nó đảm bảo không có sự trùng lặp dữ liệu vô ích và duy trì tính toàn vẹn.

---

# 🚀 Về Sản Phẩm (Quy Trình Liên Kết)

### 1️⃣ Client Khởi Tạo Yêu Cầu
- 🧑‍🎓 Sinh viên A ngồi ở nhà mở điện thoại hoặc laptop.
- 🌍 Truy cập vào website.
- 🔓 Sau khi đăng nhập thành công, sinh viên A nhìn thấy danh sách các phòng còn trống hiển thị trên màn hình.
- 🖱️ Sinh viên A nhấn nút **"Đăng ký Phòng ABC"**.
- 📤 Hành động này tạo ra một yêu cầu HTTP Request.
- 🏷️ Yêu cầu này mang theo thông tin: Mã số sinh viên của A đã được xác thực, và Mã số Phòng ABC mà A mong muốn.

### 2️⃣ Controller Tiếp Nhận và Phân Luồng
- 🔎 Nhiệm vụ đầu tiên của Controller trong quy trình quản lý ký túc xá là **kiểm tra tính hợp lệ cơ bản**.
- ❓ Nó tự hỏi: *Sinh viên này đã đăng nhập chưa? Sinh viên này có đang bị kỷ luật cấm ở ký túc xá không?*
- 👨‍💼 Nếu mọi thứ hợp lệ, Controller không tự ý xử lý tiếp mà nó đóng vai trò như một người quản đốc, nó gọi đến bộ phận chuyên môn phù hợp, đó chính là **Model**.

### 3️⃣ Model Thực Thi Logic Nghiệp Vụ và Tương Tác với Cơ Sở Dữ Liệu
Quy trình xử lý bên trong Model diễn ra như sau:

- 📋 **Kiểm tra ràng buộc khóa ngoại và tính tồn tại.**
- 🧮 **Kiểm tra sức chứa:** Đây chính là lúc mối quan hệ 1-N phát huy tác dụng để ngăn chặn việc đăng ký vượt quá tải.
- ✅ **Ghi nhận giao dịch.**
- 📤 Sau khi hoàn tất, Model trả về kết quả cho Controller.

### 4️⃣ Controller Lựa Chọn View Để Phản Hồi
Controller nhận kết quả từ Model. Dựa vào kết quả đó, nó sẽ đưa ra quyết định cuối cùng về mặt hiển thị:

- **✅ Trường hợp 1: Đăng ký thành công**
    - Controller sẽ tìm đến một tệp tin View có tên là `DangKyThanhCong.html`.
    - Nó đổ dữ liệu hợp đồng vừa tạo (số phòng, ngày bắt đầu) vào View này.
    - View sẽ định dạng thông tin đó thành một trang web đẹp mắt.

- **❌ Trường hợp 2: Đăng ký thất bại do phòng đã đầy**
    - Controller sẽ chọn một tệp View khác, ví dụ `LoiDangKy.html`.
    - Truyền vào đó thông điệp lỗi mà Model đã cung cấp. View sẽ hiển thị một cảnh báo.

### 5️⃣ Server Gửi Trả Kết Quả Về Client
- 📦 Toàn bộ mã HTML mà View tạo ra được đóng gói thành một gói tin HTTP Response.
- 🌐 Máy chủ gửi gói tin này ngược trở lại mạng Internet để đến đúng địa chỉ IP của điện thoại sinh viên A.
- 📲 Trình duyệt trên điện thoại của sinh viên A nhận được phản hồi và hiển thị trang web thông báo thành công hoặc thất bại.

---