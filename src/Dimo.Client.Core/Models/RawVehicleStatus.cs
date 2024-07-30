using System;
using System.Collections.Generic;

namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class RawVehicleStatus
    {
        public StatusValue<string> Vim { get; set; }
        public StatusValue<string> Make { get; set; }
        public StatusValue<int> Year { get; set; }
        public StatusValue<string> Model { get; set; }
        public ErrorResult Errors { get; set; }
        public StatusValue<DateTime> TimeStamp { get; set; }
        public StatusValue<string> VehicleId { get; set; }
    }

    public class ErrorResult
    {
        public List<ErrorResult> Values { get; set; }
    }

    public class StatusError
    {
        public string Type { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorType { get; set; }
        public ResolutionType Resolution { get; set; }
        public string Description { get; set; }
    }

    public class ResolutionType
    {
        public string Type { get; set; }
    }

    public class StatusValue<T>
    {
        public T Value { get; set; }
        public string Source { get; set; }
        public DateTime Timestamp { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record RawVehicleStatus
    {
        public StatusValue<string> Vim { get; init; }
        public StatusValue<string> Make { get; init; }
        public StatusValue<int> Year { get; init; }
        public StatusValue<string> Model { get; init; }
        public ErrorResult Errors { get; init; }
        public StatusValue<DateTime> TimeStamp { get; init; }
        public StatusValue<string> VehicleId { get; init; }
    }
    
    public record ErrorResult(List<ErrorResult> Values);

    public record StatusError
    {
        public string Type { get; init; }
        public int ErrorCode { get; init; }
        public string ErrorType { get; init; }
        public ResolutionType Resolution { get; init; }
        public string Description { get; init; }
    }

    public record ResolutionType(string Type);

    public record StatusValue<T>
    {
        public T Value { get; init; }
        public string Source { get; init; }
        public DateTime Timestamp { get; init; }
    }
#endif
}