namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsList
{
    public record GroupArtistListDto(string Letter, List<ArtistListDto> Artists);
    public record ArtistListDto(int Id, string Name);
}