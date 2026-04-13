# 📚 User Management API Documentation

## Giới thiệu
Tài liệu này mô tả các API endpoints để quản lý người dùng trong Hệ thống Quản lý Ký túc xá.

---

## 🔐 Authentication
Tất cả các endpoint (trừ check-username, check-email) yêu cầu:
- Header: `Authorization: Bearer {JWT_TOKEN}`

---

## 📋 API Endpoints

### 1. Tạo người dùng mới
**POST** `/api/user/create`

**Yêu cầu quyền**: Admin

**Request Body**:
```json
{
  "userName": "nguyenvan",
  "firstName": "Nguyễn",
  "lastName": "Văn",
  "email": "nguyen@example.com",
  "phoneNumber": "0123456789",
  "password": "Password123",
  "roles": ["Student"]
}
```

**Response** (200 OK):
```json
{
  "message": "Tạo người dùng thành công"
}
```

**Response** (400 Bad Request):
```json
{
  "message": "Tên người dùng đã tồn tại"
}
```

---

### 2. Lấy danh sách tất cả người dùng
**GET** `/api/user/all`

**Yêu cầu quyền**: Admin, Manager

**Response** (200 OK):
```json
[
  {
    "id": "user-id-123",
    "userName": "nguyenvan",
    "fullName": "Nguyễn Văn",
    "email": "nguyen@example.com",
    "phoneNumber": "0123456789",
    "isActive": true,
    "roles": ["Student"]
  }
]
```

---

### 3. Lấy danh sách người dùng hoạt động
**GET** `/api/user/active`

**Yêu cầu quyền**: Admin, Manager

**Response**: Danh sách người dùng có `isActive = true`

---

### 4. Lấy chi tiết người dùng
**GET** `/api/user/{userId}`

**Yêu cầu quyền**: Authenticated

**Response** (200 OK):
```json
{
  "id": "user-id-123",
  "userName": "nguyenvan",
  "firstName": "Nguyễn",
  "lastName": "Văn",
  "email": "nguyen@example.com",
  "phoneNumber": "0123456789",
  "isActive": true,
  "roles": ["Student"],
  "lockoutEnd": null
}
```

---

### 5. Tìm kiếm người dùng
**GET** `/api/user/search?searchTerm={term}`

**Yêu cầu quyền**: Admin, Manager

**Tìm kiếm theo**: Tên đăng nhập, tên, email

**Query Parameters**:
- `searchTerm`: Từ khóa tìm kiếm (bắt buộc)

**Response** (200 OK):
```json
[
  {
    "id": "user-id-123",
    "userName": "nguyenvan",
    "fullName": "Nguyễn Văn",
    "email": "nguyen@example.com",
    "phoneNumber": "0123456789",
    "isActive": true,
    "roles": ["Student"]
  }
]
```

---

### 6. Cập nhật thông tin người dùng
**PUT** `/api/user/update`

**Yêu cầu quyền**: Admin, Manager

**Request Body**:
```json
{
  "id": "user-id-123",
  "firstName": "Nguyễn",
  "lastName": "Văn",
  "email": "nguyen.van@example.com",
  "phoneNumber": "0987654321",
  "isActive": true,
  "roles": ["Student", "Manager"]
}
```

**Response** (200 OK):
```json
{
  "message": "Cập nhật người dùng thành công"
}
```

---

### 7. Xóa người dùng (Soft Delete)
**DELETE** `/api/user/{userId}`

**Yêu cầu quyền**: Admin

**Response** (200 OK):
```json
{
  "message": "Xóa người dùng thành công"
}
```

**Ghi chú**: Chỉ đánh dấu `IsActive = false`, dữ liệu vẫn giữ lại

---

### 8. Khôi phục người dùng
**POST** `/api/user/{userId}/restore`

**Yêu cầu quyền**: Admin

**Response** (200 OK):
```json
{
  "message": "Khôi phục người dùng thành công"
}
```

---

### 9. Đổi mật khẩu
**POST** `/api/user/change-password`

**Yêu cầu quyền**: Authenticated

**Request Body**:
```json
{
  "userId": "user-id-123",
  "currentPassword": "OldPassword123",
  "newPassword": "NewPassword456",
  "confirmPassword": "NewPassword456"
}
```

**Response** (200 OK):
```json
{
  "message": "Đổi mật khẩu thành công"
}
```

