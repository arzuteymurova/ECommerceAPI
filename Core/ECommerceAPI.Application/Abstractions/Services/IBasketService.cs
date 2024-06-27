using ECommerceAPI.Application.DTOs.Basket;
using ECommerceAPI.Domain.Entities;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(CreateBasketItemRequestDto createBasketItem);
        public Task UpdateQuantityAsync(UpdateBasketItemRequestDto updateBasketItem);
        public Task RemoveBasketItemAsync(Guid basketItemId);
        public Basket? GetUserActiveBasket {  get; }
    }
}
