using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Models;
using LyricsAPI.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LyricsAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        public StatisticsController(
            IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        [HttpGet("{artist}")]
        public async Task<ActionResult> GetArtistStatisticsAsync(
            string artist)
        {
            if (!_cache.TryGetValue(artist, out var _songs))
            {
                _songs = await _mediator.Send(new GetSongLyricsByArtistRequest(artist));
                _cache.Set(artist, _songs);
            }

            if (_songs is null)
            {
                return NotFound(ResponseWrapper.Wrap("Error", "Artist Is Not Found"));
            }

            var songs = (List<SongLyrics>) _songs;

            if (songs.Count == 0)
            {
                return NotFound(ResponseWrapper.Wrap("Error", "Artist Is Not Found"));
            }

            return Ok(ResponseWrapper.Wrap(
                "Song Statistics", StatisticsProvider.GetStatistics(songs)));
        }

        [HttpGet("{artist}/{word}")]
        public async Task<ActionResult> GetWordAmountAsync(
            string artist, string word)
        {
            if (!_cache.TryGetValue(artist, out var _songs))
            {
                _songs = await _mediator.Send(new GetSongLyricsByArtistRequest(artist));
                _cache.Set(artist, _songs);
            }

            if (_songs is null)
            {
                return NotFound(ResponseWrapper.Wrap("Error", "Artist Is Not Found"));
            }

            var songs = (List<SongLyrics>)_songs;

            if (songs.Count == 0)
            {
                return NotFound(ResponseWrapper.Wrap("Error", "Artist Is Not Found"));
            }

            return Ok(ResponseWrapper.Wrap(
                "Song Statistics", StatisticsProvider.GetWordStatistics(songs, word)));
        }
    }
}
