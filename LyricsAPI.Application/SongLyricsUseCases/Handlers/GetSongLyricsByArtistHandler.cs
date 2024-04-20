using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Handlers
{
    public class GetSongLyricsByArtistHandler
        : IRequestHandler<GetSongLyricsByArtistRequest, IList<SongLyrics>>
    {
        private readonly ISongLyricsRepository _repository;

        public GetSongLyricsByArtistHandler(
            ISongLyricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<SongLyrics>> Handle(
            GetSongLyricsByArtistRequest request, CancellationToken cancellationToken)
        {
           var songs = await _repository.GetByArtistAsync(request.Artist);

            return songs;
                
        }
    }
}
