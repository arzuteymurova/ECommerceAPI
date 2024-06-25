using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasket(BasketItem basketItem);
        public Task UpdateQuantityAsync(BasketItem basketItem);
        public Task RemoveBasketItemAsync(Guid basketItemId);
    }
}
