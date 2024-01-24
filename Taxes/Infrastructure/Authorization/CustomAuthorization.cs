using Taxes.Infrastructure.Middlewares;

namespace Taxes.Infrastructure.Authorization
{
    public static class CustomAuthorization
    {
        public static void UseCustomAuthorization(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthMiddleware>();
        }
    }
}
