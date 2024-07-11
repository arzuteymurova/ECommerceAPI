using ECommerceAPI.Application.Abstractions.Services;

namespace ECommerceAPI.Infrastructure.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        public Task AssignRoleToEndpointAsync(string[] roles, string code)
        {
            throw new NotImplementedException();
        }
    }
}
