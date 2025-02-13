﻿using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class StaffRepository(AppDbContext context) : BaseRepository<Staff>(context), IStaffRepository
{
    public async Task<Staff> GetByNameAndRoleIdAsync(string staffName, int roleId)
    {
        return await _context.Staff
            .FirstOrDefaultAsync(s => s.Name == staffName && s.RoleId == roleId);
    }
}
