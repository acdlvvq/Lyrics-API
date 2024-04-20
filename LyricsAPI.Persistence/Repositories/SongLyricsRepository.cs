using FluentValidation;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.Models;
using LyricsAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LyricsAPI.Persistence.Repositories
{
    public class SongLyricsRepository : ISongLyricsRepository
    {
        private readonly SongLyricsDbContext _context;
        private readonly IValidator<SongLyrics> _validator;

        public SongLyricsRepository(
            SongLyricsDbContext context, IValidator<SongLyrics> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<SongLyrics?> AddAsync(SongLyrics songLyrics)
        {
            var validate = _validator.Validate(songLyrics);

            if (validate.IsValid)
            {
                await _context.SongLyrics
                    .AddAsync(SongLyricsEntity.Create(songLyrics));

                return songLyrics;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleted = await _context.SongLyrics
                .Where(ls => ls.Id == id)
                .ExecuteDeleteAsync();

            return deleted != 0;
        }

        public async Task<IList<SongLyrics>> GetAsync()
        {
            var songs = await _context.SongLyrics
                .AsNoTracking()
                .Select(ls => new SongLyrics(
                    ls.Id, ls.Artist, ls.TrackName, ls.RawLyrics, ls.ArtistVerses))
                .ToListAsync();

            return songs;
        }

        public async Task<IList<SongLyrics>> GetByArtistAsync(string artist)
        {
            var songs = await _context.SongLyrics
                .AsNoTracking()
                .Where(ls => ls.Artist.ToLower().Contains(artist.ToLower()) ||
                             artist.ToLower().Contains(ls.Artist.ToLower()))
                .Select(ls => new SongLyrics(
                    ls.Id, ls.Artist, ls.TrackName, ls.RawLyrics, ls.ArtistVerses))
                .ToListAsync();
            
            return songs ?? [];
        }

        public async Task<SongLyrics?> GetByIdAsync(string id)
        {
            var song = await _context.SongLyrics
                .AsNoTracking()
                .Where(ls => ls.Id == id)
                .Select(ls => new SongLyrics(
                    ls.Id, ls.Artist, ls.TrackName, ls.RawLyrics, ls.ArtistVerses))
                .FirstOrDefaultAsync();

            return song;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(string id, string rawLyrics, string artistVerses)
        {
            var updated = await _context.SongLyrics
                .Where(ls => ls.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(ls => ls.RawLyrics, rawLyrics)
                    .SetProperty(ls => ls.ArtistVerses, artistVerses));

            return updated != 0;
        }
    }
}
