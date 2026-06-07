using Bogus;
using DormitoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator
{
    public class SeedContext
    {
        public Faker Faker { get; } = new Faker("vi");

        public PasswordHasher<User> Hasher { get; } = new PasswordHasher<User>();

        public List<User> Users { get; set; } = new List<User>();

        public List<Block> Blocks { get; set; } = new List<Block>();

        public List<RoomType> RoomTypes { get; set; } = new List<RoomType>();

        public List<Room> Rooms { get; set; } = new List<Room>();

        public List<Bed> Beds { get; set; } = new List<Bed>();

        public List<Utility> Utilities { get; set; } = new List<Utility>();

        public List<UtilityUsage> UtilityUsages { get; set; } = new List<UtilityUsage>();

        public List<Contract> Contracts { get; set; } = new List<Contract>();

        public List<Invoice> Invoices { get; set; } = new List<Invoice>();

        public List<Payment> Payments { get; set; } = new List<Payment>();

        public List<Surcharge> Surcharges { get; set; } = new List<Surcharge>();

        public List<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

        public List<Asset> Assets { get; set; } = new List<Asset>();

        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public List<Violation> Violations { get; set; } = new List<Violation>();

        public List<UtilityServiceRequest> UtilityServiceRequests { get; set; } = new List<UtilityServiceRequest>();
    }
}
