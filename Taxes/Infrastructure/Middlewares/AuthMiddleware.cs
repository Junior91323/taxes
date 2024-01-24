using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Taxes.Infrastructure.Middlewares
{
    public class AuthMiddleware
    {
        private const string Role = "Role";

        private readonly RequestDelegate next;

        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var roles = context.Request.Headers[Role];

            if (!roles.IsNullOrEmpty())
            {
                var claims = roles.Select(role => new Claim(ClaimTypes.Role, role));

                var identity = new ClaimsIdentity(claims, Role);
                var principal = new ClaimsPrincipal(identity);

                context.User = principal;
            }

            await next.Invoke(context);
        }
    }
}
