using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.DeleteEncja
{
    public class DeleteEncjaCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteEncjaCommand(Guid id)
        {
            Id = id;
        }
    }
}