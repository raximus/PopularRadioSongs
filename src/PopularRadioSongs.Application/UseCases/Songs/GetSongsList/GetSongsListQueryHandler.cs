using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsList
{
    public class GetSongsListQueryHandler : IRequestHandler<GetSongsListQuery, PagedUseCaseResult<List<GroupSongListDto>>>
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public GetSongsListQueryHandler(ISongRepository songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task<PagedUseCaseResult<List<GroupSongListDto>>> Handle(GetSongsListQuery request, CancellationToken cancellationToken)
        {
            (var songs, var songsCount) = await _songRepository.GetSongsAsync(request.Page, request.PageSize);

            var songsDto = _mapper.Map<List<SongListDto>>(songs);

            var songsGroup = songsDto.GroupBy(a => TitleToLetter(a.Title)).Select(g => new GroupSongListDto(g.Key, g.ToList())).ToList();

            return PagedUseCaseResult<List<GroupSongListDto>>.Success(request.Page, request.PageSize, songsCount, songsGroup);
        }

        private static string TitleToLetter(string title)
        {
            var letter = title.First();

            if (char.IsDigit(letter))
            {
                return "Number";
            }

            return letter.ToString();
        }
    }
}