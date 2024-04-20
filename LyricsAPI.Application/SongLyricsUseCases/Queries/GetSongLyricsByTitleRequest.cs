using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Queries
{
    public record GetSongLyricsByTitleRequest(
        string Title) : IRequest<IList<SongLyrics>>;
}
