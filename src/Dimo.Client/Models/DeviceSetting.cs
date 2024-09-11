namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class DeviceSetting
    {
        public string TemplateName { get; set; }
        public decimal SafetyCutOutVoltage { get; set; }
        public int SleepTimerEventDrivenIntervalSeconds { get; set; }
        public int SleepTimerInactivityAfterSleepSeconds { get; set; }
        public int SleepTimerInactivityFallbackIntervalSeconds { get; set; }
        public decimal WakeTriggerVoltageLevel { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record DeviceSetting(string TemplateName, decimal SafetyCutOutVoltage, int SleepTimerEventDrivenIntervalSeconds, int SleepTimerInactivityAfterSleepSeconds, int SleepTimerInactivityFallbackIntervalSeconds, decimal WakeTriggerVoltageLevel);
#endif
}