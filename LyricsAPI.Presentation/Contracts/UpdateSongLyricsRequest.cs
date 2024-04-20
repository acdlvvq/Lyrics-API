namespace LyricsAPI.Presentation.Contracts
{
    public record UpdateSongLyricsRequest(
        string? RawLyrics, string? ArtistVerses);
}
