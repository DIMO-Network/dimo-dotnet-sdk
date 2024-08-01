using System;

namespace Dimo.Client.Core.Models
{
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
}