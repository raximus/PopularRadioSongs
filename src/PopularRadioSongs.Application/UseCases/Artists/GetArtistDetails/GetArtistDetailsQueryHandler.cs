using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Artists.GetArtistDetails
{
    public class GetArtistDetailsQueryHandler : IRequestHandler<GetArtistDetailsQuery, UseCaseResult<ArtistDetailsDto>>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public GetArtistDetailsQueryHandler(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<UseCaseResult<ArtistDetailsDto>> Handle(GetArtistDetailsQuery request, CancellationToken cancellationToken)
        {
            var artist = await _artistRepository.GetArtistWithSongsByIdAsync(request.ArtistId);

            if (artist is null)
            {
                return UseCaseResult<ArtistDetailsDto>.NotFound("Artist not found");
            }

            return UseCaseResult<ArtistDetailsDto>.Success(_mapper.Map<ArtistDetailsDto>(artist));
        }
    }
}