using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.UpdateEncja
{
    public class UpdateEncjaCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string TypEncji { get; set; } = string.Empty;
        public string? KodEncji { get; set; }
    }
}