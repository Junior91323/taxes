namespace Taxes.Common.Models.Responses
{
    public class ListResponse<T>
    {
        public int TotalCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
