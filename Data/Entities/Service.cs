using System.Collections.Generic;

namespace Data.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public decimal HourlyPrice { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
