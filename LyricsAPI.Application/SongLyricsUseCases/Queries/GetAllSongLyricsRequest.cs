using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Queries
{
    public record GetAllSongLyricsRequest() : IRequest<IList<SongLyrics>>;
}
