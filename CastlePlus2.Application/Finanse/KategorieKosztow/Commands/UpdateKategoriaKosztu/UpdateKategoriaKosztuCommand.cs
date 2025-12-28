using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.UpdateKategoriaKosztu
{
    public class UpdateKategoriaKosztuCommand : IRequest<bool>
    {
        public long IdKategoriiKosztu { get; set; }
        public string Kod { get; set; } = string.Empty;
        public string Nazwa { get; set; } = string.Empty;
    }
}