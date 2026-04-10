# 📘 Tài liệu: Quy trình đóng góp lên dự án

## 🎯 Mục tiêu

Đảm bảo:

* Code luôn đồng bộ với team
* Hạn chế conflict
* Commit rõ ràng, dễ review

---

## Kiểm tra branch hiện tại

```bash
git branch
```

* Đảm bảo đang ở đúng branch (feature/xxx)
* Tránh commit nhầm vào main hoặc develop

---

## Cập nhật code mới nhất

```bash
git pull origin develop
```

Hoặc nếu dùng main:

```bash
git pull origin main
```

* Luôn pull trước khi code/push
* Giảm nguy cơ conflict

---

## Kiểm tra thay đổi

```bash
git status
```

* Xem file đã thay đổi
* Xác định file cần commit

---

## ➕ 4. Add file vào staging

```bash
git add .
```

Hoặc add từng file:

```bash
git add <file-name>
```

---

## 📝 5. Commit code

```bash
git commit -m "feat: add login api"
```

### 📌 Quy tắc commit message

* feat: thêm chức năng
* fix: sửa bug
* refactor: cải thiện code
* docs: tài liệu
* chore: việc nhỏ, config

---

## 🚀 6. Push lên remote

```bash
git push origin <branch-name>
```

Nếu push lần đầu:

```bash
git push -u origin <branch-name>
```
---