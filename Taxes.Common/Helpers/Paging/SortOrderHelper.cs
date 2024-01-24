using Taxes.Common.Enums.Paging;

namespace Taxes.Common.Helpers.Paging
{
    public static class SortOrderHelper
    {
        private const string Desc = "DESC";

        private const string Asc = "ASC";

        public static SortOrderEnum ConvertToSortOrder(this string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortOrder))
            {
                throw new ArgumentNullException(nameof(sortOrder));
            }

            switch (sortOrder.ToUpper())
            {
                case Desc:
                    return SortOrderEnum.Desc;
                case Asc:
                    return SortOrderEnum.Asc;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder));
            }
        }
    }
}
