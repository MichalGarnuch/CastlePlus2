using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Commands.DeleteLicznik
{
    public class DeleteLicznikCommand : IRequest<bool>
    {
        public long IdLicznika { get; }

        public DeleteLicznikCommand(long idLicznika)
        {
            IdLicznika = idLicznika;
        }
    }
}
