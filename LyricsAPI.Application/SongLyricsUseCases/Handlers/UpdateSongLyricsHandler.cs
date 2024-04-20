using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Handlers
{
    public class UpdateSongLyricsHandler
        : IRequestHandler<UpdateSongLyricsRequest, bool>
    {
        private readonly ISongLyricsRepository _repository;

        public UpdateSongLyricsHandler(
            ISongLyricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateSongLyricsRequest request, CancellationToken cancellationToken)
        {
            var updated = await _repository
                .UpdateAsync(request.Id, request.RawLyrics, request.ArtistVerses);

            if (updated)
            {
                await _repository.SaveChangesAsync();
            }

            return updated;
        }
    }
}
