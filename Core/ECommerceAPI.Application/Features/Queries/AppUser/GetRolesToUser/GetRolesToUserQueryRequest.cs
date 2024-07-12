using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolesToUserQueryRequest : IRequest<GetRolesToUserQueryResponse>
    {
        public Guid UserId { get; set; }
    }
}