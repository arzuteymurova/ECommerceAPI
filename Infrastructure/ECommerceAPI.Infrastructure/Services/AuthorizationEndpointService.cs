using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Infrastructure.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        private readonly IEndpointWriteRepository _endpointWriteRepository;
        private readonly IEndpointReadRepository _endpointReadRepository;
        private readonly IEndpointControllerReadRepository _endpointControllerReadRepository;
        private readonly IEndpointControllerWriteRepository _endpointControllerWriteRepository;
        private readonly IApplicationService _applicationService;
        readonly RoleManager<AppRole> _roleManager;
        public AuthorizationEndpointService(IEndpointWriteRepository endpointWriteRepository, IEndpointReadRepository endpointReadRepository,
            IEndpointControllerReadRepository endpointControllerReadRepository, IEndpointControllerWriteRepository endpointControllerWriteRepository, IApplicationService applicationService, RoleManager<AppRole> roleManager)
        {
            _endpointWriteRepository = endpointWriteRepository;
            _endpointReadRepository = endpointReadRepository;
            _endpointControllerReadRepository = endpointControllerReadRepository;
            _endpointControllerWriteRepository = endpointControllerWriteRepository;
            _applicationService = applicationService;
            _roleManager = roleManager;
        }

        public async Task AssignRoleToEndpointAsync(string[] roles, string controller, string code, Type type)
        {
            EndpointController endpointController = await _endpointControllerReadRepository.GetSingleAsync(c => c.Name == controller);
            if (endpointController == null)
            {
                endpointController = new()
                {
                    Name = controller
                };
                await _endpointControllerWriteRepository.AddAsync(endpointController);

                await _endpointControllerWriteRepository.SaveAsync();
            }

            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.EndpointController)
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code && e.EndpointController.Name == controller);

            if (endpoint == null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndpoints(type)
                    .FirstOrDefault(c => c.Name == controller)?.Actions.FirstOrDefault(a => a.Code == code);

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    EndpointController = endpointController
                };

                await _endpointWriteRepository.AddAsync(endpoint);
                await _endpointWriteRepository.SaveAsync();
            }

            endpoint.Roles.Clear();

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
                endpoint.Roles.Add(role);

            await _endpointWriteRepository.SaveAsync();
        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string controller)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table
                   .Include(e => e.EndpointController)
                   .Include(e => e.Roles)
                   .FirstOrDefaultAsync(e => e.Code == code && e.EndpointController.Name == controller);

            if (endpoint != null)
                return endpoint.Roles.Select(r => r.Name).ToList();

            return null;
        }
    }
}
