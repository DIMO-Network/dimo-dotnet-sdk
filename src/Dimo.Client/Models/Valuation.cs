using System;

namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class Valuation
    {
        public ValuationSet[] ValuationSets { get; set; }
    }
    public class ValuationSet
    {
        public string Vendor { get; set; }
        public DateTime Updated { get; set; }
        public int Mileage { get; set; }
        public string TradeInSource { get; set; }
        public int TradeIn { get; set; }
        public int TradeInAverage { get; set; }
        public string RetailSource { get; set; }
        public int Retail { get; set; }
        public int RetailAverage { get; set; }
        public string OdometerUnit { get; set; }
        public long Odometer { get; set; }
        public string OdometerMeasurementType { get; set; }
        public decimal UserDisplayPrice { get; set; }
        public string Currency { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record Valuation(ValuationSet[] ValuationSets);
    public record ValuationSet(string Vendor, DateTime Updated, int Mileage, string TradeInSource, int TradeIn, int TradeInAverage, string RetailSource, int Retail, int RetailAverage, string OdometerUnit, long Odometer, string OdometerMeasurementType, decimal UserDisplayPrice, string Currency);
#endif
}