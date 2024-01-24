using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Taxes.Common.Enums.Auth;
using Taxes.Common.Exceptions;

namespace Taxes.Infrastructure.Filters
{
    public class AuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly Roles[] _roles;

        public AuthAttribute(params Roles[] roles)
        {
            this._roles = roles;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userRoles = context.HttpContext.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

            if (!this._roles.Any(role => userRoles.Contains(role.ToString())) && !userRoles.Contains(Roles.Admin.ToString()))
            {
                throw new ForbiddenException("Unauthorized: User does not have the required roles");
            }

            return Task.CompletedTask;
        }
    }
}
