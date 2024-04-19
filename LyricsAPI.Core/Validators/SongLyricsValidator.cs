using FluentValidation;
using LyricsAPI.Core.Models;

namespace LyricsAPI.Core.Validators
{
    public class SongLyricsValidator : AbstractValidator<SongLyrics>
    {
        public SongLyricsValidator()
        {
            RuleFor(ls => ls.Artist)
                .NotEmpty()
                .Length(2, 30);

            RuleFor(ls => ls.Title)
                .NotEmpty()
                .Length(2, 50);

            RuleFor(ls => ls.RawLyrics)
                .NotEmpty();

            RuleFor(ls => ls.ArtistVerses)
                .NotEmpty();
        }
    }
}
