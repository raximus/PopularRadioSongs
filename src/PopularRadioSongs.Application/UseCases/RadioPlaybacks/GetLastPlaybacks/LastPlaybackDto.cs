namespace PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks
{
    public record LastPlaybacksDto(string RadioName, List<PlaybackLastPlaybacksDto> Playbacks);

    public record PlaybackLastPlaybacksDto(DateTimeOffset PlayTime, int SongId, string SongTitle, List<ArtistLastPlaybackDto> Artists);

    public record ArtistLastPlaybackDto(int Id, string Name);
}