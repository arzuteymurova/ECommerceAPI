namespace ECommerceAPI.Application.Abstractions.Identity
{
    public interface IJWTOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
