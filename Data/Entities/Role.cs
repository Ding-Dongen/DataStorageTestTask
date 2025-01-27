using System.Collections.Generic;

namespace Data.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<Staff> StaffMembers { get; set; }
    }
}
