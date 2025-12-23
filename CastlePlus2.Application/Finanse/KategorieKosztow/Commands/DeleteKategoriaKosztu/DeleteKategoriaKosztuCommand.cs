using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.DeleteKategoriaKosztu
{
    public class DeleteKategoriaKosztuCommand : IRequest
    {
        public long IdKategoriiKosztu { get; set; }
    }
}
