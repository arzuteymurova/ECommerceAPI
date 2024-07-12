using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.DTOs.User
{
    public class ListUser
    {
        public List<AppUser> Users { get; set; }

        public int TotalUserCount { get; set; }
    }
}
