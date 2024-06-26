﻿using LyricsAPI.Core.DTO;
using MediatR;

namespace LyricsAPI.Application.SongLyricsUseCases.Queries
{
    public record GetAllSongLyricsRequest() : IRequest<IList<SongDTO>>;
}
