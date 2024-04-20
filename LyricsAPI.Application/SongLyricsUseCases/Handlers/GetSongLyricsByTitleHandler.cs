using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Handlers
{
    public class GetSongLyricsByTitleHandler
        : IRequestHandler<GetSongLyricsByTitleRequest, IList<SongLyrics>>
    {
        private readonly ISongLyricsRepository _repository;

        public GetSongLyricsByTitleHandler(
            ISongLyricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<SongLyrics>> Handle(
            GetSongLyricsByTitleRequest request, CancellationToken cancellationToken)
        {
            var songs = await _repository.GetByTitleAsync(request.Title);

            return songs;
        }
    }
}
