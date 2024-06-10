using ECommerceAPI.Application.Abstractions.Identity;

namespace ECommerceAPI.Infrastructure.Services.Identity
{
    public class JWTOptions : IJWTOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
