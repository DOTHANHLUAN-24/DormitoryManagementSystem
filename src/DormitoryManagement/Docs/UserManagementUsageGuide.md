# 📖 Hướng dẫn sử dụng Chức năng Quản lý Người dùng

## Mục lục
1. [Khái niệm chung](#khái-niệm-chung)
2. [Cấu trúc Layer](#cấu-trúc-layer)
3. [Hướng dẫn sử dụng từng chức năng](#hướng-dẫn-sử-dụng-từng-chức-năng)
4. [Xử lý lỗi](#xử-lý-lỗi)
5. [Ví dụ thực tế](#ví-dụ-thực-tế)

---

## Khái niệm chung

### Kiến trúc 3 Layer
```
┌─────────────────────────┐
│   Controller Layer      │  ← Xử lý HTTP requests/responses
├─────────────────────────┤
│   Service Layer         │  ← Business logic
├─────────────────────────┤
│   Repository Layer      │  ← Database access
└─────────────────────────┘
```

### Flow xử lý điển hình
1. **Client** gửi request HTTP
2. **UserController** nhận request, validate input
3. **UserService** xử lý business logic (validate dữ liệu, check tồn tại, v.v.)
4. **UserRepository** tương tác với database
5. **Trả về response** cho client

---

## Cấu trúc Layer

### 1. Controller (UserController.cs)
**Trách nhiệm**:
- Xử lý HTTP requests
- Validate input
- Kiểm tra quyền (Authorization)
- Gọi Service xử lý logic
- Trả về response

**Vị trí**: `Controllers/UserController.cs`

**Ví dụ**:
```csharp
[HttpPost("create")]
[Authorize(Roles = "Admin")]
public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
{
    // Validate input
    if (!ModelState.IsValid)
        return BadRequest(ModelState);
    
    // Gọi service
    var result = await _userService.CreateUserAsync(createUserDTO);
    
    // Trả về response
    if (!result.Success)
        return BadRequest(new { message = result.Message });
    
    return Ok(new { message = result.Message });
}
```

---

### 2. Service (UserService.cs)
**Trách nhiệm**:
- Xử lý business logic
- Validate dữ liệu
- Gọi Repository để truy cập database
- Xử lý lỗi
- Logging

**Vị trí**: `Services/Implementations/UserService.cs`

**Ví dụ - Tạo người dùng**:
```csharp
public async Task<(bool Success, string Message)> CreateUserAsync(CreateUserDTO userDTO)
{
    try
    {
        // Validate: tên đăng nhập đã tồn tại?
        if (await _userRepository.UserExistsAsync(userDTO.UserName))
            return (false, "Tên người dùng đã tồn tại");
        
        // Validate: email đã tồn tại?
        if (await _userRepository.EmailExistsAsync(userDTO.Email))
            return (false, "Email đã tồn tại");
        
        // Tạo object User mới
        var user = new User(
            Guid.NewGuid().ToString(),
            userDTO.UserName,
            userDTO.FirstName,
            userDTO.LastName,
            userDTO.Email,
            userDTO.PhoneNumber
        );
        
        // Tạo người dùng qua UserManager (ASP.NET Identity)
        var result = await _userManager.CreateAsync(user, userDTO.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError($"Tạo người dùng {userDTO.UserName} thất bại: {errors}");
            return (false, $"Tạo người dùng thất bại: {errors}");
        }
        
        // Gán roles
        if (userDTO.Roles.Any())
        {
            var validRoles = new List<string>();
            foreach (var role in userDTO.Roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                    validRoles.Add(role);
            }
            
            if (validRoles.Any())
            {
                await _userManager.AddToRolesAsync(user, validRoles);
            }
        }
        
        // Lưu vào Repository
        await _userRepository.AddAsync(user);
        _logger.LogInformation($"Tạo người dùng {userDTO.UserName} thành công");
        
        return (true, "Tạo người dùng thành công");
    }
    catch (Exception ex)
    {
        _logger.LogError($"Lỗi khi tạo người dùng: {ex.Message}");
        return (false, "Có lỗi xảy ra khi tạo người dùng");
    }
}
```

---

### 3. Repository (UserRepository.cs)
**Trách nhiệm**:
- Truy vấn database
- Thực hiện CRUD operations
- Không chứa business logic

**Vị trí**: `Repositories/Implementations/UserRepository.cs`

**Ví dụ - Tìm kiếm người dùng**:
```csharp
public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
{
    return await _dbSet
        .AsNoTracking()
        .Where(u =>
            u.UserName.Contains(searchTerm) ||
            u.FirstName.Contains(searchTerm) ||
            u.LastName.Contains(searchTerm) ||
            u.Email.Contains(searchTerm)
        )
        .ToListAsync();
}
```

---

## Hướng dẫn sử dụng từng chức năng

### 1. Tạo người dùng mới

**Yêu cầu quyền**: Admin

**Bước**:
1. Gọi endpoint: `POST /api/user/create`
2. Gửi thông tin người dùng (userName, email, firstName, v.v.)
3. Service kiểm tra tên/email đã tồn tại?
4. Tạo tài khoản qua ASP.NET Identity
5. Gán roles nếu có
6. Lưu vào database

**Ví dụ Request**:
```bash
curl -X POST https://localhost:5001/api/user/create \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "nguyenvan",
    "firstName": "Nguyễn",
    "lastName": "Văn",
    "email": "nguyen@example.com",
    "phoneNumber": "0123456789",
    "password": "Password123",
    "roles": ["Student"]
  }'
```

---

### 2. Xem danh sách người dùng

**Yêu cầu quyền**: Admin, Manager

**API**: `GET /api/user/all` hoặc `GET /api/user/active`

**Ví dụ Response**:
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
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

### 3. Tìm kiếm người dùng

**Yêu cầu quyền**: Admin, Manager

**API**: `GET /api/user/search?searchTerm=van`

**Tìm kiếm theo**: Tên đăng nhập, tên, email

---

### 4. Cập nhật thông tin người dùng

**Yêu cầu quyền**: Admin, Manager

**Bước**:
1. Lấy ID người dùng cần update
2. Gửi dữ liệu cần cập nhật
3. Service cập nhật thông tin
4. Cập nhật roles nếu có

**Ví dụ Request**:
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "firstName": "Nguyễn",
  "lastName": "Văn",
  "email": "nguyen.new@example.com",
  "phoneNumber": "0987654321",
  "isActive": true,
  "roles": ["Student", "Manager"]
}
```

---

### 5. Xóa người dùng (Soft Delete)

**Yêu cầu quyền**: Admin

**API**: `DELETE /api/user/{userId}`

**Điểm quan trọng**:
- Xóa mềm (soft delete): Chỉ đánh dấu `IsActive = false`
- Dữ liệu vẫn giữ lại trong database
- Có thể khôi phục sau

---

### 6. Khôi phục người dùng

**Yêu cầu quyền**: Admin

**API**: `POST /api/user/{userId}/restore`

**Kết quả**: Đặt `IsActive = true`

---

### 7. Quản lý mật khẩu

#### a. Đổi mật khẩu (người dùng tự đổi)
**API**: `POST /api/user/change-password`

**Request**:
```json
{
  "userId": "550e8400-e29b-41d4-a716-446655440000",
  "currentPassword": "OldPassword123",
  "newPassword": "NewPassword456",
  "confirmPassword": "NewPassword456"
}
```

#### b. Đặt lại mật khẩu (Admin đặt lại)
**API**: `POST /api/user/{userId}/reset-password`

**Kết quả**: Hệ thống tạo mật khẩu ngẫu nhiên tạm thời

---

### 8. Khóa/Mở khóa tài khoản

**Yêu cầu quyền**: Admin

#### a. Khóa tài khoản
**API**: `POST /api/user/{userId}/lock?lockoutMinutes=30`

**Kết quả**: Người dùng không thể đăng nhập trong 30 phút

#### b. Mở khóa tài khoản
**API**: `POST /api/user/{userId}/unlock`

---

### 9. Quản lý Roles

**Yêu cầu quyền**: Admin

#### a. Gán roles cho người dùng
**API**: `POST /api/user/{userId}/assign-roles`

**Request**:
```json
["Admin", "Manager"]
```

#### b. Lấy roles của người dùng
**API**: `GET /api/user/{userId}/roles`

**Response**:
```json
{
  "roles": ["Admin", "Manager"]
}
```

---

### 10. Kiểm tra tính khả dụng

**Public (Không cần token)**

#### a. Kiểm tra tên đăng nhập
**API**: `GET /api/user/check-username/nguyenvan`

**Response**:
```json
{
  "exists": true
}
```

#### b. Kiểm tra email
**API**: `GET /api/user/check-email/nguyen@example.com`

---

## Xử lý lỗi

### Kiến trúc xử lý lỗi

```
try
{
    // Thực hiện logic
}
catch (Exception ex)
{
    // Log lỗi
    _logger.LogError($"Lỗi: {ex.Message}");
    
    // Trả về response
    return (false, "Có lỗi xảy ra");
}
```

### Các loại lỗi thường gặp

| Lỗi | Nguyên nhân | Cách xử lý |
|-----|-----------|-----------|
| Tên đăng nhập trùng | Dữ liệu không hợp lệ | Check trước khi tạo |
| Email trùng | Dữ liệu không hợp lệ | Check trước khi tạo |
| Mật khẩu yếu | Mật khẩu không đủ mạnh | Kiểm tra độ mạnh mật khẩu |
| Người dùng không tồn tại | ID không đúng | Kiểm tra ID trước |
| Không có quyền | Người dùng không phải Admin | Kiểm tra role trước |

---

## Ví dụ thực tế

### Ví dụ 1: Tạo người dùng mới từ Frontend

```javascript
// JavaScript/React
const createUser = async (userData) => {
  try {
    const response = await fetch('/api/user/create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${getToken()}`
      },
      body: JSON.stringify({
        userName: userData.username,
        firstName: userData.firstName,
        lastName: userData.lastName,
        email: userData.email,
        phoneNumber: userData.phone,
        password: userData.password,
        roles: ['Student']
      })
    });
    
    const result = await response.json();
    if (response.ok) {
      alert('Tạo người dùng thành công');
      // Refresh danh sách
      loadUsers();
    } else {
      alert(`Lỗi: ${result.message}`);
    }
  } catch (error) {
    console.error('Lỗi:', error);
  }
};
```

---

### Ví dụ 2: Tìm kiếm và cập nhật người dùng

```javascript
const searchAndUpdate = async () => {
  try {
    // 1. Tìm kiếm
    const searchResponse = await fetch(
      `/api/user/search?searchTerm=nguyen`,
      {
        headers: { 'Authorization': `Bearer ${getToken()}` }
      }
    );
    
    const users = await searchResponse.json();
    if (users.length === 0) {
      alert('Không tìm thấy người dùng');
      return;
    }
    
    const user = users[0];
    
    // 2. Cập nhật
    const updateResponse = await fetch('/api/user/update', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${getToken()}`
      },
      body: JSON.stringify({
        id: user.id,
        firstName: user.firstName,
        lastName: user.lastName,
        email: user.email + '.new',
        phoneNumber: '0987654321',
        isActive: true,
        roles: ['Student', 'Manager']
      })
    });
    
    const result = await updateResponse.json();
    if (updateResponse.ok) {
      alert('Cập nhật thành công');
    }
  } catch (error) {
    console.error('Lỗi:', error);
  }
};
```

---

### Ví dụ 3: Quản lý mật khẩu

```javascript
// Đổi mật khẩu
const changePassword = async (userId, oldPwd, newPwd) => {
  const response = await fetch('/api/user/change-password', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${getToken()}`
    },
    body: JSON.stringify({
      userId: userId,
      currentPassword: oldPwd,
      newPassword: newPwd,
      confirmPassword: newPwd
    })
  });
  
  return response.ok;
};

// Đặt lại mật khẩu (Admin)
const resetPassword = async (userId) => {
  const response = await fetch(
    `/api/user/${userId}/reset-password`,
    {
      method: 'POST',
      headers: { 'Authorization': `Bearer ${getToken()}` }
    }
  );
  
  const result = await response.json();
  return result.newPassword; // Mật khẩu tạm thời mới
};
```

---

## 📝 Checklist triển khai

- [ ] Kiểm tra database có cột IsActive trong User
- [ ] Chạy migration để cập nhật schema
- [ ] Kiểm tra ASP.NET Identity đã được cấu hình
- [ ] Tạo roles (Admin, Manager, Accountant, Security, Student)
- [ ] Kiểm tra JWT authentication hoạt động
- [ ] Test tất cả endpoints
- [ ] Kiểm tra logging hoạt động
- [ ] Cấu hình CORS nếu cần (frontend khác domain)

---
