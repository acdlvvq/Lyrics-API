using LyricsAPI.Persistence.Configurations;
using LyricsAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace LyricsAPI.Persistence
{
    public class SongLyricsDbContext : DbContext
    {
        public SongLyricsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<SongLyricsEntity> SongLyrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SongLyricsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
