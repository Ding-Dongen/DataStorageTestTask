﻿using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepo;
        private readonly IRoleService _roleService;

        public StaffService(IStaffRepository staffRepo, IRoleService roleService)
        {
            _staffRepo = staffRepo;
            _roleService = roleService;
        }

        public async Task<int> EnsureStaffAsync(StaffDto staffDto)
        {
            if (staffDto == null)
                throw new ArgumentException("Staff details cannot be null.");

            var roleId = await _roleService.EnsureRoleAsync(staffDto.RoleName);

            var existingStaff = await _staffRepo.GetByNameAndRoleIdAsync(staffDto.Name, roleId);
            if (existingStaff != null)
                return existingStaff.StaffId;

            var newStaff = new Staff
            {
                Name = staffDto.Name,
                RoleId = roleId
            };
            await _staffRepo.AddAsync(newStaff);
            return newStaff.StaffId;
        }


        public async Task<bool> CheckStaffExistsAsync(int staffId)
        {
            var staff = await _staffRepo.GetAsync(staffId);
            return staff != null;
        }

    }
}
