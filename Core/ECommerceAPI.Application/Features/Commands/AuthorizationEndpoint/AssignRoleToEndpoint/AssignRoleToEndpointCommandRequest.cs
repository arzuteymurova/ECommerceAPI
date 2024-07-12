using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleToEndpoint
{
    public class AssignRoleToEndpointCommandRequest : IRequest<AssignRoleToEndpointCommandResponse>
    {
        public string[] Roles { get; set; }
        public string Code { get; set; }
        public string Controller { get; set; }
        public Type? Type { get; set; }
    }
}