using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Queries
{
    public record AddSongLyricsRequest(
        string Artist, string Title,
        string RawLyrics, string ArtistVerses) : IRequest<SongLyrics?>;
}
