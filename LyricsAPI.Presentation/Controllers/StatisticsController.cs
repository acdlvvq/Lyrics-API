using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LyricsAPI.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{artist}")]
        public async Task<ActionResult> GetArtistStatisticsAsync(
            string artist)
        {
            var songs = await _mediator.Send(new GetSongLyricsByArtistRequest(artist));

            if (songs is null)
            {
                return NotFound(ResponseWrapper.Wrap("Error", "Song Is Not Found"));
            }

            return Ok(ResponseWrapper.Wrap(
                "Song Statistics", StatisticsProvider.GetStatistics(songs)));
        }

        [HttpGet("{artist}/{word}")]
        public async Task<ActionResult> GetWordAmountAsync(
            string artist, string word)
        {
            var songs = await _mediator.Send(new GetSongLyricsByArtistRequest(artist));

            if (songs is null)
            {
                return NotFound(ResponseWrapper.Wrap("Error", "Song Is Not Found"));
            }

            return Ok(ResponseWrapper.Wrap(
                "Song Statistics", StatisticsProvider.GetWordStatistics(songs, word)));
        }
    }
}
