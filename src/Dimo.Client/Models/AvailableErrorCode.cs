using System;

namespace Dimo.Client.Models
{
    #if NETSTANDARD
    public class AvailableErrorCode
    {
        public QueryResult[] Queries { get; set; }
    }

    public class QueryResult
    {
        public DateTime CreatedAt { get; set; }
        public ErrorCode[] ErrorCodes { get; set; }
        public DateTime RequestedAt { get; set; }
    }

    public class ErrorCode
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    #elif NET6_0_OR_GREATER
    public record AvailableErrorCode(QueryResult[] Queries);
    
    public record QueryResult(DateTime CreatedAt, ErrorCode[] ErrorCodes, DateTime RequestedAt);
    
    public record ErrorCode(string Code, string Description);
    
    #endif
}