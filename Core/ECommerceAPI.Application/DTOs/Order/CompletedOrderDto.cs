namespace ECommerceAPI.Application.DTOs.Order
{
    public class CompletedOrderDto
    {
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
