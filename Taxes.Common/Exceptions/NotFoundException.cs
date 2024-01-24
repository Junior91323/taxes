using System.Net;

namespace Taxes.Common.Exceptions
{
    public class NotFoundException : BaseHttpException
    {
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}
