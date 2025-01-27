using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context) { }
    }
}
