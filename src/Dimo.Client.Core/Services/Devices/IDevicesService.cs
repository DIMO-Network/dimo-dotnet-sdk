using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

namespace Dimo.Client.Core.Services.Devices
{
    public interface IDevicesService
    {
        Task<CreatedVehicle> CreateVehicleAsync(string countryCode, string deviceDefinitionId, string authToken, CancellationToken cancellationToken = default);
        Task<CreatedVehicle> CreateVehicleFromSmartCarAsync(string code, string countryCode, string redirectUri, string authToken, CancellationToken cancellationToken = default);
        Task<CreatedVehicle> CreateVehicleFromVinAsync(string vin, string countryCode, string canProtocol, string authToken, CancellationToken cancellationToken = default);
        Task UpdateVehicleVinAsync(string userDeviceId, string vin, string signature, string authToken, CancellationToken cancellationToken = default);
        Task<ClaimingPayload> GetClaimingPayloadAsync(string serial, string authToken, CancellationToken cancellationToken = default);
        Task SignClaimingPayloadAsync(string serial, string aftermarketDeviceSignature, string userSignature, string authToken, CancellationToken cancellationToken = default);
        Task<ClaimingPayload> GetMintingPayloadAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default);
        Task SignMintingPayloadAsync(string userDeviceId, string aftermarketDeviceSignature, string userSignature, string authToken, CancellationToken cancellationToken = default);
        Task OptInShareDataAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default);
        Task RefreshSmartCarDataAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default);
        Task GetPairingPayloadAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default);
        Task SignPairingPayloadAsync(string userDeviceId, int[] aftermarketDeviceSignature, string externalId, int[] signature, string authToken, CancellationToken cancellationToken = default);
        Task<CommandResponse> LockDoorsAsync(long tokenId, string authToken, CancellationToken cancellationToken = default);
        Task<CommandResponse> UnlockDoorsAsync(long tokenId, string authToken, CancellationToken cancellationToken = default);
        Task<CommandResponse> OpenFrunkAsync(long tokenId, string authToken, CancellationToken cancellationToken = default);
        Task<CommandResponse> OpenTrunkAsync(long tokenId, string authToken, CancellationToken cancellationToken = default);
        Task<AvailableErrorCode> ListErrorCodesAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default);
        Task<AvailableErrorCode> SubmitErrorCodesAsync(string userDeviceId, string[] errorCodes, string authToken, CancellationToken cancellationToken = default);
        Task<QueryResult>ClearErrorCodesAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default);
        Task<AftermarketDevice> GetAftermarketDeviceAsync(long tokenId, string authToken, CancellationToken cancellationToken = default);
        Task<Stream> GetAftermarketDeviceImageAsync(long tokenId, string authToken, CancellationToken cancellationToken = default);
        Task<AftermarketDevice> GetAftermarketDeviceMetadataByAddressAsync(string address, string authToken, CancellationToken cancellationToken = default);
    }
}
