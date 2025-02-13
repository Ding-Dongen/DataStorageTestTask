

namespace Data.Entities;

public class Project
{
    public string ProjectNumber { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    public int StaffId { get; set; }
    public Staff Staff { get; set; }
    public int StatusId { get; set; }
    public Status Status { get; set; }
    public decimal TotalPrice { get; set; }
    public string Description { get; set; }
}
