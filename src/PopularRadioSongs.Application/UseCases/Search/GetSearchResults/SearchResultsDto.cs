namespace PopularRadioSongs.Application.UseCases.Search.GetSearchResults
{
    public record SearchResultsDto(string SearchValue, List<ResultSearchResultsDto> Results);

    public record ResultSearchResultsDto(SongSearchResultsDto? Song, ArtistSearchResultsDto? Artist);

    public record SongSearchResultsDto(int Id, string Title, List<ArtistSearchResultsDto> Artists);

    public record ArtistSearchResultsDto(int Id, string Name);
}