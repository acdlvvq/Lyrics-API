namespace LyricsAPI.Core.Models
{
    public class SongLyrics(
        string artist, string title, string rawLyrics, string artistVerses)
    {
        public string Artist { get; private set; } = artist;
        public string Title { get; private set; } = title;
        public string RawLyrics { get; private set; } = rawLyrics;
        public string ArtistVerses { get; private set; } = artistVerses;
    }
}
