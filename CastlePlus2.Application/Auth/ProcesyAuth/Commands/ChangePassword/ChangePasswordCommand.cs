using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.ChangePassword
{
    public sealed record ChangePasswordCommand(
        int UserId,
        string CurrentPassword,
        string NewPassword,
        string ConfirmNewPassword) : IRequest;
}