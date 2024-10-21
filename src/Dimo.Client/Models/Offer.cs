using System;

namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class OfferData
    {
        public OfferSet[] OfferSets { get; set; }
    }
    public class OfferSet
    {
        public int Mileage { get; set; }
        public string OdometerMeasurementType { get; set; }
        public Offer[] Offers { get; set; }
        public string Source { get; set; }
        public DateTimeOffset Updated { get; set; }
        public string Zipcode { get; set; }
    }
    
    public class Offer
    {
        public string DeclineReason { get; set; }
        public string Error { get; set; }
        public string Grade { get; set; }
        public decimal Price { get; set; }
        public string Url { get; set; }
        public string Vendor { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record OfferData(OfferSet[] OfferSets);
    public record OfferSet(int Mileage, string OdometerMeasurementType, Offer[] Offers, string Source, DateTimeOffset Updated, string Zipcode);
    public record Offer(string DeclineReason, string Error, string Grade, decimal Price, string Url, string Vendor);
#endif
}