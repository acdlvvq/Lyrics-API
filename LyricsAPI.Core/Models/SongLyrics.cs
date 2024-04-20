using System.Security.Cryptography;
using System.Text;

namespace LyricsAPI.Core.Models
{
    public class SongLyrics
    {
        public SongLyrics(string artist, string title, string rawLyrics, string artistVerses)
        {
            var fullName = new StringBuilder()
                .Append(artist)
                .Append(title)
                .ToString();

            Id = BitConverter.ToString(
                SHA256.HashData(Encoding.UTF8.GetBytes(fullName))).Replace("-", "");
            Artist = artist;
            Title = title;
            RawLyrics = rawLyrics;
            ArtistVerses = artistVerses;
        }

        public SongLyrics(string id, string artist, string title, string rawLyrics, string artistVerses)
        {
            Id = id;
            Artist = artist;
            Title = title;
            RawLyrics = rawLyrics;
            ArtistVerses = artistVerses;
        }

        public string Id { get; private set; } = string.Empty;
        public string Artist { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public string RawLyrics { get; private set; } = string.Empty;
        public string ArtistVerses { get; private set; } = string.Empty;
    }
}
