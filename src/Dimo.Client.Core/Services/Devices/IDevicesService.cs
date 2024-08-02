using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

namespace Dimo.Client.Core.Services.Devices
{
    public interface IDevicesService
    {
        Task<CreatedVehicle> CreateVehicleAsync(string countryCode, string deviceDefinitionId, CancellationToken cancellationToken = default);
        Task<CreatedVehicle> CreateVehicleFromSmartCarAsync(string code, string countryCode, string redirectUri, CancellationToken cancellationToken = default);
        Task<CreatedVehicle> CreateVehicleFromVinAsync(string vin, string countryCode, string canProtocol, CancellationToken cancellationToken = default);
        Task UpdateVehicleVinAsync(string userDeviceId, string vin, string signature, CancellationToken cancellationToken = default);
        Task<ClaimingPayload> GetClaimingPayloadAsync(string serial, CancellationToken cancellationToken = default);
        Task SignClaimingPayloadAsync(string serial, string aftermarketDeviceSignature, string userSignature, CancellationToken cancellationToken = default);
        Task<ClaimingPayload> GetMintingPayloadAsync(string userDeviceId, CancellationToken cancellationToken = default);
        Task SignMintingPayloadAsync(string userDeviceId, string aftermarketDeviceSignature, string userSignature, CancellationToken cancellationToken = default);
        Task OptInShareDataAsync(string userDeviceId, CancellationToken cancellationToken = default);
        Task RefreshSmartCarDataAsync(string userDeviceId, CancellationToken cancellationToken = default);
        Task GetPairingPayloadAsync(string userDeviceId, CancellationToken cancellationToken = default);
        Task SignPairingPayloadAsync(string userDeviceId, int[] aftermarketDeviceSignature, string externalId, int[] signature, CancellationToken cancellationToken = default);
        Task<CommandResponse> LockDoorsAsync(long tokenId, CancellationToken cancellationToken = default);
        Task<CommandResponse> UnlockDoorsAsync(long tokenId, CancellationToken cancellationToken = default);
        Task<CommandResponse> OpenFrunkAsync(long tokenId, CancellationToken cancellationToken = default);
        Task<CommandResponse> OpenTrunkAsync(long tokenId, CancellationToken cancellationToken = default);
        Task<AvailableErrorCode> ListErrorCodesAsync(string userDeviceId, CancellationToken cancellationToken = default);
        Task<AvailableErrorCode> SubmitErrorCodesAsync(string userDeviceId, string[] errorCodes, CancellationToken cancellationToken = default);
        Task<QueryResult>ClearErrorCodesAsync(string userDeviceId, CancellationToken cancellationToken = default);
        Task<AftermarketDevice> GetAftermarketDeviceAsync(long tokenId, CancellationToken cancellationToken = default);
        Task<Stream> GetAftermarketDeviceImageAsync(long tokenId, CancellationToken cancellationToken = default);
        Task<AftermarketDevice> GetAftermarketDeviceMetadataByAddressAsync(string address, CancellationToken cancellationToken = default);
    }
}
