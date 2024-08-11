namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class VehicleSchemeResponse<T>
    {
        public T Vehicles { get; set; }
    }

    public class CountResult
    {
        public int TotalCount { get; set; }
    }
    
    public class VehicleDefinition
    {
        public VehicleNode[] Nodes { get; set; }
    }

    public class VehicleNode
    {
        public AftermarketDevice AftermarketDevice { get; set; }
        public AftermarketDevice SyntheticDevice { get; set; }
        public Definition Definition { get; set; }
    }

    public class AftermarketDevice
    {
        public long TokenId { get; set; }
        public string Address { get; set; }
    }

    public class Definition
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record VehicleSchemeResponse<T>(T Vehicles);

    public record CountResult(int TotalCount);

    public record VehicleDefinition(VehicleNode[] Nodes);

    public record VehicleNode(AftermarketDevice AftermarketDevice, AftermarketDevice SyntheticDevice, Definition Definition);

    public record Definition(string Make, string Model, int Year);
#endif
}