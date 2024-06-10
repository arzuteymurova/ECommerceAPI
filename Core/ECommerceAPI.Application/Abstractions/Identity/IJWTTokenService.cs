namespace ECommerceAPI.Application.Abstractions.Identity
{
    public interface IJWTTokenService
    {
        string GenerateJwt(IJWTOptions jwtSettings);
    }
}
