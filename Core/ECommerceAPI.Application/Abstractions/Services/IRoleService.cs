using ECommerceAPI.Application.DTOs.Role;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<ListRole> GetAllRolesAsync(int page, int size);
        Task<AppRole> GetRoleByIdAsync(Guid id);
        Task<bool> CreateRoleAsync(string name);
        Task<bool> DeleteRoleAsync(Guid id);
        Task<bool> UpdateRoleAsync(Guid id, string name);
    }
}
