using DormitoryManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public ApplicationDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
        }

        public DbSet<Block> Blocks { get; set; } = null!;
        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<RoomType> RoomTypes { get; set; } = null!;
        public DbSet<Utility> utilities { get; set; } = null!;
        public DbSet<UtilityUsage> UtilityUsages { get; set; } = null!;
        public DbSet<Violation> Violations { get; set; } = null!;
    }
}
