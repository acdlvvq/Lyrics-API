using CsvHelper;
using CsvHelper.Configuration.Attributes;
using LyricsAPI.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace LyricsAPI.Application
{
    public class Rec
    {
        [Index(0)]
        public string Track { get; set; } = string.Empty;
        [Index(1)]
        public string Artist { get; set; } = string.Empty;
        [Index(2)]
        public string RawLyrics { get; set; } = string.Empty;
        [Index(3)]
        public string ArtistVerses {  get; set; } = string.Empty;
    }

    public static class DbInitializer
    {
        public static async Task Initialize(
            IServiceProvider services)
        {
            var repository = services.GetRequiredService<ISongLyricsRepository>();

            using var reader = new StreamReader("C:/Users/ac1dl/Downloads/lyrics_raw.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<Rec>();
            foreach (var item in records)
            {
                await repository.AddAsync(new Core.Models.SongLyrics(item.Artist, item.Track, item.RawLyrics, item.ArtistVerses));
            }
            await repository.SaveChangesAsync();
        }
    }
}
