using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Handlers
{
    public class DeleteSongLyricsHandler
        : IRequestHandler<DeleteSongLyricsRequest, bool>
    {
        private readonly ISongLyricsRepository _repository;

        public DeleteSongLyricsHandler(
            ISongLyricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            DeleteSongLyricsRequest request, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(request.Id);
            
            if (deleted)
            {
                await _repository.SaveChangesAsync();
            }

            return deleted;
        }
    }
}
