using LyricsAPI.Core.DTO;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Queries
{
    public record GetSongDTOByArtistRequest(
        string Artist) : IRequest<IList<SongDTO>>;
}
