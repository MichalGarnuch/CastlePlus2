using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.DeleteKategoriaKosztu
{
    public class DeleteKategoriaKosztuCommand : IRequest<bool>
    {
        public long IdKategoriiKosztu { get; set; }
    }
}
