namespace DormitoryManagement.Data.Interfaces
{
    public interface IDateTimeTracking
    {
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
