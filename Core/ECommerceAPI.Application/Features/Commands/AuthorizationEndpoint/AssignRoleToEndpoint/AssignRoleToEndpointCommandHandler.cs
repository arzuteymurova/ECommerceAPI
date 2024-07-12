using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleToEndpoint
{
    public class AssignRoleToEndpointCommandHandler : IRequestHandler<AssignRoleToEndpointCommandRequest, AssignRoleToEndpointCommandResponse>
    {
        private readonly IAuthorizationEndpointService _authorizationEndpointService;

        public AssignRoleToEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<AssignRoleToEndpointCommandResponse> Handle(AssignRoleToEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            await _authorizationEndpointService.AssignRoleToEndpointAsync(request.Roles, request.Controller, request.Code, request.Type);

            return new();
        }
    }
}
