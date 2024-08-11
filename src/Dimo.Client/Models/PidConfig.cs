namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class PidConfig
    {
        public string TemplateName { get; set; }
        public string Version { get; set; }
        public Signal[] Requests { get; set; }
    }

    public class Signal
    {
        public string Name { get; set; }
        public int Header { get; set; }
        public int Mode { get; set; }
        public string Formula { get; set; }
        public int IntervalSeconds { get; set; }
        public string Protocol { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record PidConfig(string TemplateName, string Version, Signal[] Requests);

    public record Signal(string Name, int Header, int Mode, string Formula, int IntervalSeconds, string Protocol);
#endif
}