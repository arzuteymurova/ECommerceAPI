namespace ECommerceAPI.Application.DTOs.Basket
{
    public class CreateBasketItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
