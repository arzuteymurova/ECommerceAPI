using ECommerceAPI.Application.Abstractions.Identity;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Infrastructure.Enums;
using ECommerceAPI.Infrastructure.Services;
using ECommerceAPI.Infrastructure.Services.Configurations;
using ECommerceAPI.Infrastructure.Services.Identity;
using ECommerceAPI.Infrastructure.Services.Storage;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IJWTTokenService,JWTTokenService>();
            services.AddScoped<IMailService,MailService>();
            services.AddScoped<IApplicationService,ApplicationServices>();
            services.AddScoped<IAuthorizationEndpointService,AuthorizationEndpointService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService,OrderService>();   

        }

        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    //services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    //services.AddScoped<IStorage, AWSStorage>();
                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
