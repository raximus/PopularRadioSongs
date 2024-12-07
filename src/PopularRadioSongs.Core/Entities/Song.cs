namespace PopularRadioSongs.Core.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Lookup { get; set; } = string.Empty;
        public List<Artist> Artists { get; set; } = new List<Artist>();
        public List<Playback> Playbacks { get; set; } = new List<Playback>();
    }
}