using AutoMapper;
using MediatR;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Results;

namespace PopularRadioSongs.Application.UseCases.Songs.GetSongsList
{
    public class GetSongsListQueryHandler : IRequestHandler<GetSongsListQuery, UseCaseResult<List<GroupSongListDto>>>
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public GetSongsListQueryHandler(ISongRepository songRepository, IMapper mapper)
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task<UseCaseResult<List<GroupSongListDto>>> Handle(GetSongsListQuery request, CancellationToken cancellationToken)
        {
            var songs = await _songRepository.GetSongsAsync();

            var songsDto = _mapper.Map<List<SongListDto>>(songs);

            return UseCaseResult<List<GroupSongListDto>>.Success(songsDto.GroupBy(a => TitleToLetter(a.Title)).Select(g => new GroupSongListDto(g.Key, g.ToList())).ToList());
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