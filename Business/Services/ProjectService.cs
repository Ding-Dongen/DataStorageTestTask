
using Business.Dtos;
using Business.Factories;
using Data.Interfaces;
namespace Business.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepo.GetAllAsync();
            return projects.Select(p => new ProjectDto
            {
                ProjectNumber = p.ProjectNumber,
                Name = p.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                CustomerId = p.CustomerId,
                ServiceId = p.ServiceId,
                StaffId = p.StaffId,
                StatusId = p.StatusId,
                TotalPrice = p.TotalPrice,
                Description = p.Description
            });
        }

        public async Task<ProjectDto> GetProjectByNumberAsync(string projectNumber)
        {
            var project = await _projectRepo.GetAsync(projectNumber);
            if (project == null) return null;

            return new ProjectDto
            {
                ProjectNumber = project.ProjectNumber,
                Name = project.Name,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                CustomerId = project.CustomerId,
                ServiceId = project.ServiceId,
                StaffId = project.StaffId,
                StatusId = project.StatusId,
                TotalPrice = project.TotalPrice,
                Description = project.Description
            };
        }

        public async Task<string> CreateProjectAsync(ProjectDto dto)
        {
            // Step 1: Generate a unique project number
            var newNumber = await GenerateProjectNumberAsync();

            // Step 2: Convert DTO to Entity
            var projectEntity = EntityFactory.CreateProject(dto, newNumber);

            // Step 3: Save to DB
            await _projectRepo.AddAsync(projectEntity);

            // Return the new project number to the caller
            return newNumber;
        }

        public async Task UpdateProjectAsync(ProjectDto dto)
        {
            // We look up the existing Project by its unique ProjectNumber
            var existing = await _projectRepo.GetAsync(dto.ProjectNumber);
            if (existing == null) return; // No project found

            // Update only the fields that can change
            existing.Name = dto.Name;
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;
            existing.CustomerId = dto.CustomerId;
            existing.ServiceId = dto.ServiceId;
            existing.StaffId = dto.StaffId;
            existing.StatusId = dto.StatusId;
            existing.TotalPrice = dto.TotalPrice;
            existing.Description = dto.Description;

            await _projectRepo.UpdateAsync(existing);
        }

        public async Task DeleteProjectAsync(string projectNumber)
        {
            await _projectRepo.DeleteAsync(projectNumber);
        }

        private async Task<string> GenerateProjectNumberAsync()
        {
            // Very simple approach: "P-[count + 101]"
            var all = await _projectRepo.GetAllAsync();
            int count = all.Count();

            // Example result: "P-101", "P-102", etc.
            return $"P-{count + 101}";
        }
    }
}
