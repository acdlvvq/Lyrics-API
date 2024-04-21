using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.DTO;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Handlers
{
    public class GetSongDTOByArtistHandler
        : IRequestHandler<GetSongDTOByArtistRequest, IList<SongDTO>>
    {
        private readonly ISongLyricsRepository _repository;

        public GetSongDTOByArtistHandler(
            ISongLyricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<SongDTO>> Handle(
            GetSongDTOByArtistRequest request, CancellationToken cancellationToken)
        {
            var songs = await _repository.GetDTOByArtistAsync(request.Artist);

            return songs;
        }
    }
}
