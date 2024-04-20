using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.DTO;
using LyricsAPI.Core.Models;
using LyricsAPI.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LyricsAPI.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SongsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var songs = await _mediator.Send(new GetAllSongLyricsRequest());

            return Ok(
                ResponseWrapper.Wrap(
                    "All Songs", songs
                        .Select(ls => new SongDTO(ls.Id, ls.Artist, ls.Title))));
        }

        [HttpGet("{artist}")]
        public async Task<ActionResult<IList<SongLyrics>>> GetSongLyricsByArtistAsync(string artist)
        {
            var songs = await _mediator.Send(new GetSongLyricsByArtistRequest(artist));

            return Ok(ResponseWrapper.Wrap(
                $"'{ artist }' Search Results", songs
                    .Select(ls => new SongDTO(ls.Id, ls.Artist, ls.Title))));
        }
    }
}
