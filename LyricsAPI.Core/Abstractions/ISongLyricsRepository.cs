using LyricsAPI.Core.Models;

namespace LyricsAPI.Core.Abstractions
{
    public interface ISongLyricsRepository
    {
        public Task<IList<SongLyrics>> GetAsync();
        public Task<SongLyrics?> GetByIdAsync(string id);
        public Task<IList<SongLyrics>> GetByArtistAsync(string artist);
        public Task<IList<SongLyrics>> GetByTitleAsync(string title);
        public Task<SongLyrics?> AddAsync(SongLyrics songLyrics);
        public Task<bool> UpdateAsync(string id, string rawLyrics, string artistVerses);
        public Task<bool> DeleteAsync(string id);
        public Task SaveChangesAsync();
    }
}
