namespace LyricsAPI.Core.Models
{
    public record WordFrequencyStatistics(
        string Artist, string Word, int SongsAmount, int AppearedInSongs, decimal FrequencyPercent);
}
