using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }    
        public string RoomType { get; set; }      
        public int Floor { get; set; }           
        public string Building { get; set; }      
        public int Capacity { get; set; }         
        public int CurrentOccupancy { get; set; } 
        public decimal Price { get; set; }        
        public string Status { get; set; }       
    }
}