namespace ECommerceAPI.Application.DTOs.Order
{
    public class CreateOrder
    {
        public Guid BasketId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
