using ECommerceAPI.Infrastructure.Services.Identity;

namespace ECommerceAPI.Application.Abstractions.Identity
{
    public interface IJWTTokenService
    {
        string GenerateJwt(JWTOptions jwtSettings);
    }
}
