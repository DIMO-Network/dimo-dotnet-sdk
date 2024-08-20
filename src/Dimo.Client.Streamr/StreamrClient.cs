namespace Dimo.Client.Streamr
{
    public enum EnvironmentId
    {
        Polygon,
        PolygonAmoy,
        Dev2
    }
    
    public enum LogLevel {}

    public class StreamrClientConfig
    {
        public EnvironmentId Environment { get; set; }
        public string Id { get; set; }
        public LogLevel LogLevel { get; set; }
        
    }
    
    public class StreamrClient
    {
        
    }
}