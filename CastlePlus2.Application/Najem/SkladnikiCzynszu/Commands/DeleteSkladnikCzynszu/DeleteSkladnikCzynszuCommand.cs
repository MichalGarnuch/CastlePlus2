using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.DeleteSkladnikCzynszu
{
    public class DeleteSkladnikCzynszuCommand : IRequest<bool>
    {
        public long IdSkladnikaCzynszu { get; }

        public DeleteSkladnikCzynszuCommand(long idSkladnikaCzynszu)
        {
            IdSkladnikaCzynszu = idSkladnikaCzynszu;
        }
    }
}