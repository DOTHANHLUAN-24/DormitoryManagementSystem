using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Data
{
    // Cấu hình chuẩn cho Identity dùng Guid
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Cấu hình tự động cho toàn bộ kiểu decimal trong dự án
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            // Cấu hình các bảng Identity dùng Guid (Sửa lỗi <string> thành <Guid>)
            builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserLogin<Guid>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });
            builder.Entity<IdentityUserToken<Guid>>().HasKey(p => new { p.UserId, p.LoginProvider, p.Name });

            // Cấu hình thực thể Contract (Đảm bảo khóa ngoại là Guid)
            builder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Sử dụng NEWSEQUENTIALID để tối ưu hiệu năng Index cho Guid trong SQL Server
                entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                entity.Property(e => e.ContractCode).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.ContractCode).IsUnique(); // Thêm IsUnique để quản lý mã HD tốt hơn

                entity.Property(e => e.DepositAmount)
                      .HasColumnType("decimal(18,2)")
                      .HasDefaultValue(0m);

                entity.Property(e => e.Status).HasConversion<int>();

                // Quan hệ với User (User Id lúc này đã là Guid)
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Contracts)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Bed)
                      .WithMany(b => b.Contracts)
                      .HasForeignKey(e => e.BedId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        /// <summary>
        /// Ghi đè phương thức lưu thay đổi để tự động cập nhật các trường thông tin Audit:
        /// CreatedDate, LastModified, IsActive, IsDeleted.
        /// </summary>
        /// <param name="cancellationToken">Token hủy bỏ tác vụ.</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Lấy các thực thể IAuditableEntity đang ở trạng thái Thêm hoặc Sửa
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditableEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (IAuditableEntity)entityEntry.Entity;
                var now = DateTime.Now;

                // Luôn luôn cập nhật ngày sửa đổi cuối cùng
                entity.LastModified = now;

                if (entityEntry.State == EntityState.Added)
                {
                    // Chỉ cập nhật khi thêm mới
                    if (entity.CreatedDate == default) entity.CreatedDate = now;
                    entity.IsDeleted = false;

                    // Chỉ set IsActive = true nếu nó chưa được set (mặc định)
                    if (entityEntry.Property(nameof(IAuditableEntity.IsActive)).CurrentValue == null)
                        entity.IsActive = true;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        // Entity sets
        public DbSet<Block> Blocks { get; set; } = null!;
        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<RoomType> RoomTypes { get; set; } = null!;
        public DbSet<Utility> Utilities { get; set; } = null!;
        public DbSet<UtilityUsage> UtilityUsages { get; set; } = null!;
        public DbSet<Violation> Violations { get; set; } = null!;
        public DbSet<Surcharge> Surcharges { get; set; } = null!;
        public DbSet<Bed> Beds { get; set; } = null!;
        public DbSet<Asset> Assets { get; set; } = null!;
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<VisitorLog> VisitorLogs { get; set; } = null!;
    }
}