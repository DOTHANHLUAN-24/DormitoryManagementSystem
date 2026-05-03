# 🤝 Hướng Dẫn Đóng Góp Mã Nguồn (Contributing Guide)

Chào mừng các thành viên của **Nhóm 02** tham gia đóng góp mã nguồn cho dự án **Hệ Thống Quản Lý Ký Túc Xá**. Để đảm bảo source code luôn sạch sẽ, dễ bảo trì và hạn chế xung đột (conflict) khi làm việc nhóm, toàn đội cần thống nhất tuân thủ các quy tắc dưới đây.

---

## 1. 🔄 Quy Trình Làm Việc (Git Workflow)

Dự án áp dụng mô hình nhánh theo **Feature Branch Workflow**:

1. **Cập nhật code mới nhất** (Luôn bắt đầu từ nhánh `main`):
   ```bash
   git checkout main
   git pull origin main
   ```
2. **Tạo nhánh mới** cho tính năng hoặc lỗi đang xử lý:
   ```bash
   git checkout -b <loai-nhanh>/<ten-tinh-nang>
   ```
3. Thực hiện code, kiểm tra cẩn thận và commit.
4. **Push nhánh** của bạn lên remote repository:
   ```bash
   git push origin <loai-nhanh>/<ten-tinh-nang>
   ```
5. Tạo **Pull Request (PR)** trên GitHub để merge vào nhánh `main`. Yêu cầu ít nhất 1 thành viên khác review code trước khi merge.

---

## 2. 🏷️ Quy Tắc Đặt Tên Nhánh (Branch Naming)

Tên nhánh cần viết chữ thường, không dấu và sử dụng gạch nối (`-`) thay cho khoảng trắng.

- `feature/...` : Dùng khi phát triển một chức năng mới (VD: `feature/dang-ky-phong`)
- `bugfix/...`  : Dùng khi sửa lỗi (VD: `bugfix/loi-tinh-tien-dien`)
- `hotfix/...`  : Dùng khi sửa lỗi khẩn cấp trên nhánh main (VD: `hotfix/loi-dang-nhap`)
- `docs/...`    : Dùng khi cập nhật tài liệu (VD: `docs/cap-nhat-readme`)

---

## 3. 💬 Quy Tắc Viết Commit Message

Tuân thủ theo chuẩn Conventional Commits.
Cấu trúc: `<loại>(<phạm-vi-tuỳ-chọn>): <mô-tả-ngắn-gọn>`

**Các loại commit phổ biến:**
- `feat:` Thêm một tính năng mới (VD: `feat(auth): thêm chức năng đăng nhập bằng JWT`).
- `fix:` Sửa một lỗi bug (VD: `fix(invoice): sửa lỗi tính sai tổng tiền hóa đơn`).
- `docs:` Cập nhật tài liệu dự án, Swagger, Markdown.
- `style:` Cập nhật format code, dấu phẩy, khoảng trắng... (không ảnh hưởng logic code).
- `refactor:` Cải thiện/Viết lại code nhưng không thay đổi chức năng hay sửa lỗi.
- `test:` Thêm hoặc sửa Unit Test/Integration Test.

---

## 4. 💻 Tiêu Chuẩn Code (Coding Convention)

Dự án sử dụng **C# / ASP.NET Core 8** với **Clean Architecture**, do đó cần tuân thủ:
1. **Quy tắc Đặt tên (Naming):** `PascalCase` cho Tên Class, Method, Interface (bắt đầu bằng `I`), Public Properties. Sử dụng `camelCase` cho tham số và biến cục bộ. `_camelCase` đối với private readonly fields.
2. **Nguyên tắc SOLID & DRY:** Tránh lặp lại code (DRY). Chia nhỏ Service theo nguyên tắc Đơn trách nhiệm (Single Responsibility).
3. **Kiến trúc 3 Lớp:** Controller chỉ dùng để nhận request/response. Logic nghiệp vụ phải đặt ở Application/Service Layer. Thao tác Database thông qua Repository Pattern ở Infrastructure Layer.
4. **Format & Xử lý lỗi:** Sử dụng Global Exception Middleware để bắt lỗi tập trung. Tận dụng thư viện FluentValidation để kiểm tra dữ liệu đầu vào.

*Chúc các bạn hoàn thành dự án chuyên đề với kết quả tốt nhất!*
