using Taxes.Common.Enums.Paging;

namespace Taxes.Common.Models.Paging
{
    public class PageSortInfo
    {
        public PageSortInfo() : this(null, null, 0, 10) { }

        public PageSortInfo(int pageIndex, int pageSize) : this(null, null, pageIndex, pageSize) { }

        public PageSortInfo(string sortField, SortOrderEnum? sortOrder, int pageIndex, int pageSize)
        {
            SortField = !string.IsNullOrWhiteSpace(sortField) ? sortField : "Id";
            SortOrder = sortOrder ?? SortOrderEnum.Asc;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public string SortField { get; }

        public SortOrderEnum? SortOrder { get; }

        public int PageIndex { get; }

        public int PageSize { get; }
    }
}
