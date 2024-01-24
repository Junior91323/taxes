namespace Taxes.Common.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }
}
