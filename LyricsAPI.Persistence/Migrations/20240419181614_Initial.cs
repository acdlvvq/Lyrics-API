using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LyricsAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SongLyrics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TrackName = table.Column<string>(type: "text", nullable: false),
                    Artist = table.Column<string>(type: "text", nullable: false),
                    RawLyrics = table.Column<string>(type: "text", nullable: false),
                    ArtistVerses = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongLyrics", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongLyrics");
        }
    }
}
