using LyricsAPI.Core.Models;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Queries
{
    public record GetSongLyricsByIdRequest(
        string Id) : IRequest<SongLyrics?>;
}
