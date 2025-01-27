using System.Collections.Generic;

namespace Data.Entities
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        // Could have a Role, or just store a role as string

        public ICollection<Project> Projects { get; set; }
    }
}
