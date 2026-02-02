using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Client.Services.Auth.RequestAccess
{
    public interface IRequestAccessAdminService
    {
        Task<RequestAccessDto[]> GetRequestsAsync(string status);
        Task ApproveAsync(int requestId, string login, string email, string[] roleCodes);
        Task RejectAsync(int requestId, string? reason);
    }
}