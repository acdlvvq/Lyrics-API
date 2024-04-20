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
        {
            var fullName = new StringBuilder()
                .Append(songLyrics.Artist)
                .Append(songLyrics.Title)
                .ToString();

            return new SongLyricsEntity()
            { 
                TrackName = songLyrics.Title,
                Artist = songLyrics.Artist,
                RawLyrics = songLyrics.RawLyrics,
                ArtistVerses = songLyrics.ArtistVerses,
                Id = BitConverter.ToString(SHA256.HashData(Encoding.UTF8.GetBytes(fullName))).Replace("-", "")
            };
        }
    }
}
