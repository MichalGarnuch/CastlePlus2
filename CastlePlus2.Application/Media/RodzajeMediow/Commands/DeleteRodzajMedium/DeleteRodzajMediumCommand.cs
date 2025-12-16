using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Commands.DeleteRodzajMedium
{
    public sealed class DeleteRodzajMediumCommand : IRequest<bool>
    {
        public string KodRodzaju { get; }

        public DeleteRodzajMediumCommand(string kodRodzaju)
        {
            KodRodzaju = kodRodzaju;
        }
    }
}
