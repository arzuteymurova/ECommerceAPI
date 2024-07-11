using ECommerceAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpPost]
        public async Task<IActionResult> AssignRolesToEndpoint(AssignRoleToEndpointCommandRequest assignRolesToEndpointCommandRequest)
        {
           AssignRoleToEndpointCommandResponse response = await _mediator.Send(assignRolesToEndpointCommandRequest);
            return Ok(response);
        }
    }
}
