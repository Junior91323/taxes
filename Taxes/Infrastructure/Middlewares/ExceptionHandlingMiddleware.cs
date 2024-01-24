using System.Net;
using Taxes.Common.Constants;
using Taxes.Common.Exceptions;
using Taxes.Common.Models;

namespace Taxes.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = MediaTypeNamesConstants.Json;

                response.StatusCode = exception is BaseHttpException 
                    ? (int)((BaseHttpException)exception).StatusCode 
                    : (int)HttpStatusCode.InternalServerError;

                var body = new ErrorResponse(exception.Message);

                await response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(body));
            }
        }
    }
}
