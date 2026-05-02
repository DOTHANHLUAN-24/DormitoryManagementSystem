using DormitoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public ApplicationDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // Phải là dấu đóng ngoặc đơn )
                property.SetColumnType("decimal(18,2)");
            }

            builder.Entity<IdentityRole>()
                .Property(x => x.Id).HasMaxLength(50).IsUnicode(false);

            builder.Entity<User>()
                .Property(x => x.Id).HasMaxLength(50).IsUnicode(false);

            builder.Entity<IdentityUserRole<string>>()
                .Property(x => x.UserId).HasMaxLength(50).IsUnicode(false);

            builder.Entity<IdentityUserRole<string>>()
                .Property(x => x.RoleId).HasMaxLength(50).IsUnicode(false);

            builder.Entity<IdentityUserClaim<string>>()
                .Property(x => x.UserId).HasMaxLength(50).IsUnicode(false);

            builder.Entity<IdentityRoleClaim<string>>()
                .Property(x => x.RoleId).HasMaxLength(50).IsUnicode(false);

            builder.Entity<IdentityUserLogin<string>>()
                .Property(x => x.UserId).HasMaxLength(50).IsUnicode(false);

            builder.Entity<IdentityUserToken<string>>()
                .Property(x => x.UserId).HasMaxLength(50).IsUnicode(false);

            // Contract configuration (fixed decimal syntax)
            builder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ContractCode)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.HasIndex(e => e.ContractCode);
                entity.Property(e => e.StartDate)
                      .IsRequired();
                entity.Property(e => e.EndDate)
                      .IsRequired();
                entity.Property(e => e.DepositAmount)
                      .HasColumnType("decimal(18,2)")
                      .HasDefaultValue(0m);
                entity.Property(e => e.Status)
                      .HasConversion<int>();
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                      .IsRequired(false);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Contracts)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Bed)
                      .WithMany(b => b.Contracts)
                      .HasForeignKey(e => e.BedId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Invoices)
                      .WithOne(i => i.Contract)
                      .HasForeignKey(i => i.ContractId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Violations)
                      .WithOne(v => v.Contract)
                      .HasForeignKey(v => v.ContractId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
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
