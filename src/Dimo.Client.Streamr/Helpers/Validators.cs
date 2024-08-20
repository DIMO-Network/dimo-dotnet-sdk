namespace Dimo.Client.Streamr.Helpers
{
    public class Validators
    {
    }

    public static class NumberUtils
    {
        private const long SafeMinInteger = -(1L << 53) + 1;
        private const long SafeMaxInteger = (1L << 53) - 1;
        
        public static bool IsSafeInteger(double value)
        {
            return value >= SafeMinInteger && value <= SafeMaxInteger;
        }
    }
}