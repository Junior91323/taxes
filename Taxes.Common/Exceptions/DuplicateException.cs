using System.Net;

namespace Taxes.Common.Exceptions
{
    public class DuplicateException : BaseHttpException
    {
        public DuplicateException(string message) : base(message, HttpStatusCode.Conflict)
        {
        }
    }
}
