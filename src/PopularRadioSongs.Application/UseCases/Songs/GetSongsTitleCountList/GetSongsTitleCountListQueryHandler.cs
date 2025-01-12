using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList
{
    public class GetSongsTitleCountListQueryHandler : IRequestHandler<GetSongsTitleCountListQuery, UseCaseResult<List<SongTitleCountListDto>>>
    {
        private readonly ISongRepository _songRepository;

        public GetSongsTitleCountListQueryHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<UseCaseResult<List<SongTitleCountListDto>>> Handle(GetSongsTitleCountListQuery request, CancellationToken cancellationToken)
        {
            var songs = await _songRepository.GetSongTitleCountListAsync();

            return UseCaseResult<List<SongTitleCountListDto>>.Success(songs);
        }
    }
}