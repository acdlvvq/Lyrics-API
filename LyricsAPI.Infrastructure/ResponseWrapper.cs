using LyricsAPI.Core.Models;

namespace LyricsAPI.Infrastructure
{
    public static class ResponseWrapper
    {
        public static Response<T> Wrap<T>(string message, T item)
        {
            return new Response<T>
            {
                Message = message,
                Value = item
            };
        }
    }
}
