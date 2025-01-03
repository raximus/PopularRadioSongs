namespace PopularRadioSongs.Mvc.Models
{
    public record HomeViewModel(List<KeyValuePair<int, string>> RadioStationNames, bool IsDevelopment);
}