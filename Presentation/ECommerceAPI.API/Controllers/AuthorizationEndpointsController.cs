using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Features.Commands.AuthorizationEndpoint.AssignRoleToEndpoint;
using ECommerceAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("[action]")]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest rolesToEndpointQueryRequest)
        {
            GetRolesToEndpointQueryResponse response = await _mediator.Send(rolesToEndpointQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRolesToEndpoint(AssignRoleToEndpointCommandRequest assignRolesToEndpointCommandRequest)
         {
            assignRolesToEndpointCommandRequest.Type = typeof(Program);
            AssignRoleToEndpointCommandResponse response = await _mediator.Send(assignRolesToEndpointCommandRequest);
            return Ok(response);
        }

              
    }
}
