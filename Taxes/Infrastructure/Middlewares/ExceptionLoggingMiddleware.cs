namespace Taxes.Infrastructure.Middlewares
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger logger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next;
            this.logger = loggerFactory.CreateLogger<ExceptionLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "{Message}", ex.Message);
                throw;
            }
        }
    }
}
