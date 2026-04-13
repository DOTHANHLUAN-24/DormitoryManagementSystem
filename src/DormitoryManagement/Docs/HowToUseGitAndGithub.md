# 📘 Tài liệu Git: Tương tác giữa các nhánh trong teamwork

## 🎯 Mục tiêu

Tài liệu này hướng dẫn cách:

* Lấy code mới nhất từ `main` về nhánh cá nhân
* Làm việc trên nhánh riêng
* Đẩy code lên remote
* Merge vào `main`
* Đồng bộ giữa các thành viên

---

## 🌿 1. Tạo nhánh mới từ main

```bash
git checkout main
git pull origin main
git checkout -b feature/ten-chuc-nang
```

---

## 🔄 2. Lấy code mới nhất từ main về nhánh của bạn

### Cách 1: merge (an toàn, dễ dùng)

```bash
git checkout feature/ten-chuc-nang
git pull origin main
```

### Cách 2: rebase (lịch sử đẹp hơn)

```bash
git checkout feature/ten-chuc-nang
git fetch origin
git rebase origin/main
```

---

## 💻 3. Làm việc trên nhánh cá nhân

```bash
git add .
git commit -m "feat: mô tả chức năng"
```

---

## 🚀 4. Đẩy code lên remote

```bash
git push origin feature/ten-chuc-nang
```

---

## 🔀 5. Merge vào main (cách phổ biến)

### Bước 1: Tạo Pull Request trên GitHub

* Vào repo
* Chọn nhánh feature
* Tạo Pull Request vào main

### Bước 2: Review & Merge

---

## 🔁 6. Sau khi merge, cập nhật lại local

```bash
git checkout main
git pull origin main
```

---

## 🔄 7. Đồng bộ lại nhánh cá nhân sau khi main thay đổi

```bash
git checkout feature/ten-chuc-nang
git merge main
```

hoặc

```bash
git rebase main
```

---

## ⚠️ 8. Xử lý conflict

Khi có conflict:

```bash
# sửa file bị conflict
git add .
git commit
```

Nếu rebase:

```bash
git rebase --continue
```

---

## 🔥 9. Một số lệnh hữu ích

```bash
git branch            # xem danh sách nhánh
git branch -r         # xem nhánh remote
git checkout <branch> # chuyển nhánh
git log --oneline     # xem lịch sử commit
git stash             # lưu tạm thay đổi
git stash pop         # lấy lại thay đổi
```

---

## 🤝 Quy tắc teamwork đề xuất

* Không commit trực tiếp vào main
* Luôn pull main trước khi code
* Dùng Pull Request để merge
* Viết commit message rõ ràng
* Resolve conflict trước khi push

---

## 📌 Workflow đề xuất (ngắn gọn)

```bash
# 1. Cập nhật main
git checkout main
git pull

# 2. Tạo nhánh
git checkout -b feature/abc

# 3. Code + commit
git add .
git commit -m "feat: abc"

# 4. Push
git push origin feature/abc

# 5. Tạo PR -> merge
```

---

## 🧠 Ghi nhớ nhanh

* Luôn làm việc trên nhánh riêng
* main = code ổn định
* feature = code đang phát triển
* merge/rebase = đồng bộ code
