using LyricsAPI.Core.Models;

namespace LyricsAPI.Core.Abstractions
{
    public interface IStatisticsProvider
    {
        IEnumerable<string> GetSongMatches(SongLyrics song, SongLyrics other);
        ArtistStatistics GetStatistics(IList<SongLyrics> songs);
        IEnumerable<string> GetWords(SongLyrics song);
        WordFrequencyStatistics GetWordStatistics(IList<SongLyrics> songs, string wordToSearch);
    }
}