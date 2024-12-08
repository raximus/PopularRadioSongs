namespace PopularRadioSongs.Core.Entities
{
    public class Playback
    {
        public int Id { get; private set; }
        public int SongId { get; private set; }
        public Song Song { get; private set; } = null!;
        public int RadioId { get; private set; }
        public DateTimeOffset PlayTime { get; private set; }

        public Playback(Song song, int radioId, DateTimeOffset playTime)
        {
            if (song != null)
            {
                SongId = song.Id;
                Song = song;
            }
            RadioId = radioId;
            PlayTime = playTime;
        }
        private Playback() { }

        public override string ToString()
        {
            return string.Format("Playback Id: {0}, SongId: {1}, RadioId: {2}, PlayTime: {3}", Id, SongId, RadioId, PlayTime);
        }
    }
}