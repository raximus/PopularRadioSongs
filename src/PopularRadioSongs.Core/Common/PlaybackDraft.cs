namespace PopularRadioSongs.Core.Common
{
    public class PlaybackDraft
    {
        public string SongTitle { get; }
        public string SongLookup { get; }
        public List<ArtistDraft> Artists { get; }
        public DateTimeOffset PlayTime { get; }

        public PlaybackDraft(string songTitle, string artistName, DateTimeOffset playTime) : this(songTitle, artistName.Split(" / ").ToList(), playTime)
        { }

        public PlaybackDraft(string songTitle, List<string> artists, DateTimeOffset playTime)
        {
            SongTitle = StringsHelper.StandardizeString(songTitle);
            SongLookup = StringsHelper.LookupString(SongTitle);

            Artists = artists.Select(a => StringsHelper.StandardizeString(a)).Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => new ArtistDraft(a)).ToList();

            PlayTime = playTime;
        }

        public override string ToString()
        {
            return string.Format("Playback Song: {0}, Artists: {1}, played at {2}", SongTitle, string.Join(" | ", Artists), PlayTime);
        }
    }

    public class ArtistDraft
    {
        public string Name { get; }
        public string Lookup { get; }

        public ArtistDraft(string name)
        {
            Name = name;
            Lookup = StringsHelper.LookupString(Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}