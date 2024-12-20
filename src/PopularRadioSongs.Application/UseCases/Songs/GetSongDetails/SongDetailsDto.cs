namespace PopularRadioSongs.Application.UseCases.Songs.GetSongDetails
{
    public record SongDetailsDto(int Id, string Title, List<ArtistSongDetailsDto> Artists);

    public record ArtistSongDetailsDto(int Id, string Name);
}