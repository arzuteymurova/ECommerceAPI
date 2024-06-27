namespace ECommerceAPI.Application.DTOs.Basket
{
    public class UpdateBasketItemRequestDto
    {
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
