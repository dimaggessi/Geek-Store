namespace GeekStore.Domain.Shared
{
    public sealed record PaginatedResult<T>
    {
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; } = 0;
        public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
        public T? Data { get; init; }

    }
}
