using System;
using System.Collections.Generic;

namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class TripHistory
    {
        public List<Trip> Trips { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }

    public class Trip
    {
        public string Id { get; set; }
        public TripStop Start { get; set; }
        public TripStop End { get; set; }
        public bool DroppedData { get; set; }
    }

    public class TripStop
    {
        public DateTimeOffset Time { get; set; }
        public Location Location { get; set; }
        public Location EstimatedLocation { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record TripHistory(List<Trip> Trips, int TotalPages, int CurrentPage);
    
    public record Trip(string Id, TripStop Start, TripStop End, bool DroppedData);
    
    public record TripStop(DateTimeOffset Time, Location Location, Location EstimatedLocation);
#endif
}