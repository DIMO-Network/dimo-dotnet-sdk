using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

namespace Dimo.Client.Core.Services.DeviceData
{
    public interface IDeviceDataService
    {
        /// <summary>
        /// Retrieves the Vehicle History by the Vehicle identified with the provided Vehicle ID.
        /// This endpoint returns the historical data of a given time period, the data volume is very large so please handle with care.
        /// </summary>
        /// <param name="tokenId">
        /// Vehicle token ID, this is the token ID of your vehicle NFT.
        /// A prerequisite is to obtain a token that is permitted to access trips data for this specific vehicle.
        /// </param>
        /// <param name="startDate">
        /// Starting timestamp in the ISO8601 format (YYYY-MM-DD). Defaults to 2 weeks ago if not specified.
        /// </param>
        /// <param name="endDate">
        /// Ending timestamp in the ISO8601 format (YYYY-MM-DD). Defaults to today if not specified.
        /// </param>
        /// <param name="cancellationToken">
        ///  A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation. Which result is of type <see cref="VehicleHistory"/></returns>
        Task<VehicleHistory> GetVehicleHistoryAsync(long tokenId, string startDate = null, string endDate = null,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<VehicleStatus> GetVehicleStatusAsync(long tokenId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RawVehicleStatus> GetRawVehicleStatusAsync(long tokenId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDeviceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<VehicleStatus> GetUserDeviceStatusAsync(string userDeviceId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDeviceId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<VehicleHistory> GetUserDeviceHistoryAsync(string userDeviceId, string startDate = null, string endDate = null,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDeviceId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ExportDataResponse> ExportUserDeviceDataAsync(string userDeviceId, CancellationToken cancellationToken = default);
        
    }
}