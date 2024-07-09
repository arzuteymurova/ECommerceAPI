using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.DTOs.Configuration;
using ECommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ECommerceAPI.Infrastructure.Configurations
{
    public class ApplicationServices : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            var assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes()
                                      .Where(t => typeof(ControllerBase).IsAssignableFrom(t));

            var menus = new List<Menu>();

            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods()
                                        .Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));

                foreach (var action in actions)
                {
                    var attributes = action.GetCustomAttributes(true);

                    var authorizeDefinition = attributes.OfType<AuthorizeDefinitionAttribute>().FirstOrDefault();
                    if (authorizeDefinition == null) continue;

                    var menu = menus.FirstOrDefault(m => m.Name == authorizeDefinition.Menu) ??
                               new Menu { Name = authorizeDefinition.Menu, Actions = new List<Application.DTOs.Configuration.Action>() };

                    if (!menus.Any(m => m.Name == authorizeDefinition.Menu))
                    {
                        menus.Add(menu);
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

                    menu.Actions.Add(actionDto);
                }
            }
            return menus;
        }
    }
}
