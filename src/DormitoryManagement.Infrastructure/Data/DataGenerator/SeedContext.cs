using Bogus;
using DormitoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Infrastructure.Data.DataGenerator
{
    public class SeedContext
    {
        public Faker Faker { get; } = new Faker("vi");
        
        public PasswordHasher<User> Hasher { get; } = new();

        public List<User> Users { get; set; } = new();

        public List<Block> Blocks { get; set; } = new();
        
        public List<RoomType> RoomTypes { get; set; } = new();
        
        public List<Room> Rooms { get; set; } = new();
        
        public List<Bed> Beds { get; set; } = new();
        
        public List<Utility> Utilities { get; set; } = new();
        
        public List<UtilityUsage> UtilityUsages { get; set; } = new();
        
        public List<Contract> Contracts { get; set; } = new();
        
        public List<Invoice> Invoices { get; set; } = new();
        
        public List<Payment> Payments { get; set; } = new();
        
        public List<Surcharge> Surcharges { get; set; } = new();
        
        public List<MaintenanceRequest> MaintenanceRequests { get; set; } = new();
        
        public List<Asset> Assets { get; set; } = new();
        
        public List<Vehicle> Vehicles { get; set; } = new();
        
        public List<Violation> Violations { get; set; } = new();

        public List<UtilityServiceRequest> UtilityServiceRequests { get; set; } = new();
    }
}
