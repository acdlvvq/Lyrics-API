using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Queries
{
    public record UpdateSongLyricsRequest(
        string Id, string RawLyrics, string ArtistVerses) : IRequest<bool>;
}
