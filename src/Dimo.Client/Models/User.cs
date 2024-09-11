using System;

namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class User
    {
        public string Id { get; set; }
        public Email Email { get; set; }
        public Web3 Web3 { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CountryCode { get; set; }
        public DateTime AgreedTosAt { get; set; }
        public string ReferralCode { get; set; }
        public string ReferredBy { get; set; }
        public DateTime ReferredAt { get; set; }
    }

    public class Email
    {
        public string Address { get; set; }
        public bool Confirmed { get; set; }
        public DateTime ConfirmationSentAt { get; set; }
    }

    public class Web3
    {
        public string Address { get; set; }
        public bool Confirmed { get; set; }
        public bool Used { get; set; }
        public bool InApp { get; set; }
        public DateTime ChallengeSentAt { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record User(string Id, Email Email, Web3 Web3, DateTime CreatedAt, string CountryCode, DateTime AgreedTosAt, string ReferralCode, string ReferredBy, DateTime ReferredAt);

    public record Email(string Address, bool Confirmed, DateTime ConfirmationSentAt);

    public record Web3(string Address, bool Confirmed, bool Used, bool InApp, DateTime ChallengeSentAt);
#endif
}