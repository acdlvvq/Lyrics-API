using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace LyricsAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LyricsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private readonly IResponseWrapper _wrapper;
        private readonly IStatisticsProvider _statisticsProvider;

        public LyricsController(
            IMediator mediator, IMemoryCache cache, 
            IResponseWrapper wrapper, IStatisticsProvider statisticsProvider)
        {
            _mediator = mediator;
            _cache = cache;
            _wrapper = wrapper;
            _statisticsProvider = statisticsProvider;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSongLyricsByIdAsync(string id)
        {
            if (id.Length != SongLyrics.IdLength)
            {
                return BadRequest(_wrapper.Wrap(
                    "Error", $"Id Must Be { SongLyrics.IdLength } Characters Length"));
            }

            if (!_cache.TryGetValue(id, out var song))
            {
                song = await _mediator.Send(new GetSongLyricsByIdRequest(id));
                _cache.Set(
                    id, song, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
            }

            if (song is null) 
            {
                return NotFound(_wrapper.Wrap(
                    "Error", "Song Is Not Found"));
            }

            return Ok(_wrapper.Wrap("Song Lyrics", (SongLyrics)song));
        }

        [HttpPost]
        public async Task<ActionResult> AddSongLyricsAsync(
            AddSongLyricsRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Artist) ||
                string.IsNullOrWhiteSpace(request.Title) ||
                string.IsNullOrWhiteSpace(request.RawLyrics) ||
                string.IsNullOrWhiteSpace(request.ArtistVerses))
            {
                return BadRequest(_wrapper.Wrap(
                    "Error", "Parameters Must Not Be Null"));
            }

            var addedSong = await _mediator.Send(
                new AddSongLyricsRequest(
                    request.Artist, request.Title, request.RawLyrics, request.ArtistVerses));

            if (addedSong is null) 
            {
                return BadRequest(_wrapper.Wrap(
                    "Error", "Parameters Are Not Valid"));
            }

            var uri = new StringBuilder()
                .Append(Request.Host.Value)
                .Append('/')
                .Append("Lyrics")
                .Append('/')
                .Append(addedSong.Id)
                .ToString();
            return Created(uri, _wrapper.Wrap("Added Song", addedSong));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSongLyricsAsync(string id)
        {
            var result = await _mediator.Send(new DeleteSongLyricsRequest(id));

            if (result && _cache.TryGetValue(id, out var _))
            {
                _cache.Remove(id);
            }

            return result ? NoContent() : BadRequest(_wrapper.Wrap("Error", "Song Is Not Found"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSongLyricsAsync(
            string id, UpdateSongLyricsRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RawLyrics) ||
                string.IsNullOrWhiteSpace(request.ArtistVerses))
            {
                return BadRequest(_wrapper.Wrap(
                    "Error", "Parameters Must Not Be Null"));
            }

            var result = await _mediator.Send(
                new UpdateSongLyricsRequest(id, request.RawLyrics, request.ArtistVerses));

            if (result && _cache.TryGetValue(id, out var _))
            {
                _cache.Remove(id);
            }

            return result ? NoContent() : BadRequest(_wrapper.Wrap("Error", "Song Is Not Found"));    
        }

        [HttpGet("{id}/{other}")]
        public async Task<ActionResult> GetSongMathcesAsync(
            string id, string other)
        {
            if (!_cache.TryGetValue(id, out var song))
            {
                song = await _mediator.Send(new GetSongLyricsByIdRequest(id));
                _cache.Set(
                    id, song, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
            }

            if (song is null)
            {
                return BadRequest(_wrapper.Wrap("Error", "Wrong Song Id"));
            }

            if (!_cache.TryGetValue(other, out var otherSong))
            {
                otherSong = await _mediator.Send(new GetSongLyricsByIdRequest(other));
                _cache.Set(
                    other, otherSong, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
            }

            if (otherSong is null) 
            {
                return BadRequest(_wrapper.Wrap("Error", "Wrong Second Song Id"));
            }

            return Ok(_wrapper.Wrap(
                "Song Word Matches List", _statisticsProvider.GetSongMatches((SongLyrics)song, (SongLyrics)otherSong)));
        }
    }
}
