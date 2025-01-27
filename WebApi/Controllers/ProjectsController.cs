using Business.Dtos;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _projectService.GetAllProjectsAsync());

        [HttpGet("{projectNumber}")]
        public async Task<IActionResult> Get(string projectNumber)
        {
            var project = await _projectService.GetProjectByNumberAsync(projectNumber);
            return project == null ? NotFound() : Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectDto dto)
        {
            var newProjectNumber = await _projectService.CreateProjectAsync(dto);
            return CreatedAtAction(nameof(Get), new { projectNumber = newProjectNumber }, newProjectNumber);
        }

        [HttpPut("{projectNumber}")]
        public async Task<IActionResult> Update(string projectNumber, [FromBody] ProjectDto dto)
        {
            dto.ProjectNumber = projectNumber;
            await _projectService.UpdateProjectAsync(dto);
            return NoContent();
        }

        [HttpDelete("{projectNumber}")]
        public async Task<IActionResult> Delete(string projectNumber)
        {
            await _projectService.DeleteProjectAsync(projectNumber);
            return NoContent();
        }
    }
}
