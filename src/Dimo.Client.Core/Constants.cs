using System.Collections.Generic;

namespace Dimo.Client.Core
{
    /// <summary>
    /// DIMO API environments
    /// </summary>
    public enum DimoEnvironment { Development, Production }
    
    internal static class ProdApiUrls
    {
        public const string Auth = "https://auth.dimo.zone";
        public const string Devices = "https://devices-api.dimo.zone";
        public const string DeviceData = "https://device-data-api.dimo.zone";
        public const string DeviceDefinitions = "https://device-definitions-api.dimo.zone";
        public const string Events = "https://events-api.dimo.zone";
        public const string TokenExchange = "https://token-exchange-api.dimo.zone";
        public const string Trips = "https://trips-api.dimo.zone";
        public const string User = "https://users-api.dimo.zone";
        public const string Valuations = "https://valuations-api.dimo.zone";
        public const string VehicleSignalDecoding = "https://vehicle-signal-decoding.dimo.zone";
    }
    
    internal static class DevApiUrls
    {
        public const string Auth = "https://auth.dev.dimo.zone";
        public const string Devices = "https://devices-api.dev.dimo.zone";
        public const string DeviceData = "https://device-data-api.dev.dimo.zone";
        public const string DeviceDefinitions = "https://device-definitions-api.dev.dimo.zone";
        public const string Events = "https://events-api.dev.dimo.zone";
        public const string TokenExchange = "https://token-exchange-api.dev.dimo.zone";
        public const string Trips = "https://trips-api.dev.dimo.zone";
        public const string User = "https://users-api.dev.dimo.zone";
        public const string Valuations = "https://valuations-api.dev.dimo.zone";
        public const string VehicleSignalDecoding = "https://vehicle-signal-decoding.dev.dimo.zone";
    }

    internal static class ApiNames
    {
        public const string Auth = "AuthApi";
        public const string Devices = "DevicesApi";
        public const string DeviceData = "DeviceDataApi";
        public const string DeviceDefinitions = "DeviceDefinitionsApi";
        public const string Events = "EventsApi";
        public const string TokenExchange = "TokenExchangeApi";
        public const string Trips = "TripsApi";
        public const string User = "UserApi";
        public const string Valuations = "ValuationsApi";
        public const string VehicleSignalDecoding = "VehicleSignalDecodingApi";
    }
    
    internal static class Constants
    {
        public const string UserAgent = "dimo-dotnet-sdk";
        public static Dictionary<DimoEnvironment, Dictionary<string, string>> ApiUrls =
            new Dictionary<DimoEnvironment, Dictionary<string, string>>
            {
                {
                    DimoEnvironment.Development, new Dictionary<string, string>
                    {
                        {
                            ApiNames.DeviceData,
                            DevApiUrls.DeviceData
                        },
                        {
                            ApiNames.Devices,
                            DevApiUrls.Devices
                        },
                        {
                            ApiNames.DeviceDefinitions,
                            DevApiUrls.DeviceDefinitions
                        },
                        {
                            ApiNames.Events,
                            DevApiUrls.Events
                        },
                        {
                            ApiNames.TokenExchange,
                            DevApiUrls.TokenExchange
                        },
                        {
                            ApiNames.Trips,
                            DevApiUrls.Trips
                        },
                        {
                            ApiNames.User,
                            DevApiUrls.User
                        },
                        {
                            ApiNames.Valuations,
                            DevApiUrls.Valuations
                        },
                        {
                            ApiNames.VehicleSignalDecoding,
                            DevApiUrls.VehicleSignalDecoding
                        },
                        {
                            ApiNames.Auth,
                            DevApiUrls.Auth
                        }
                    }

                },
                {
                    DimoEnvironment.Production, new Dictionary<string, string>
                    {
                        {
                            ApiNames.DeviceData,
                            ProdApiUrls.DeviceData
                        },
                        {
                            ApiNames.Devices,
                            ProdApiUrls.Devices
                        },
                        {
                            ApiNames.DeviceDefinitions,
                            ProdApiUrls.DeviceDefinitions
                        },
                        {
                            ApiNames.Events,
                            ProdApiUrls.Events
                        },
                        {
                            ApiNames.TokenExchange,
                            ProdApiUrls.TokenExchange
                        },
                        {
                            ApiNames.Trips,
                            ProdApiUrls.Trips
                        },
                        {
                            ApiNames.User,
                            ProdApiUrls.User
                        },
                        {
                            ApiNames.Valuations,
                            ProdApiUrls.Valuations
                        },
                        {
                            ApiNames.VehicleSignalDecoding,
                            ProdApiUrls.VehicleSignalDecoding
                        },
                        {
                            ApiNames.Auth,
                            ProdApiUrls.Auth
                        }
                    }
                }
            };

        public static Dictionary<DimoEnvironment, RpcSigner> RpcSigners = new Dictionary<DimoEnvironment, RpcSigner>
        {
            {
                DimoEnvironment.Development, 
                new RpcSigner
                {
                    NftAddress = "0xbA5738a18d83D41847dfFbDC6101d37C69c9B0cF",
                    RpcProvider = "https://eth.llamarpc.com"
                }
            },
            { 
                DimoEnvironment.Production, 
                new RpcSigner {
                    NftAddress = "0xbA5738a18d83D41847dfFbDC6101d37C69c9B0cF",
                    RpcProvider = "https://eth.llamarpc.com"
                }
            }
        };
    }

    internal sealed class RpcSigner
    {
        public string NftAddress { get; set; }
        public string RpcProvider { get; set; }
    } 
}