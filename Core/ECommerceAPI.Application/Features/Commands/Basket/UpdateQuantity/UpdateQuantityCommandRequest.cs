using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Basket.UpdateQuantity
{
    public class UpdateQuantityCommandRequest : IRequest<UpdateQuantityCommandResponse>
    {
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
