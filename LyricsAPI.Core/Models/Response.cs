namespace LyricsAPI.Core.Models
{
    public class Response<T>
    {
        public string Message { get; set; } = string.Empty;
        public T Value { get; set; } = default!;
    }
}
