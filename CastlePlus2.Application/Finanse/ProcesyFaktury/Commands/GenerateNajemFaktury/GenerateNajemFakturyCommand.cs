using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.GenerateNajemFaktury
{
    public class GenerateNajemFakturyCommand : IRequest<GenerateNajemFakturyResultDto>
    {
        public string Miesiac { get; set; } = string.Empty;
        public DateTime DataWystawienia { get; set; }
    }
}