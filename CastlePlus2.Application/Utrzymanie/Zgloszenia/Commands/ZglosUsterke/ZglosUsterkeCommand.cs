using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.Zgloszenia.Commands.ZglosUsterke
{
    public class ZglosUsterkeCommand : IRequest<ZglosUsterkeResult>
    {
        public Guid IdEncjiGospodarza { get; set; }
        public string Tytul { get; set; } = string.Empty;
        public string? Opis { get; set; }
    }
}