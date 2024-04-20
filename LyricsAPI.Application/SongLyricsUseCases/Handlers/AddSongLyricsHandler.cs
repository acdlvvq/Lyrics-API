using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Handlers
{
    public class AddSongLyricsHandler
        : IRequestHandler<AddSongLyricsRequest, SongLyrics?>
    {
        private readonly ISongLyricsRepository _repository;

        public AddSongLyricsHandler(
            ISongLyricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<SongLyrics?> Handle(
            AddSongLyricsRequest request, CancellationToken cancellationToken)
        {
            var song = new SongLyrics(
                request.Artist, request.Title, request.RawLyrics, request.ArtistVerses);

            var addedSong = await _repository.AddAsync(song);

            if (addedSong is not null) 
            {
                await _repository.SaveChangesAsync();
            }

            return addedSong;
        }
    }
}
