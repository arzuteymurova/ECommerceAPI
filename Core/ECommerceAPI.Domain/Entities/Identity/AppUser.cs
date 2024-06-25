using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string?  RefreshToken { get; set; }
        public DateTime?  RefreshTokenEndDate { get; set; }
        public ICollection<Basket> Baskets { get; set; }
    }
}
