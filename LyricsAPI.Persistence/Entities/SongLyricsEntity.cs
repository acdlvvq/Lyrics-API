using LyricsAPI.Core.Models;
using System.Security.Cryptography;
using System.Text;

namespace LyricsAPI.Persistence.Entities
{
    public class SongLyricsEntity : Entity
    {
        public string TrackName { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string RawLyrics { get; set; } = string.Empty;
        public string ArtistVerses { get; set; } = string.Empty;  

        public static SongLyricsEntity Create(SongLyrics songLyrics)
            => new SongLyricsEntity()
                { 
                    Id = songLyrics.Id,
                    TrackName = songLyrics.Title,
                    Artist = songLyrics.Artist,
                    RawLyrics = songLyrics.RawLyrics,
                    ArtistVerses = songLyrics.ArtistVerses,
                };
    }
}
