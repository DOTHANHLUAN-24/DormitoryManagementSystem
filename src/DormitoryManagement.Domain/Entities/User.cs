using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;

namespace DormitoryManagement.Domain.Entities
{
    // Người dùng
    [Table("Users")]
    public class User : IdentityUser, IDateTimeTracking
    {
        
        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Code { get; set; } = string.Empty; // MSSV hoặc Mã nhân viên
        
        public bool IsActive { get; set; } = true;
        
        public string IdentityCardNumber { get; set; } = string.Empty; // CCCD/CMND - 13 số
        
        public UserRole Role { get; set; } // Nếu không dùng IdentityRole - để tạm

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
        
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        
    }
}
