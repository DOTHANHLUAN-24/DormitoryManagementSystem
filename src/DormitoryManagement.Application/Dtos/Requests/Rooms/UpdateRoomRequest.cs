using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class UpdateRoomRequest : CreateRoomRequest
    {
        public Guid Id { get; set; }

        [Display(Name = "Trạng thái hoạt động")]
        public bool IsActive { get; set; }
    }
}