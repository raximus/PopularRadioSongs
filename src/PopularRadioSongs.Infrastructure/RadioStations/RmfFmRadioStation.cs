using Microsoft.Extensions.Logging;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Core.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PopularRadioSongs.Infrastructure.RadioStations
{
    public class RmfFmRadioStation : IRadioStation
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RmfFmRadioStation> _logger;

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { NumberHandling = JsonNumberHandling.AllowReadingFromString };

        public RmfFmRadioStation(HttpClient httpClient, ILogger<RmfFmRadioStation> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public int Id => 1;
        public string Name => "RMF FM";

        public async Task<List<PlaybackDraft>> GetPlaybacksAsync(DateTimeOffset playbacksTime)
        {
            try
            {
                var playbacksListSourceData = await GetPlaybacksListSourceDataAsync();

                var playbacksId = ConvertDataToPlaybacksId(playbacksListSourceData, playbacksTime);

                var playbacksDetailsSourceData = await GetPlaybacksDetailsSourceDataAsync(playbacksId);

                var playbacks = ConvertDataToPlaybacks(playbacksDetailsSourceData);

                _logger.LogInformation("Downloaded {playbacksCount} Playbacks for Radio {radioName}, time: {playbacksTime}", playbacks.Count, Name, playbacksTime);

                return playbacks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while downloading Playbacks for Radio {radioName}, time: {playbacksTime}", Name, playbacksTime);

                return new List<PlaybackDraft>();
            }
        }

        private async Task<string> GetPlaybacksListSourceDataAsync()
        {
            var sourceAdress = "https://live.rmf.fm/items-list.html";
            var sourceResponse = await _httpClient.GetAsync(sourceAdress);
            return await sourceResponse.Content.ReadAsStringAsync();
        }

        private static List<int> ConvertDataToPlaybacksId(string sourceData, DateTimeOffset playbacksTime)
        {
            var responseData = JsonSerializer.Deserialize<List<RmfFmListResponse>>(sourceData, _jsonSerializerOptions);

            if (responseData is not null)
            {
                var startTime = playbacksTime.ToUnixTimeMilliseconds();
                var endTime = playbacksTime.AddHours(1).ToUnixTimeMilliseconds();

                return responseData.Where(d => d.Category == "song" && d.Id.HasValue && d.Timestamp.HasValue && d.Timestamp >= startTime && d.Timestamp < endTime)
                    .Select(d => d.Id!.Value).ToList();
            }

            return new List<int>();
        }

        private async Task<string> GetPlaybacksDetailsSourceDataAsync(List<int> playbacksId)
        {
            var sourceAdress = string.Format("https://live.rmf.fm/items.html?ids={0}", string.Join("%2C", playbacksId));
            var sourceResponse = await _httpClient.GetAsync(sourceAdress);
            return await sourceResponse.Content.ReadAsStringAsync();
        }

        private static List<PlaybackDraft> ConvertDataToPlaybacks(string sourceData)
        {
            var responseData = JsonSerializer.Deserialize<Dictionary<int, RmfFmDetailsResponse>>(sourceData, _jsonSerializerOptions);

            if (responseData is not null)
            {
                return responseData.Where(d => !string.IsNullOrEmpty(d.Value.Title) && d.Value.Timestamp.HasValue).OrderBy(d => d.Value.Timestamp)
                    .Select(d => d.Value.ToPlayback()).ToList();
            }

            return new List<PlaybackDraft>();
        }

        private class RmfFmListResponse
        {
            [JsonPropertyName("ID")]
            public int? Id { get; set; }
            [JsonPropertyName("category")]
            public string? Category { get; set; }
            [JsonPropertyName("timestamp")]
            public long? Timestamp { get; set; }
        }

        private class RmfFmDetailsResponse
        {
            [JsonPropertyName("title")]
            public string? Title { get; set; }
            [JsonPropertyName("timestamp")]
            public long? Timestamp { get; set; }

            public PlaybackDraft ToPlayback()
            {
                var songTitleArtistSplit = Title!.Split(" - ", 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var songTitle = songTitleArtistSplit.Last();
                var artistName = songTitleArtistSplit.First();

                return new PlaybackDraft(songTitle, artistName, DateTimeOffset.FromUnixTimeMilliseconds(Timestamp!.Value).ToLocalTime());
            }
        }
    }
}