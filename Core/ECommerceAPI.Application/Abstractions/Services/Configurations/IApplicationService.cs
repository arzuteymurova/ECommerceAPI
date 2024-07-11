using ECommerceAPI.Application.DTOs.Configuration;

namespace ECommerceAPI.Application.Abstractions.Services.Configurations
{
    public interface IApplicationService
    {
        List<Controller> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
