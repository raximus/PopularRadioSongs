namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails
{
    public record ArtistDetailsDto(int Id, string Name, List<SongArtistDetailsDto> Songs);

    public record SongArtistDetailsDto(int Id, string Title);
}