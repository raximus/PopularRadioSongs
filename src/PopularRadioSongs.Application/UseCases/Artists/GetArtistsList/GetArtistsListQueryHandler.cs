using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsList
{
    public class GetArtistsListQueryHandler : IRequestHandler<GetArtistsListQuery, PagedUseCaseResult<List<GroupArtistListDto>>>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public GetArtistsListQueryHandler(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<PagedUseCaseResult<List<GroupArtistListDto>>> Handle(GetArtistsListQuery request, CancellationToken cancellationToken)
        {
            (var artists, var artistsCount) = await _artistRepository.GetArtistsAsync(request.Page, request.PageSize);

            var artistsDto = _mapper.Map<List<ArtistListDto>>(artists);

            var artistsGroup = artistsDto.GroupBy(a => NameToLetter(a.Name)).Select(g => new GroupArtistListDto(g.Key, g.ToList())).ToList();

            return PagedUseCaseResult<List<GroupArtistListDto>>.Success(request.Page, request.PageSize, artistsCount, artistsGroup);
        }

        private static string NameToLetter(string name)
        {
            var letter = name.First();

            if (char.IsDigit(letter))
            {
                return "Number";
            }

            return letter.ToString();
        }
    }
}