using LyricsAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LyricsAPI.Persistence.Configurations
{
    public class SongLyricsConfiguration : IEntityTypeConfiguration<SongLyricsEntity>
    {
        public void Configure(EntityTypeBuilder<SongLyricsEntity> builder)
        {
            builder
                .HasKey(ls => ls.Id);
        }
    }
}
