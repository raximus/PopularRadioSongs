namespace PopularRadioSongs.Application.UseCases.Songs.GetSongDetails
{
    public record SongDetailsDto(int Id, string Title, List<ArtistSongDetailsDto> Artists, List<PlaybackSongDetailsDto> Playbacks);

    public record ArtistSongDetailsDto(int Id, string Name);

    public record PlaybackSongDetailsDto(int RadioId, DateTimeOffset PlayTime)
    {
        public string RadioName { get; set; } = string.Empty;
    }
}