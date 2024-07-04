using ECommerceAPI.Application.DTOs.Order;

namespace ECommerceAPI.Application.DTOs
{
    public class ListOrder
    {
        public int TotalOrderCount { get; set; }
        public object Orders { get; set; }
    }
}
