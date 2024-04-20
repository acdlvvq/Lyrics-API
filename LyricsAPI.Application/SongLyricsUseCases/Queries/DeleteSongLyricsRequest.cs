using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Queries
{
    public record DeleteSongLyricsRequest(
        string Id) : IRequest<bool>;
}