---

### 10. Đặt lại mật khẩu
**POST** `/api/user/{userId}/reset-password`

**Yêu cầu quyền**: Admin

**Response** (200 OK):
```json
{
  "message": "Đặt lại mật khẩu thành công",
  "newPassword": "TmpPwd123!"
}
```

**Ghi chú**: Hệ thống tạo mật khẩu ngẫu nhiên tạm thời

---

### 11. Khóa tài khoản người dùng
**POST** `/api/user/{userId}/lock`

**Yêu cầu quyền**: Admin

**Query Parameters**:
- `lockoutMinutes`: Thời gian khóa (phút), mặc định = 30

**Response** (200 OK):
```json
{
  "message": "Khóa tài khoản thành công"
}
```

---

### 12. Mở khóa tài khoản người dùng
**POST** `/api/user/{userId}/unlock`

**Yêu cầu quyền**: Admin

**Response** (200 OK):
```json
{
  "message": "Mở khóa tài khoản thành công"
}
```

---

### 13. Gán roles cho người dùng
**POST** `/api/user/{userId}/assign-roles`

**Yêu cầu quyền**: Admin

**Request Body**:
```json
["Admin", "Manager"]
```

**Response** (200 OK):
```json
{
  "message": "Gán roles thành công"
}
```

---

### 14. Lấy roles của người dùng
**GET** `/api/user/{userId}/roles`

**Yêu cầu quyền**: Admin, Manager

**Response** (200 OK):
```json
{
  "roles": ["Student", "Manager"]
}
```

---

### 15. Kiểm tra tên đăng nhập đã tồn tại
**GET** `/api/user/check-username/{userName}`

**Yêu cầu quyền**: None (Public)

**Response** (200 OK):
```json
{
  "exists": false
}
```

---

### 16. Kiểm tra email đã tồn tại
**GET** `/api/user/check-email/{email}`

**Yêu cầu quyền**: None (Public)

**Response** (200 OK):
```json
{
  "exists": true
}
```

---

## 🗂️ DTOs

### CreateUserDTO
```csharp
public class CreateUserDTO
{
    public string UserName { get; set; }           // Tên đăng nhập
    public string FirstName { get; set; }          // Tên
    public string LastName { get; set; }           // Họ
    public string Email { get; set; }              // Email
    public string PhoneNumber { get; set; }        // Số điện thoại
    public string Password { get; set; }           // Mật khẩu
    public List<string> Roles { get; set; }        // Danh sách roles
}
```

### UpdateUserDTO
```csharp
public class UpdateUserDTO
{
    public string Id { get; set; }                 // ID người dùng
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }             // Trạng thái hoạt động
    public List<string> Roles { get; set; }
}
```

### UserDetailsDTO
```csharp
public class UserDetailsDTO
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public List<string> Roles { get; set; }
    public DateTime? LockoutEnd { get; set; }      // Thời gian khóa kết thúc
}
```

### UserListDTO
```csharp
public class UserListDTO
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }           // Họ tên đầy đủ
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public List<string> Roles { get; set; }
}
```

### ChangePasswordDTO
```csharp
public class ChangePasswordDTO
{
    public string UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
```

---

## 🔍 Danh sách Roles hợp lệ
- `Admin` - Quản trị viên hệ thống
- `Manager` - Quản lý ký túc xá
- `Accountant` - Kế toán
- `Security` - Bảo vệ
- `Student` - Sinh viên

---

## ❌ Error Responses

**400 Bad Request**:
```json
{
  "message": "Lỗi chi tiết"
}
```

**401 Unauthorized**:
```json
{
  "message": "Không có quyền truy cập"
}
```

**404 Not Found**:
```json
{
  "message": "Không tìm thấy người dùng"
}
```

---

## 📝 Logging
Tất cả các hành động sẽ được ghi log:
- Tạo/cập nhật/xóa người dùng
- Đổi mật khẩu
- Khóa/mở khóa tài khoản
- Gán/xóa roles

---

## 🔒 Bảo mật
- Mật khẩu: Tối thiểu 6 ký tự (tuỳ chỉnh được)
- Xóa mềm: Không xóa cứng người dùng
- Audit Trail: Lưu log tất cả hành động
- Token JWT: Sử dụng cho authentication

---
