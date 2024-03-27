namespace E_CommerceProject.Business.Shared
{
    public class PageList<T>
    {
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IList<T> Items { get; set; }

        public PageList(IList<T> items, int pageIndex, int pageSize, int totalCount)
        {
            if(pageIndex < 0)
                throw new ArgumentOutOfRangeException(pageIndex.ToString(), $"'{pageIndex}' can't be less than zero.");
            if(pageSize <= 0)
                throw new ArgumentOutOfRangeException(pageSize.ToString(), $"'{pageSize}' can't be less than zero.");
            if(totalCount <= 0)
                throw new ArgumentOutOfRangeException(totalCount.ToString(), $"'{totalCount}' can't be less than or equal zero zero.");


            Items = items ?? throw new ArgumentNullException(nameof(items));
            Count = items.Count;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;

        }
    }
}
