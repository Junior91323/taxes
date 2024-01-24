using System.Net;

namespace Taxes.Common.Exceptions
{
    public class ForbiddenException : BaseHttpException
    {
        public ForbiddenException(string message) : base(message, HttpStatusCode.Forbidden)
        {
        }
    }
}
