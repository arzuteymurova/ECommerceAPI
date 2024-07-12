using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories
{
    public class EndpointControllerReadRepository : ReadRepository<EndpointController>, IEndpointControllerReadRepository
    {
        public EndpointControllerReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
