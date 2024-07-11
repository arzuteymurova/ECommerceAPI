using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Role;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            IdentityResult result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<ListRole> GetAllRolesAsync(int page, int size)
        {
            var query = _roleManager.Roles;

            IQueryable<AppRole>? rolesQuery = null;

            if (page != -1 && size != -1)
                rolesQuery = query.Skip(page * size).Take(size);
            else
                rolesQuery = query;

            return new()
            {
                Roles = await rolesQuery.ToListAsync(),
                TotalRoleCount = await query.CountAsync(),
            };
        }

        public async Task<AppRole> GetRoleByIdAsync(Guid id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            return role;
        }

        public async Task<bool> UpdateRoleAsync(Guid id, string name)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
