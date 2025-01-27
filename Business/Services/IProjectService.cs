using Business.Dtos;


namespace Business.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByNumberAsync(string projectNumber);
        Task<string> CreateProjectAsync(ProjectDto dto);
        Task UpdateProjectAsync(ProjectDto dto);
        Task DeleteProjectAsync(string projectNumber);
    }
}
