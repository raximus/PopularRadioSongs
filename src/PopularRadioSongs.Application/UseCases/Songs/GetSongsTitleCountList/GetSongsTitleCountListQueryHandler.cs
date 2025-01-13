using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList
{
    public class GetSongsTitleCountListQueryHandler : IRequestHandler<GetSongsTitleCountListQuery, PagedUseCaseResult<List<SongTitleCountListDto>>>
    {
        private readonly ISongRepository _songRepository;

        public GetSongsTitleCountListQueryHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<PagedUseCaseResult<List<SongTitleCountListDto>>> Handle(GetSongsTitleCountListQuery request, CancellationToken cancellationToken)
        {
            (var songs, var songsCount) = await _songRepository.GetSongTitleCountListAsync(request.Page, request.PageSize);

            return PagedUseCaseResult<List<SongTitleCountListDto>>.Success(request.Page, request.PageSize, songsCount, songs);
        }
    }
}