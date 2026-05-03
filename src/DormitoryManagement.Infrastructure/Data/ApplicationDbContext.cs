using DormitoryManagement.Domain.Entities;
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
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Bed)
                      .WithMany(b => b.Contracts)
                      .HasForeignKey(e => e.BedId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Contract>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Invoice>().HasQueryFilter(i => !i.IsDeleted);
            builder.Entity<Payment>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Surcharge>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<Violation>().HasQueryFilter(v => !v.IsDeleted);
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
    }
}