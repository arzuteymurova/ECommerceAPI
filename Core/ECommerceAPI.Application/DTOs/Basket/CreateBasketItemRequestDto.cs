namespace ECommerceAPI.Application.DTOs.Basket
{
    public class CreateBasketItemRequestDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
