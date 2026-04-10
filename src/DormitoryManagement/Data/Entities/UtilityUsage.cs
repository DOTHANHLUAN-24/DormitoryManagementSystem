using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    [Table("UtilityUsages")]
    public class UtilityUsage
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        
        public Room Room { get; set; } = null!;

        public int UtilityId { get; set; }
        
        public Utility Utility { get; set; } = null!;

        public double PreviousIndex { get; set; } 

        public double CurrentIndex { get; set; }  
        
        public DateTime ReadingDate { get; set; }
    }
}
