using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
    {
        readonly IBasketService _basketService;

        public GetBasketItemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItemsAsync();
            var response = basketItems.Select(bi => new GetBasketItemsQueryResponse
            {
                BasketItemId = bi.Id,
                Name = bi.Product.Name,
                Price = bi.Product.Price,
                Quantity = bi.Quantity
            }).ToList();

            return response;
        }
    }
}
