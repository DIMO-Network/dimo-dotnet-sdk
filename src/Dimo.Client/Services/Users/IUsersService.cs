using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Users
{
    public interface IUsersService
    {
        Task<User> GetUserAsync(string authToken, CancellationToken cancellationToken = default);
        Task<User> UpdateUserAsync(string authToken, User user, CancellationToken cancellationToken = default);
        Task DeleteUserAsync(string authToken, CancellationToken cancellationToken = default);
        Task SendConfirmationEmailAsync(string authToken, CancellationToken cancellationToken = default);
        Task ConfirmEmailAsync(string authToken, string confirmationCode, CancellationToken cancellationToken = default);
    }
}