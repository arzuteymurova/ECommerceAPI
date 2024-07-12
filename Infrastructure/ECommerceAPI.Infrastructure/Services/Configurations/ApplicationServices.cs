using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using EndpointController = ECommerceAPI.Application.DTOs.Configuration.EndpointController;

namespace ECommerceAPI.Infrastructure.Services.Configurations
{
    public class ApplicationServices : IApplicationService
    {
        public List<EndpointController> GetAuthorizeDefinitionEndpoints(Type type)
        {
            var assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes()
                                      .Where(t => typeof(ControllerBase).IsAssignableFrom(t));

            List<EndpointController> endpointControllers = new();

            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods()
                                        .Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));

                foreach (var action in actions)
                {
                    var attributes = action.GetCustomAttributes(true);

                    var authorizeDefinition = attributes.OfType<AuthorizeDefinitionAttribute>().FirstOrDefault();
                    if (authorizeDefinition == null) continue;

                    var endpointController = endpointControllers.FirstOrDefault(m => m.Name == authorizeDefinition.Controller) ??
                               new EndpointController { Name = authorizeDefinition.Controller, Actions = new List<Application.DTOs.Configuration.Action>() };

                    if (!endpointControllers.Any(m => m.Name == authorizeDefinition.Controller))
                    {
                        endpointControllers.Add(endpointController);
                    }

                    var actionType = Enum.GetName(typeof(ActionType), authorizeDefinition.ActionType);
                    var httpAttribute = attributes.OfType<HttpMethodAttribute>().FirstOrDefault();

                    var actionDto = new Application.DTOs.Configuration.Action
                    {
                        ActionType = actionType,
                        Definition = authorizeDefinition.Definition,
                        HttpType = httpAttribute?.HttpMethods.FirstOrDefault() ?? HttpMethods.Get,
                        Code = $"{httpAttribute?.HttpMethods.FirstOrDefault() ?? HttpMethods.Get}.{actionType}.{authorizeDefinition.Definition.Replace(" ", "")}"
                    };

                    endpointController.Actions.Add(actionDto);
                }
            }
            return endpointControllers;
        }
    }
}
