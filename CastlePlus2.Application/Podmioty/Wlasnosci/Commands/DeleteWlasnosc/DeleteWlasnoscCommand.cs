using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.DeleteWlasnosc
{
    public class DeleteWlasnoscCommand : IRequest<bool>
    {
        public long IdWlasnosci { get; }

        public DeleteWlasnoscCommand(long idWlasnosci)
        {
            IdWlasnosci = idWlasnosci;
        }
    }
}