using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class RoleService(IRoleRepository roleRepo) : IRoleService
    {
        private readonly IRoleRepository _roleRepo = roleRepo;

        public async Task<int> EnsureRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Role name cannot be empty.");

            var existing = await _roleRepo.GetByNameAsync(roleName);
            if (existing != null)
            {
                return existing.Id;
            }
            else
            {
                var newRole = new Role { Name = roleName };
                await _roleRepo.AddAsync(newRole);
                return newRole.Id;
            }
        }

        public async Task<int> GetRoleIdByNameAsync(string roleName)
        {
            var role = await _roleRepo.GetByNameAsync(roleName);
            if (role == null)
            {
                throw new ArgumentException($"Role '{roleName}' does not exist.");
            }
            return role.Id;
        }
    }
}
