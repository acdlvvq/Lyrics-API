namespace LyricsAPI.Presentation.Contracts
{
    public record AddSongLyricsRequest(
        string? Artist, string? Title, string? RawLyrics, string? ArtistVerses);
}
