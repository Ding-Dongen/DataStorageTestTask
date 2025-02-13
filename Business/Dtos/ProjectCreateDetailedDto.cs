namespace Business.Dtos;

public class ProjectCreateDetailedDto
{
    public string ProjectNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string Description { get; set; } = string.Empty;

  
     
    public int ServiceId { get; set; }  
    public int StaffId { get; set; }

     
    public ServiceDto? Service { get; set; }
    public StaffDto? Staff { get; set; }
}
