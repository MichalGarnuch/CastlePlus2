using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.CreateKategoriaKosztu
{
    public class CreateKategoriaKosztuCommand : IRequest<KategoriaKosztuDto>
    {
        public string Kod { get; set; } = string.Empty;
        public string Nazwa { get; set; } = string.Empty;
    }
}
