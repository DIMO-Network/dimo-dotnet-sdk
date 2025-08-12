namespace Dimo.Client.Models
{
    #if NETSTANDARD
    public class UserReward
    {
        public Reward Rewards { get; set; }
    }

    public class Reward
    {
        public decimal TotalTokens { get; set; }
        public EarningsConnection History { get; set; }
    }
    #elif NET6_0_OR_GREATER
    public record UserReward(Reward Rewards);
    public record Reward(decimal TotalTokens, EarningsConnection History);
    #endif
}