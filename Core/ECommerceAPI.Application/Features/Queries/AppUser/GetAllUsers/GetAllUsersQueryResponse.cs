using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Features.Queries

{
    public class GetAllUsersQueryResponse
    {
        public List<Domain.Entities.Identity.AppUser> Users { get; set; }
        public int TotalUserCount { get; set; }
    }
}