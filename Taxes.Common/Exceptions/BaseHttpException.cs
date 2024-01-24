using System.Net;

namespace Taxes.Common.Exceptions
{
    public class BaseHttpException : Exception
    {
        public BaseHttpException(string message, HttpStatusCode statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
