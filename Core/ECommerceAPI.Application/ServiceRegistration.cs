using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerceAPI.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services) {

            services.AddMediatR(s => s.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        }
    }
}
