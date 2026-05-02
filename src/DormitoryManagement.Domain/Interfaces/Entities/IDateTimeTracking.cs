namespace DormitoryManagement.Domain.Interfaces.Entities
{
    public interface IDateTimeTracking
    {
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
