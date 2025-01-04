namespace PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks
{
    public record LastPlaybacksDto(string RadioName, List<PlaybackGroupLastPlaybacksDto> PlaybackGroups);

    public record PlaybackGroupLastPlaybacksDto(DateTimeOffset FromTime, DateTimeOffset ToTime, List<PlaybackLastPlaybacksDto> Playbacks);

    public record PlaybackLastPlaybacksDto(DateTimeOffset PlayTime, int SongId, string SongTitle, List<ArtistLastPlaybacksDto> Artists);

    public record ArtistLastPlaybacksDto(int Id, string Name);
}