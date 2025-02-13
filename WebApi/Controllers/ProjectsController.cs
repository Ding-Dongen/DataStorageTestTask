using System.Text.Json;
using Business.Dtos;
using Business.Interfaces;
using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController(
        IProjectService projectService,
        IStaffService staffService,
        IServiceService serviceService,
        AppDbContext context) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;
        private readonly IStaffService _staffService = staffService;
        private readonly IServiceService _serviceService = serviceService;
        private readonly AppDbContext _context = context;  

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        
        [HttpGet("{projectNumber}")]
        public async Task<IActionResult> Get(string projectNumber)
        {
            var project = await _projectService.GetProjectByNumberAsync(projectNumber);
            return project == null ? NotFound() : Ok(project);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto dto)
        {
            try
            {
                var newNumber = await _projectService.CreateProjectAsync(dto);
                return CreatedAtAction(nameof(Get), new { projectNumber = newNumber }, newNumber);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create-details")]
        public async Task<IActionResult> CreateProjectWithDetails([FromBody] ProjectCreateDetailedDto dto)
        {
            Console.WriteLine($"Received JSON: {JsonSerializer.Serialize(dto)}"); 

            if (dto == null)
                return BadRequest(new { message = "Invalid request. No data provided." });

            if (dto.Service == null)
            {
                Console.WriteLine("ERROR: Service is NULL!");  
                return BadRequest(new { message = "Service details are required." });
            }

            try
            {
                var serviceId = await _serviceService.EnsureServiceAsync(dto.Service);
                var staffId = await _staffService.EnsureStaffAsync(dto.Staff);

                dto.ServiceId = serviceId;
                dto.StaffId = staffId;

                var newNumber = await _projectService.CreateProjectWithDetailsAsync(dto);
                return CreatedAtAction(nameof(Get), new { projectNumber = newNumber }, newNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while creating the project." });
            }
        }



        [HttpPut("{projectNumber}")]
        public async Task<IActionResult> UpdateProject(string projectNumber, [FromBody] ProjectDto dto)
        {
            var existingProject = await _projectService.GetProjectByNumberAsync(projectNumber);
            if (existingProject == null)
            {
                return NotFound(new { message = $"Project '{projectNumber}' not found." });
            }

            var staffExists = await _staffService.CheckStaffExistsAsync(dto.StaffId);
            if (!staffExists)
            {
                return BadRequest(new { message = $"StaffId {dto.StaffId} does not exist." });
            }

            dto.ProjectNumber = projectNumber;
            await _projectService.UpdateProjectAsync(dto);
            return NoContent();
        }

        [HttpDelete("{projectNumber}")]
        public async Task<IActionResult> DeleteProject(string projectNumber)
        {
            var success = await _projectService.DeleteProjectAsync(projectNumber);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("statuses")]
        public async Task<IActionResult> GetStatuses()
        {
            var statuses = await _context.Statuses.ToListAsync();
            return Ok(statuses ?? new List<Status>());
        }

        [HttpGet("services")]
        public async Task<IActionResult> GetServices()
        {
            var services = await _context.Services.ToListAsync();
            return Ok(services ?? new List<Service>());
        }

        [HttpGet("staff")]
        public async Task<IActionResult> GetStaff()
        {
            try
            {
                var staff = await _context.Staff.Include(s => s.Role).ToListAsync();
                if (staff == null || !staff.Any())
                {
                    return NotFound(new { message = "No staff found in the database." });
                }
                return Ok(staff);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching staff: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while fetching staff." });
            }
        }


        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers ?? new List<Customer>());
        }
    }
}
