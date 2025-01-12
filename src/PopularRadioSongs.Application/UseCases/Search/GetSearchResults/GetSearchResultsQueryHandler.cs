using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;
using PopularRadioSongs.Core.Common;

namespace PopularRadioSongs.Application.UseCases.Search.GetSearchResults
{
    public class GetSearchResultsQueryHandler : IRequestHandler<GetSearchResultsQuery, UseCaseResult<SearchResultsDto>>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public GetSearchResultsQueryHandler(IArtistRepository artistRepository, ISongRepository songRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task<UseCaseResult<SearchResultsDto>> Handle(GetSearchResultsQuery request, CancellationToken cancellationToken)
        {
            var searchLookup = StringsHelper.LookupString(StringsHelper.StandardizeString(request.SearchValue));

            var artists = await _artistRepository.GetArtistsBySearchAsync(searchLookup);
            var artistsDto = _mapper.Map<List<ArtistSearchResultsDto>>(artists);

            var songs = await _songRepository.GetSongsBySearchAsync(searchLookup);
            var songsDto = _mapper.Map<List<SongSearchResultsDto>>(songs);

            var searchResults = artistsDto.Select(a => new ResultSearchResultsDto(null, a)).ToList();
            searchResults.AddRange(songsDto.Select(s => new ResultSearchResultsDto(s, null)));

            return UseCaseResult<SearchResultsDto>.Success(new SearchResultsDto(request.SearchValue, searchResults));
        }
    }
}