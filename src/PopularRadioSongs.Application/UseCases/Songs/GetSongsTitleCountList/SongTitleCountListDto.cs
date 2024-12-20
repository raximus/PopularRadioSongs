namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList
{
    public record SongTitleCountListDto(string Title, int SongsCount, List<SongTitleCountDto> Songs);

    public record SongTitleCountDto(int Id, string Title, List<ArtistSongTitleCountDto> Artists);

    public record ArtistSongTitleCountDto(int Id, string Name);
}