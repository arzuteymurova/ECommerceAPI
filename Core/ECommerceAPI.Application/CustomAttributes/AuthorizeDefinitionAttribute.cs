using ECommerceAPI.Application.Enums;

namespace ECommerceAPI.Application.CustomAttributes
{
    public class AuthorizeDefinitionAttribute : Attribute
    {
        public string Controller { get; set; }
        public string Definition { get; set; }
        public ActionType ActionType { get; set; }
    }
}
