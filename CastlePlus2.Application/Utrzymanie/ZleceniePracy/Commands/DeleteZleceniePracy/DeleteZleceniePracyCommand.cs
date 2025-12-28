using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.DeleteZleceniePracy
{
    public class DeleteZleceniePracyCommand : IRequest<bool>
    {
        public long IdZlecenia { get; set; }
    }
}