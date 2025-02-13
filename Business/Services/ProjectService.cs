using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class ProjectService(IProjectRepository projectRepo,
                          IStaffService staffService,
                          IServiceService serviceService) : IProjectService
    {
        private readonly IProjectRepository _projectRepo = projectRepo;
        private readonly IStaffService _staffService = staffService;
        private readonly IServiceService _serviceService = serviceService;
        


        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepo.GetAllAsync();
            return projects.Select(p => new ProjectDto
            {
                ProjectNumber = p.ProjectNumber,
                Name = p.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                CustomerName = p.Customer != null ? p.Customer.Name : "Ingen kund",
                ServiceId = p.ServiceId,
                StaffId = p.StaffId,
                StatusName = p.Status.Name != null ? p.Status.Name : "Ingen status",
                TotalPrice = p.TotalPrice,
                Description = p.Description,


                 Service = p.Service != null ? new ServiceDto
                 {
                     Name = p.Service.Name,
                     HourlyPrice = p.Service.HourlyPrice
                 } : null,

                 Staff = p.Staff != null
            ? new StaffDto
            {
                Name = p.Staff.Name,
                RoleName = p.Staff.Role.Name ?? "Unknown role" 
            }
            : null
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
                CustomerName = project.Customer?.Name ?? "N/A",
                ServiceId = project.ServiceId,
                StaffId = project.StaffId,
                StatusId = project.StatusId,
                TotalPrice = project.TotalPrice,
                Description = project.Description
            };
        }

        public async Task<string> CreateProjectAsync(ProjectDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            int serviceId = await _serviceService.EnsureServiceAsync(dto.Service);

            int staffId = await _staffService.EnsureStaffAsync(dto.Staff);

            // Insert project entity directly
            var project = new Project
            {
                ProjectNumber = dto.ProjectNumber,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CustomerId = dto.CustomerId,
                ServiceId = dto.ServiceId,
                StaffId = dto.StaffId,
                StatusId = dto.StatusId,
                TotalPrice = dto.TotalPrice,
                Description = dto.Description
            };

            await _projectRepo.AddAsync(project);
            return dto.ProjectNumber;
        }

        public async Task<string> CreateProjectWithDetailsAsync(ProjectCreateDetailedDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.ProjectNumber))
                throw new ArgumentException("ProjectNumber is required.");

            var serviceId = await _serviceService.EnsureServiceAsync(dto.Service);

            var staffId = await _staffService.EnsureStaffAsync(dto.Staff);

            // 🔹 Step 3: Construct the Project entity
            var projectEntity = new Project
            {
                ProjectNumber = dto.ProjectNumber,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CustomerId = dto.CustomerId,
                ServiceId = serviceId,
                StaffId = staffId,
                StatusId = dto.StatusId,
                TotalPrice = dto.TotalPrice,
                Description = dto.Description
            };

            await _projectRepo.AddAsync(projectEntity);

            return dto.ProjectNumber;
        }


        // -----------------------------
        // UPDATE
        public async Task UpdateProjectAsync(ProjectDto dto)
        {
            var existing = await _projectRepo.GetAsync(dto.ProjectNumber);
            if (existing == null) return;

            existing.Name = dto.Name;
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;
            existing.CustomerId = dto.CustomerId;
            existing.ServiceId = await _serviceService.EnsureServiceAsync(dto.Service);
            existing.StaffId = await _staffService.EnsureStaffAsync(dto.Staff);
            existing.StatusId = dto.StatusId;
            existing.TotalPrice = dto.TotalPrice;
            existing.Description = dto.Description;

            await _projectRepo.UpdateAsync(existing);
        }

        public async Task<bool> DeleteProjectAsync(string projectNumber)
        {
            await _projectRepo.DeleteAsync(projectNumber);
            return true;
        }
    }
}
