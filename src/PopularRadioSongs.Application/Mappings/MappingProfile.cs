﻿using AutoMapper;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsList;
using PopularRadioSongs.Application.UseCases.RadioPlaybacks.GetLastPlaybacks;
using PopularRadioSongs.Application.UseCases.Search.GetSearchResults;
using PopularRadioSongs.Application.UseCases.Songs.GetSongDetails;
using PopularRadioSongs.Application.UseCases.Songs.GetSongsList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Artist, ArtistListDto>();
            CreateMap<Artist, ArtistDetailsDto>();
            CreateMap<Song, SongArtistDetailsDto>();
            CreateMap<Artist, ArtistSongArtistDetailsDto>();

            CreateMap<Song, SongListDto>();
            CreateMap<Artist, ArtistSongListDto>();
            CreateMap<Song, SongDetailsDto>();
            CreateMap<Artist, ArtistSongDetailsDto>();
            CreateMap<Playback, PlaybackSongDetailsDto>();

            CreateMap<Playback, PlaybackLastPlaybacksDto>().ForCtorParam("Artists", x => x.MapFrom(y => y.Song.Artists));
            CreateMap<Artist, ArtistLastPlaybacksDto>();

            CreateMap<Song, SongSearchResultsDto>();
            CreateMap<Artist, ArtistSearchResultsDto>();
        }
    }
}