using LyricsAPI.Core.Models;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace LyricsAPI.Infrastructure
{
    public static partial class StatisticsProvider
    {
        public static IEnumerable<string> GetWords(SongLyrics song)
        {
            var lyrics = song.RawLyrics;
            lyrics = BetweenSquareBrackets().Replace(lyrics, "");
            lyrics = NonLetterOrNumber().Replace(lyrics, "");

            return lyrics
                .Split(" ")
                .Where(word => !string.IsNullOrEmpty(word))
                .Select(word => word.ToLower());
        }
         
        public static ArtistStatistics GetStatistics(IList<SongLyrics> songs)
        {
            var artist = songs.First().Artist;
            var songsAmount = songs.Count;
            var wordsAmount = new ConcurrentDictionary<string, int>();
            var wordsCount = 0;

            songs.AsParallel().ForAll(song =>
            {
                var words = GetWords(song);
                wordsCount += words.Count();

                foreach (var word in words)
                {
                    if (!wordsAmount.TryAdd(word, 1))
                    {
                        wordsAmount[word] += 1;
                    }
                }
            });

            var averageWords = decimal.Divide(wordsCount, songsAmount);
            return new(artist, songsAmount, averageWords, wordsAmount.OrderBy(item => item.Value).Reverse());
        }

        public static WordFrequencyStatistics GetWordStatistics(IList<SongLyrics> songs, string wordToSearch)
        {
            var artist = songs.First().Artist;
            var wordAmount = 0;
            var wordsCount = 0;
            var appeared = 0;
            wordToSearch = wordToSearch.ToLower();

            songs.AsParallel().ForAll(song =>
            {
                var words = GetWords(song);
                wordsCount += words.Count();

                if (words.Contains(wordToSearch))
                {
                    ++appeared;
                    wordAmount += words.Where(word => word == wordToSearch).Count();
                }
            });

            var frequency = decimal.Multiply(decimal.Divide(wordAmount, wordsCount), Convert.ToDecimal(100));
            
            return new(artist, wordToSearch, songs.Count, appeared, frequency);
        }

        public static IEnumerable<string> GetSongMatches(
            SongLyrics song, SongLyrics other)
        {
            var lyrics = GetWords(song);
            var otherLyrics = GetWords(other);

            return lyrics.Where(otherLyrics.Contains);
        }

        [GeneratedRegex("[^a-zA-Z0-9 -]")]
        private static partial Regex NonLetterOrNumber();

        [GeneratedRegex("\\[.*?\\]")]
        private static partial Regex BetweenSquareBrackets();
    }
}
