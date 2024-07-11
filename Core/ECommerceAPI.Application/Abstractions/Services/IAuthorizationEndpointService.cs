namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IAuthorizationEndpointService
    {
        public Task AssignRoleToEndpointAsync(string[] roles, string code);
    }
}
