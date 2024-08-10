using System.Collections.Generic;

namespace Dimo.Client.Graphql
{
    /// <summary>
    /// DIMO API environments
    /// </summary>
    public enum DimoEnvironment { Development, Production }
    
    internal static class ProdApiUrls
    {
        public const string Identity = "https://identity-api.dimo.zone/query";
        public const string Telemetry = "https://telemetry-api.dimo.zone/query";
    }
    
    internal static class DevApiUrls
    {
        public const string Identity = "https://identity-api.dev.dimo.zone/query";
        public const string Telemetry = "https://telemetry-api.dev.dimo.zone/query";
    }
    
    internal static class ApiNames
    {
        public const string Identity = "IdentityApi";
        public const string Telemetry = "TelemetryApi";
    }
    
    public static class Constants
    {
        public const string UserAgent = "dimo-dotnet-sdk";
        public static Dictionary<DimoEnvironment, Dictionary<string, string>> ApiUrls =
            new Dictionary<DimoEnvironment, Dictionary<string, string>>
            {
                {
                    DimoEnvironment.Development, new Dictionary<string, string>
                    {
                        {
                            ApiNames.Identity,
                            DevApiUrls.Identity
                        },
                        {
                            ApiNames.Telemetry,
                            DevApiUrls.Telemetry
                        },
                        
                    }

                },
                {
                    DimoEnvironment.Production, new Dictionary<string, string>
                    {
                        {
                            ApiNames.Identity,
                            ProdApiUrls.Identity
                        },
                        {
                            ApiNames.Telemetry,
                            ProdApiUrls.Telemetry
                        },
                        
                    }
                }
            };
    }
}