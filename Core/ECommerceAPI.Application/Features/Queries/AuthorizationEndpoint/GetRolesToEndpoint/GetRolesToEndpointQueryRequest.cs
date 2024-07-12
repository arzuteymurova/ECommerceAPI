using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryRequest : IRequest<GetRolesToEndpointQueryResponse>
    {
        public string Code { get; set; }
        public string Controller { get; set; }
    }
}