using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.DeleteZleceniePracy
{
    public class DeleteZleceniePracyCommand : IRequest
    {
        public long IdZlecenia { get; set; }
    }
}