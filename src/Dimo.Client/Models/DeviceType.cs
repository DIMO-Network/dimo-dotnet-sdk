namespace Dimo.Client.Models
{
    public class DeviceType
    {
        public string Make { get; set; }
        public string MakeSlug { get; set; }
        public string Model { get; set; }
        public string[] SubModels { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
    }
}