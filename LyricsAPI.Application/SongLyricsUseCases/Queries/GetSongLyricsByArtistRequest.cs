using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Queries
{
    public record GetSongLyricsByArtistRequest(
        string Artist) : IRequest<IList<SongLyrics>>;
}
