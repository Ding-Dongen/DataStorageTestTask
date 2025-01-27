using System.Collections.Generic;

namespace Data.Entities
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Name { get; set; } // e.g. "Not Started", "Ongoing", "Finished"

        public ICollection<Project> Projects { get; set; }
    }
}
