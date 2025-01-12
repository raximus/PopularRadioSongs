using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistsList
{
    public class GetArtistsListQueryHandler : IRequestHandler<GetArtistsListQuery, UseCaseResult<List<GroupArtistListDto>>>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public GetArtistsListQueryHandler(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<UseCaseResult<List<GroupArtistListDto>>> Handle(GetArtistsListQuery request, CancellationToken cancellationToken)
        {
            var artists = await _artistRepository.GetArtistsAsync();

            var artistsDto = _mapper.Map<List<ArtistListDto>>(artists);

            return UseCaseResult<List<GroupArtistListDto>>.Success(artistsDto.GroupBy(a => NameToLetter(a.Name)).Select(g => new GroupArtistListDto(g.Key, g.ToList())).ToList());
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