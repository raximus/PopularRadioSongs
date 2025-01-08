namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsList
{
    public record GroupSongListDto(string Letter, List<SongListDto> Songs);

    public record SongListDto(int Id, string Title, List<ArtistSongListDto> Artists);

    public record ArtistSongListDto(int Id, string Name);
}