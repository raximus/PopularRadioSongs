namespace PopularRadioSongs.Core.Entities
{
    public class Playback
    {
        public int Id { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; } = null!;
        public int RadioId { get; set; }
        public DateTimeOffset PlayTime { get; set; }
    }
}