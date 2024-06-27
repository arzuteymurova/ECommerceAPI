using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Basket;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Infrastructure.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderReadRepository _orderReadRepository;
        readonly IBasketWriteRepository _basketWriteRepository;
        readonly IBasketReadRepository _basketReadRepository;
        readonly IBasketItemReadRepository _basketItemReadRepository;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketReadRepository basketReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketReadRepository = basketReadRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
        }

        public Basket? GetUserActiveBasket
        {
            get
            {
                Basket? basket = ContextUserBasket().Result;
                return basket;
            }
        }
        private async Task<Basket> ContextUserBasket()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(username))
            {

                AppUser? user = await _userManager.Users
                    .Include(u => u.Baskets)
                    .FirstOrDefaultAsync(u => u.UserName == username);

                if (user != null)
                {
                    var _basket = from basket in user.Baskets
                                  join order in _orderReadRepository.Table
                                  on basket.Id equals order.Id into BasketOrders
                                  from order in BasketOrders.DefaultIfEmpty()
                                  select new
                                  {
                                      Basket = basket,
                                      Order = order
                                  };

                    Basket? targetBasket = null;
                    if (_basket.Any(b => b.Order is null))
                        targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                    else
                    {
                        targetBasket = new();
                        user.Baskets.Add(targetBasket);
                    }

                    await _basketWriteRepository.SaveAsync();
                    return targetBasket;
                }
                else
                    throw new UserNotFoundException();
            }
            else
                throw new UserNameIsNullOrEmptyException();
        }

        public async Task AddItemToBasketAsync(CreateBasketItemRequestDto createBasketItem)
        {
            Basket? basket = await ContextUserBasket();

            if (basket != null)
            {
                BasketItem basketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == createBasketItem.ProductId);

                if (basketItem != null)
                    basketItem.Quantity++;
                else
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = createBasketItem.ProductId,
                        Quantity = createBasketItem.Quantity,
                    });
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket? basket = await ContextUserBasket();

            Basket? result = await _basketReadRepository.Table
                  .Include(b => b.BasketItems)
                  .ThenInclude(bi => bi.Product)
                  .FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems.ToList();
        }



        public async Task RemoveBasketItemAsync(Guid basketItemId)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);

            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(UpdateBasketItemRequestDto updateBasketItem)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(updateBasketItem.BasketItemId);

            if (basketItem != null)
            {
                basketItem.Quantity = updateBasketItem.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }

    }
}
