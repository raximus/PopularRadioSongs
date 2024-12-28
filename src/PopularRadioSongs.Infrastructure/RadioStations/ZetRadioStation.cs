﻿using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Core.Common;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PopularRadioSongs.Infrastructure.RadioStations
{
    public class ZetRadioStation : IRadioStation
    {
        private readonly HttpClient _httpClient;

        public ZetRadioStation(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public int Id => 2;
        public string Name => "ZET";

        public async Task<List<PlaybackDraft>> GetPlaybacksAsync(DateTimeOffset playbacksTime)
        {
            var playbacksSourceData = await GetPlaybacksSourceDataAsync(playbacksTime);

            var playbacks = ConvertDataToPlaybacks(playbacksSourceData, playbacksTime);

            return playbacks;
        }

        private async Task<string> GetPlaybacksSourceDataAsync(DateTimeOffset playbacksTime)
        {
            var startTime = playbacksTime.ToUnixTimeSeconds();
            var sourceAdress = string.Format("https://rds.eurozet.pl/reader/history.php?startDate={0}&emitter=radiozet&trackCount=20", startTime);
            var sourceResponse = await _httpClient.GetAsync(sourceAdress);
            return await sourceResponse.Content.ReadAsStringAsync();
        }

        private List<PlaybackDraft> ConvertDataToPlaybacks(string sourceData, DateTimeOffset playbacksTime)
        {
            sourceData = sourceData.Substring(9, sourceData.Length - 10);
            var responseData = JsonSerializer.Deserialize<ZetResponse>(sourceData);

            if (responseData is not null && responseData.Messages is not null)
            {
                var endTime = playbacksTime.AddHours(1);

                return responseData.Messages.Single().Where(d => !string.IsNullOrEmpty(d.Title) && !string.IsNullOrEmpty(d.Artist) && !string.IsNullOrEmpty(d.Start))
                    .Select(d => d.ToPlayback()).Where(p => p.PlayTime < endTime).ToList();
            }

            return new List<PlaybackDraft>();
        }

        private class ZetResponse
        {
            [JsonPropertyName("messages")]
            public List<List<ZetItem>>? Messages { get; set; }
        }

        private class ZetItem
        {
            [JsonPropertyName("rds_title")]
            public string? Title { get; set; }
            [JsonPropertyName("rds_artist")]
            public string? Artist { get; set; }
            [JsonPropertyName("rds_start")]
            public string? Start { get; set; }

            public PlaybackDraft ToPlayback()
            {
                return new PlaybackDraft(Title!, Artist!, DateTimeOffset.Parse(Start!));
            }
        }
    }
}