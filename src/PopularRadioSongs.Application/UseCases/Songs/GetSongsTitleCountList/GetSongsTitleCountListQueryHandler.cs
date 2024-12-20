using MediatR;
using PopularRadioSongs.Application.Contracts;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsTitleCountList
{
    public class GetSongsTitleCountListQueryHandler : IRequestHandler<GetSongsTitleCountListQuery, List<SongTitleCountListDto>>
    {
        private readonly ISongRepository _songRepository;

        public GetSongsTitleCountListQueryHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<List<SongTitleCountListDto>> Handle(GetSongsTitleCountListQuery request, CancellationToken cancellationToken)
        {
            var songs = await _songRepository.GetSongTitleCountListAsync();

            return songs;
        }
    }
}