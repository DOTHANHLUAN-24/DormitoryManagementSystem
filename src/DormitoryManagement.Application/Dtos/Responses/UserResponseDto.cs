using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Responses
{
    public class UserResponseDto
    {
        public string Id { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public UserRole Role { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime? LastModified { get; set; }
        
        public int ContractCount { get; set; }

        public List<int> ListContractIds { get; set; } = new List<int>();
    }
}
