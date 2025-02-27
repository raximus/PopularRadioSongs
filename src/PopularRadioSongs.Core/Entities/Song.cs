﻿using PopularRadioSongs.Core.Common;

namespace PopularRadioSongs.Core.Entities
{
    public class Song
    {
        public int Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Lookup { get; private set; } = string.Empty;

        private readonly List<Artist> _artists = new List<Artist>();
        public IReadOnlyCollection<Artist> Artists => _artists.AsReadOnly();

        private readonly List<Playback> _playbacks = new List<Playback>();
        public IReadOnlyCollection<Playback> Playbacks => _playbacks.AsReadOnly();

        public Song(string title, List<Artist>? artists = null)
        {
            Title = StringsHelper.StandardizeString(title);
            Lookup = StringsHelper.LookupString(Title);
            if (artists is not null)
            {
                _artists = artists;
            }
        }
        private Song() { }

        public int AppendArtists(List<Artist> artistsToAppend)
        {
            var appendedArtistsCount = 0;
            foreach (var artistToAppend in artistsToAppend)
            {
                if (!_artists.Any(a => a.Lookup == artistToAppend.Lookup))
                {
                    _artists.Add(artistToAppend);
                    appendedArtistsCount++;
                }
            }

            return appendedArtistsCount;
        }

        public override string ToString()
        {
            return string.Format("Song Id: {0}, Title: {1}, Lookup: {2}, Artists Count: {3}, Playbacks Count: {4}", Id, Title, Lookup, _artists.Count, _playbacks.Count);
        }
    }
}