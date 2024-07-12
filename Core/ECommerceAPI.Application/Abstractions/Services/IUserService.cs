using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int refreshTokenLifeTime);
        Task UpdatePasswordAsync(Guid userId, string resetToken, string newPassword);
        Task<ListUser> GetAllUsers(int page, int size);
        Task AssignRoleToUserAsync(Guid userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(Guid userId);
    }
}
