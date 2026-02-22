using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Client.Services.Auth.RequestAccess
{
    public interface IRequestAccessAdminService
    {
        Task<RequestAccessDto[]> GetRequestsAsync(string status);

        // Docelowa sygnatura: approve wymaga hasła wpisanego przez admina
        Task ApproveAsync(int requestId, string login, string email, string password, string confirmPassword, string[] roleCodes);

        Task RejectAsync(int requestId, string? reason);
    }
}