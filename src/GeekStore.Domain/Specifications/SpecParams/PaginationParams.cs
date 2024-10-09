namespace GeekStore.Domain.Specifications.SpecParams
{
    public class PaginationParams
    {
        private const int MaxPageSize = 10;
        public int PageIndex { get; set;} = 1;
        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}