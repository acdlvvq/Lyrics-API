﻿using LyricsAPI.Application.SongLyricsUseCases.Queries;
using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.DTO;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Handlers
{
    public class GetAllSongLyricsHandler 
        : IRequestHandler<GetAllSongLyricsRequest, IList<SongDTO>>
    {
        private readonly ISongLyricsRepository _repository;

        public GetAllSongLyricsHandler(ISongLyricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<SongDTO>> Handle(
            GetAllSongLyricsRequest request, CancellationToken cancellationToken)
        {
            var songs = await _repository.GetAsync();

            return songs;
        }
    }
}
