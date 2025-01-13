namespace PopularRadioSongs.Application.UseCases
{
    public record PagedQuery
    {
        public int Page { get; init; }
        public int PageSize { get; init; }

        public PagedQuery(int page, int pageSize, int maxPageSize)
        {
            Page = Math.Max(page, 1);
            PageSize = Math.Max(Math.Min(pageSize, maxPageSize), maxPageSize / 10);
        }
    }
}