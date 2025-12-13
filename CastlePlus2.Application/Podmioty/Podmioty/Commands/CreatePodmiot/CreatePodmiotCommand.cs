using MediatR;
using CastlePlus2.Contracts.DTOs.Podmioty;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.CreatePodmiot
{
    public class CreatePodmiotCommand : IRequest<PodmiotDto>
    {
        public string Nazwa { get; set; } = default!;
        public string? NIP { get; set; }
        public string? REGON { get; set; }
        public string? PESEL { get; set; }
        public string TypPodmiotu { get; set; } = default!;
    }
}
