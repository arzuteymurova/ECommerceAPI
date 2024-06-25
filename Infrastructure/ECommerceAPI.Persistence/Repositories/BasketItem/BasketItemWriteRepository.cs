using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;

namespace ECommerceAPI.Persistence.Repositories
{
    public class BasketItemWriteRepository : WriteRepository<BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
