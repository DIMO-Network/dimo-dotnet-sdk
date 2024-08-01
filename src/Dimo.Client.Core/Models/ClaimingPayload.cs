namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class ClaimingPayload
    {
        public Domain Domain { get; set; }
        public Message Message { get; set; }
        public string PrimaryType { get; set; }
        public Types Types { get; set; }
    }

    public class Domain
    {
        public object ChainId { get; set; }
        public string Name { get; set; }
        public string Salt { get; set; }
        public string VerifyingContract { get; set; }
        public string Version { get; set; }
    }

    public class Message
    {
        public AdditionalProperty AdditionalProperty1 { get; set; }
    }

    public class Types
    {
        public AdditionalProperty[] AdditionalProperty1 { get; set; }
        public AdditionalProperty[] AdditionalProperty2 { get; set; }
        public AdditionalProperty[] AdditionalProperty3 { get; set; }
    }

    public class AdditionalProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    
#elif NET6_0_OR_GREATER
    public record ClaimingPayload(Domain Domain, Message Message, string PrimaryType, Types Types);

    public record Domain(object ChainId, string Name, string Salt, string VerifyingContract, string Version);

    public record Message(AdditionalProperty AdditionalProperty1);

    public record Types(AdditionalProperty[] AdditionalProperty1, AdditionalProperty[] AdditionalProperty2, AdditionalProperty[] AdditionalProperty3);

    public record AdditionalProperty(string Name, string Value);
#endif
}