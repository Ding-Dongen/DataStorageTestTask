

namespace Business.Dtos
{
    public class ProjectDto
    {
        public string ProjectNumber { get; set; } // We'll treat this as read-only from the user
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int StaffId { get; set; }
        public int StatusId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
    }
}
