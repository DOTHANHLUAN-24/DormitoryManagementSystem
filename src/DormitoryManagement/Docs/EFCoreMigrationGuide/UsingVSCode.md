# 📦 EF Core Migration Guide (Clean Architecture)

## 🧱 Project Structure (của bạn)

```
DormitoryManagementSystem/
│
├── src/
│   ├── DormitoryManagement/                  (API - Startup project)
│   ├── DormitoryManagement.Infrastructure/  (DbContext + Migrations)
│   ├── DormitoryManagement.Application/
│   └── DormitoryManagement.Domain/
```

---

## ⚙️ Nguyên tắc quan trọng

* **Entities** → nằm ở `Domain`
* **DbContext + Migrations** → nằm ở `Infrastructure`
* **Startup project** → là `DormitoryManagement`

---

## 🚀 Bước 1: Tạo Migration

👉 Chạy trong thư mục `src`

```
dotnet ef migrations add Init \
--project DormitoryManagement.Infrastructure \
--startup-project DormitoryManagement
```

---

## 🚀 Bước 2: Update Database

```
dotnet ef database update \
--project DormitoryManagement.Infrastructure \
--startup-project DormitoryManagement
```

---

## 🔥 Flow chuẩn mỗi lần thay đổi Entity

1. Sửa Entity (Domain)
2. Chạy:

```
dotnet ef migrations add <MigrationName> \
--project DormitoryManagement.Infrastructure \
--startup-project DormitoryManagement
```

3. Apply:

```
dotnet ef database update \
--project DormitoryManagement.Infrastructure \
--startup-project DormitoryManagement
```

---

## ⚠️ Xử lý lỗi phổ biến

### ❌ Lỗi: bảng đã tồn tại (AspNetRoles...)

👉 Cách nhanh nhất (dev):

```
DROP DATABASE YourDatabaseName
```

Sau đó chạy lại:

```
dotnet ef database update \
--project DormitoryManagement.Infrastructure \
--startup-project DormitoryManagement
```

---

### ❌ Lỗi không tìm thấy project

👉 Fix:

* Kiểm tra chính tả tên project
* Đảm bảo đang đứng trong `src`
* Hoặc dùng full path tới `.csproj`

---

### ❌ Lỗi DbContext không tạo được

👉 Tạo DesignTime Factory:

```csharp
public class DesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("your_connection_string")
            .Options;

        return new AppDbContext(options);
    }
}
```

---

## 🧠 Tip xịn

* Dùng `TAB` để auto-complete tên project
* Chạy `dotnet build` trước nếu migration lỗi
* Không xóa `Migrations` khi DB đang tồn tại

---

## 🎯 Kết luận

```
Add Migration → Update Database
```

Luôn nhớ:

* Migration nằm ở Infrastructure
* Startup là API
* Command chạy từ src
