using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IAuthorizationEndpointService
    {
        Task AssignRoleToEndpointAsync(string[] roles, string controller, string code, Type type);
        Task<List<string>> GetRolesToEndpointAsync(string code, string controller);
    }
}
