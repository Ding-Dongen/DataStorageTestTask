using System.Collections.Generic;

namespace Data.Entities
{
    public class Customer
    {
        // Identity PK
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }

        // Navigation
        public ICollection<Project> Projects { get; set; }
    }
}
