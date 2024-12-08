namespace PopularRadioSongs.Core.Entities
{
    public class Artist
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Lookup { get; private set; } = string.Empty;

        private readonly List<Song> _songs = new List<Song>();
        public IReadOnlyCollection<Song> Songs => _songs.AsReadOnly();

        public Artist(string name)
        {
            Name = name;
            Lookup = name.ToLower();
        }
        private Artist() { }

        public override string ToString()
        {
            return string.Format("Artist Id: {0}, Name: {1}, Lookup: {2}, Songs Count: {3}", Id, Name, Lookup, _songs.Count);
        }
    }
}