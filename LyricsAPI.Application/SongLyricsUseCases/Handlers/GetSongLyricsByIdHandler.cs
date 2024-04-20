using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Handlers
{
    public class GetSongLyricsByIdHandler
        : IRequestHandler<GetSongLyricsByIdRequest, SongLyrics?>
    {
        private readonly ISongLyricsRepository _repository;

        public GetSongLyricsByIdHandler(
            ISongLyricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<SongLyrics?> Handle(GetSongLyricsByIdRequest request, CancellationToken cancellationToken)
        {
            var song = await _repository.GetByIdAsync(request.Id);

            return song;
        }
    }
}
