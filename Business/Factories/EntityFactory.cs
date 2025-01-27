
using Business.Dtos;
using Data.Entities;

namespace Business.Factories
{
    public static class EntityFactory
    {
        public static Project CreateProject(ProjectDto dto, string generatedNumber)
        {
            return new Project
            {
                ProjectNumber = generatedNumber, // override any user input
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
        }
    }
}
