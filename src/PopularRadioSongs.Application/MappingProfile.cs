using AutoMapper;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails;
using PopularRadioSongs.Application.UseCases.Artists.GetArtistsList;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Artist, ArtistListDto>();
            CreateMap<Artist, ArtistDetailsDto>();
            CreateMap<Song, SongArtistDetailsDto>();
            CreateMap<Artist, ArtistSongArtistDetailsDto>();
        }
    }
}