using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.DeleteKaucja
{
    public class DeleteKaucjaCommand : IRequest<bool>
    {
        public long IdKaucji { get; }

        public DeleteKaucjaCommand(long idKaucji)
        {
            IdKaucji = idKaucji;
        }
    }
}