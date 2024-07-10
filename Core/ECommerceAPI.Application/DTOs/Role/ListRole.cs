using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.DTOs.Role
{
    public class ListRole
    {
        public List<AppRole> Roles { get; set; }

        public int TotalRoleCount { get; set; }
    }
}
