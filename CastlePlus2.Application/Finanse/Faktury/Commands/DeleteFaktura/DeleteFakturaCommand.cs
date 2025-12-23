using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Commands.DeleteFaktura
{
    public class DeleteFakturaCommand : IRequest<bool>
    {
        public long IdFaktury { get; }

        public DeleteFakturaCommand(long idFaktury)
        {
            IdFaktury = idFaktury;
        }
    }
}
