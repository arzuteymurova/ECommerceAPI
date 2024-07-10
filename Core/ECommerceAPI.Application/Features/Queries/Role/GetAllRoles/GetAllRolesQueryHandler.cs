using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Role.GetAllRoles
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetAllRolesQueryRequest, GetAllRolesQueryResponse>
    {
        private readonly IRoleService _roleService;

        public GetRoleByIdQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetAllRolesAsync(request.Page, request.Size);
            return new()
            {
                Roles = result.Roles,
                TotalRoleCount = result.TotalRoleCount,
            };
        }
    }
}
