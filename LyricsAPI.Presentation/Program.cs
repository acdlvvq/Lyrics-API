using FluentValidation;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.Models;
using LyricsAPI.Core.Validators;
using LyricsAPI.Persistence;
using LyricsAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using LyricsAPI.Application;

namespace LyricsAPI.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SongLyricsDbContext>(
                options => options.UseNpgsql(
                    builder.Configuration.GetConnectionString(nameof(SongLyricsDbContext))));

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IValidator<SongLyrics>, SongLyricsValidator>();
            builder.Services.AddScoped<ISongLyricsRepository, SongLyricsRepository>();

            builder.Services.AddApplication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
