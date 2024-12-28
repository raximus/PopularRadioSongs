using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Core.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PopularRadioSongs.Infrastructure.RadioStations
{
    public class EskaRadioStation : IRadioStation
    {
        private readonly HttpClient _httpClient;

        public EskaRadioStation(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public int Id => 3;
        public string Name => "Eska";

        public async Task<List<PlaybackDraft>> GetPlaybacksAsync(DateTimeOffset playbacksTime)
        {
            var playbacksSourceData = await GetPlaybacksSourceDataAsync(playbacksTime);

            var playbacks = ConvertDataToPlaybacks(playbacksSourceData);

            return playbacks;
        }

        private async Task<string> GetPlaybacksSourceDataAsync(DateTimeOffset playbacksTime)
        {
            var sourceAdress = string.Format("https://www.eska.pl/api/mobile/station/2380/was_played/?date={0}&hour={1}", playbacksTime.ToString("yyyy-MM-dd"), playbacksTime.Hour);
            var sourceResponse = await _httpClient.GetAsync(sourceAdress);
            return await sourceResponse.Content.ReadAsStringAsync();
        }

        private List<PlaybackDraft> ConvertDataToPlaybacks(string sourceData)
        {
            var responseData = JsonSerializer.Deserialize<List<EskaResponse>>(sourceData);

            if (responseData is not null)
            {
                var baseOffset = TimeZoneInfo.Local.BaseUtcOffset;
                return responseData.Where(d => !string.IsNullOrEmpty(d.Name) && d.Artists is not null && d.PlayDate.HasValue)
                    .Select(d => d.ToPlayback(baseOffset)).ToList();
            }

            return new List<PlaybackDraft>();
        }

        private class EskaResponse
        {
            [JsonPropertyName("name")]
            public string? Name { get; set; }
            [JsonPropertyName("play_date")]
            public DateTimeOffset? PlayDate { get; set; }
            [JsonPropertyName("artists")]
            public List<EskaArtist>? Artists { get; set; }

            public PlaybackDraft ToPlayback(TimeSpan baseOffset)
            {
                return new PlaybackDraft(Name!, Artists!.Select(a => a.Name!).ToList(), new DateTimeOffset(PlayDate!.Value.DateTime, baseOffset));
            }
        }

        private class EskaArtist
        {
            [JsonPropertyName("name")]
            public string? Name { get; set; }
        }
    }
}