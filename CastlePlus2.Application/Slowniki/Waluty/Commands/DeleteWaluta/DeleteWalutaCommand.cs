using MediatR;

namespace CastlePlus2.Application.Slowniki.Waluty.Commands.DeleteWaluta
{
    public class DeleteWalutaCommand : IRequest<bool>
    {
        public string KodWaluty { get; set; } = string.Empty;
    }
}
