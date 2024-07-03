namespace ECommerceAPI.Application.DTOs.Basket
{
    public class UpdateBasketItem
    {
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
