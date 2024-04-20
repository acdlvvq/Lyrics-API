using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Models;
using LyricsAPI.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LyricsAPI.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LyricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LyricsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSongLyricsByIdAsync(string id)
        {
            if (id.Length != SongLyrics.IdLength)
            {
                return BadRequest(ResponseWrapper.Wrap(
                    "Error", $"Id Must Be { SongLyrics.IdLength } Characters Length"));
            }

            var song = await _mediator.Send(new GetSongLyricsByIdRequest(id));

            if (song is null) 
            {
                return NotFound(ResponseWrapper.Wrap(
                    "Error", "Song Is Not Found"));
            }

            return Ok(ResponseWrapper.Wrap("Song Lyrics", song));
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
                return BadRequest(ResponseWrapper.Wrap(
                    "Error", "Parameters Must Not Be Null"));
            }

            var addedSong = await _mediator.Send(
                new AddSongLyricsRequest(
                    request.Artist, request.Title, request.RawLyrics, request.ArtistVerses));

            if (addedSong is null) 
            {
                return BadRequest(ResponseWrapper.Wrap(
                    "Error", "Parameters Are Not Valid"));
            }

            var uri = new StringBuilder()
                .Append(Request.Host.Value)
                .Append('/')
                .Append("Lyrics")
                .Append('/')
                .Append(addedSong.Id)
                .ToString();
            return Created(uri, ResponseWrapper.Wrap("Added Song", addedSong));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSongLyricsAsync(string id)
        {
            var result = await _mediator.Send(new DeleteSongLyricsRequest(id));

            return result ? NoContent() : BadRequest(ResponseWrapper.Wrap("Error", "Song Is Not Found"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSongLyricsAsync(
            string id, UpdateSongLyricsRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.RawLyrics) ||
                string.IsNullOrWhiteSpace(request.ArtistVerses))
            {
                return BadRequest(ResponseWrapper.Wrap(
                    "Error", "Parameters Must Not Be Null"));
            }

            var result = await _mediator.Send(
                new UpdateSongLyricsRequest(id, request.RawLyrics, request.ArtistVerses));

            return result ? NoContent() : BadRequest(ResponseWrapper.Wrap("Error", "Song Is Not Found"));    
        }

        [HttpGet("{id}/{other}")]
        public async Task<ActionResult> GetSongMathcesAsync(
            string id, string other)
        {
            var song = await _mediator.Send(new GetSongLyricsByIdRequest(id));

            if (song is null)
            {
                return BadRequest(ResponseWrapper.Wrap("Error", "Wrong Song Id"));
            }

            var otherSong = await _mediator.Send(new GetSongLyricsByIdRequest(other));

            if (otherSong is null) 
            {
                return BadRequest(ResponseWrapper.Wrap("Error", "Wrong Second Song Id"));
            }

            return Ok(ResponseWrapper.Wrap(
                "Song Word Matches List", StatisticsProvider.GetSongMatches(song, otherSong)));
        }
    }
}
