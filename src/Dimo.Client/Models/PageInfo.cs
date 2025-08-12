namespace Dimo.Client.Models
{
    #if NETSTANDARD
    public class PageInfo
    {
        public string EndCursor { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string StartCursor { get; set; }
    }
    #elif NET6_0_OR_GREATER
    public record PageInfo(string EndCursor, bool HasNextPage, bool HasPreviousPage, string StartCursor);
    #endif
}