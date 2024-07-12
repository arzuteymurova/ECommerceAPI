using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AppUser.AssignRoleToUser
{
    public class AssignRoleToUserCommandRequest : IRequest<AssignRoleToUserCommandResponse>
    {
        public Guid UserId { get; set; }
        public string[] Roles { get; set; }
    }
}