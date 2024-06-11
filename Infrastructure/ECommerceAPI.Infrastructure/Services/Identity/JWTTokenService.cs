using ECommerceAPI.Application.Abstractions.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ECommerceAPI.Infrastructure.Services.Identity
{
    public class JWTTokenService : IJWTTokenService
    {
        public string GenerateJwt(JWTOptions jwtSettings)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime accessTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings.ExpirationInMinutes));

            JwtSecurityToken token = new(
                issuer: jwtSettings.Issuer,

                audience: jwtSettings.Audience,
                expires: accessTokenExpiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
