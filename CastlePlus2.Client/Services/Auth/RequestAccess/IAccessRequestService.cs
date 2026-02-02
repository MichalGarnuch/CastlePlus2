using System.Threading.Tasks;
using CastlePlus2.Contracts.Requests.Auth;

namespace CastlePlus2.Client.Services.Auth.RequestAccess
{
    public interface IAccessRequestService
    {
        Task<int> CreateRequestAsync(CreateRequestAccessRequest request);
        Task ActivateAccountAsync(string token, string password, string confirmPassword);
    }
}