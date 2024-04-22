using LyricsAPI.Core.Abstractions;
using LyricsAPI.Core.Models;

namespace LyricsAPI.Infrastructure
{
    public class ResponseWrapper : IResponseWrapper
    {
        public Response<T> Wrap<T>(string message, T item)
        {
            return new Response<T>
            {
                Message = message,
                Value = item
            };
        }
    }
}
