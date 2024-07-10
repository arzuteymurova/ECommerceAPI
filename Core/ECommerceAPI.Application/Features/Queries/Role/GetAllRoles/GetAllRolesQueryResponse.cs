using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryResponse
    {
        public List<AppRole> Roles { get; set; }
        public int TotalRoleCount { get; set; }
    }
}