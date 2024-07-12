using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories
{
    public class EndpointControllerWriteRepository : WriteRepository<EndpointController>, IEndpointControllerWriteRepository
    {
        public EndpointControllerWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
