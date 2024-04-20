namespace LyricsAPI.Core.Models
{
    public record ArtistStatistics(
        string Artist, int SongsAmount, 
        decimal AverageWordsInSongAmount, 
        IEnumerable<KeyValuePair<string, int>> WordsAmount);
}
