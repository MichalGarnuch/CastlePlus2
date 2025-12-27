using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.CreateEncja
{
    public class CreateEncjaCommand : IRequest<EncjaDto>
    {
        public string TypEncji { get; set; } = string.Empty;
        public string? KodEncji { get; set; }
    }
}