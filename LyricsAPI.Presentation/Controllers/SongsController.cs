using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LyricsAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponseWrapper _wrapper;

        public SongsController(
            IMediator mediator, IResponseWrapper wrapper)
        {
            _mediator = mediator;
            _wrapper = wrapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var songs = await _mediator.Send(new GetAllSongLyricsRequest());

            return Ok(
                _wrapper.Wrap(
                    "All Songs", songs
                        .Select(ls => new SongDTO(ls.Id, ls.Artist, ls.Title))));
        }

        [HttpGet("{artist}")]
        public async Task<ActionResult> GetSongLyricsByArtistAsync(string artist)
        {
            var songs = await _mediator.Send(new GetSongDTOByArtistRequest(artist));

            return Ok(_wrapper.Wrap(
                $"'{ artist }' Search Results", songs));
        }
    }
}
