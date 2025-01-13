namespace PopularRadioSongs.Mvc.Models
{
    public record PagedViewModel<T>(PagedData PagedData, T Value);

    public record PagedData(int Page, int PageSize, int TotalPages)
    {
        public bool HavePrevious => Page > 1;
        public bool HaveNext => Page < TotalPages;
        public bool SmallStyle => TotalPages >= 30;
    }
}