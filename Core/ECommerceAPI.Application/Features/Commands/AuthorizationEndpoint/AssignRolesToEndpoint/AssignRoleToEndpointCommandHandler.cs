using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleToEndpoint
{
    public class AssignRoleToEndpointCommandHandler : IRequestHandler<AssignRoleToEndpointCommandRequest, AssignRoleToEndpointCommandResponse>
    {
        public Task<AssignRoleToEndpointCommandResponse> Handle(AssignRoleToEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
