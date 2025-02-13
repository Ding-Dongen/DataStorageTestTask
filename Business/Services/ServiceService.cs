using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepo;

        public ServiceService(IServiceRepository serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public async Task<int> EnsureServiceAsync(ServiceDto serviceDto)
        {
            if (serviceDto == null)
                throw new ArgumentException("Service details cannot be null.");

            var existingService = await _serviceRepo.GetByNameAsync(serviceDto.Name);
            if (existingService != null)
            {
                return existingService.ServiceId;
            }

            // 🔹 Step 2: Create new service
            var newService = new Service
            {
                Name = serviceDto.Name,
                HourlyPrice = serviceDto.HourlyPrice
            };

            await _serviceRepo.AddAsync(newService);
            return newService.ServiceId;
        }

    }
}
