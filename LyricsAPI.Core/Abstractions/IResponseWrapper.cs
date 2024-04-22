using LyricsAPI.Core.Models;

namespace LyricsAPI.Core.Abstractions
{
    public interface IResponseWrapper
    {
        public Response<T> Wrap<T>(string message, T item);
    }
}
