# Stage 1: Build ứng dụng bằng SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy toàn bộ code vào container trước để đảm bảo cấu trúc thư mục src/... chính xác
COPY . .

# Restore dự án dựa theo đúng đường dẫn trong source của bạn
RUN dotnet restore "src/DormitoryManagement/DormitoryManagement.csproj"

# Publish ứng dụng sang thư mục /app/publish
RUN dotnet publish "src/DormitoryManagement/DormitoryManagement.csproj" \
    -c Release \
    -o /app/publish

# Stage 2: Runtime (Chạy ứng dụng)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy sản phẩm đã build từ stage trước qua
COPY --from=build /app/publish .

# Render mặc định map cổng qua biến môi trường PORT. 
# Việc gán cổng cố định 10000 đôi khi bị xung đột nếu Render cấp port khác.
# Ta sẽ cấu hình linh hoạt: Dùng PORT của Render, nếu không có thì mặc định là 10000.
CMD ["sh", "-c", "dotnet DormitoryManagement.dll --urls http://0.0.0.0:${PORT:-10000}"]