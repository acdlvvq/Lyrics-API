using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Handlers
{
    public class GetAllSongLyricsHandler 
        : IRequestHandler<GetAllSongLyricsRequest, IList<SongLyrics>>
    {
        private readonly ISongLyricsRepository _repository;

        public GetAllSongLyricsHandler(ISongLyricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<SongLyrics>> Handle(
            GetAllSongLyricsRequest request, CancellationToken cancellationToken)
        {
            var songs = await _repository.GetAsync();

            return songs;
        }
    }
}
