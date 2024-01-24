using Taxes.Infrastructure.Middlewares;

namespace Taxes.Infrastructure.ExceptionHandling
{
    public static class CustomExceptionHandler
    {
        public static void AddCustomExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandling();
            app.UseExceptionLogging();
        }

        private static void UseExceptionLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionLoggingMiddleware>();
        }

        private static void UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
