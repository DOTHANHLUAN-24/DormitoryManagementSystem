using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Data.Enums;

namespace DormitoryManagement.Data.Entities
{
    // Tài sản
    [Table("Assets")]
    public class Asset
    {
        [Key]
        public int Id { get; set; }
        
        [Required, StringLength(100)]
        public string AssetName { get; set; } =  string.Empty;

        public string AssetCode { get; set; } = string.Empty;

        public AssetStatus Status { get; set; } = AssetStatus.Good;

        public int RoomId { get; set; }
        
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;
    }
}
