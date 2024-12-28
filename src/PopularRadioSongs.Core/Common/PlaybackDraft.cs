namespace PopularRadioSongs.Core.Common
{
    public class PlaybackDraft
    {
        public string SongTitle { get; }
        public List<string> Artists { get; }
        public DateTimeOffset PlayTime { get; }

        public PlaybackDraft(string songTitle, string artistName, DateTimeOffset playTime) : this(songTitle, artistName.Split(" / ").ToList(), playTime)
        { }

        public PlaybackDraft(string songTitle, List<string> artists, DateTimeOffset playTime)
        {
            SongTitle = StringsHelper.StandardizeString(songTitle);

            Artists = artists.Select(a => StringsHelper.StandardizeString(a)).Where(a => !string.IsNullOrWhiteSpace(a)).ToList();

            PlayTime = playTime;
        }

        public override string ToString()
        {
            return string.Format("Playback Song: {0}, Artists: {1}, played at {2}", SongTitle, string.Join(" | ", Artists), PlayTime);
        }
    }
}