namespace ECommerceAPI.Application.DTOs.Configuration
{
    public class Controller
    {
        public string Name { get; set; }
        public List<Action> Actions { get; set; } = new();
    }
}
