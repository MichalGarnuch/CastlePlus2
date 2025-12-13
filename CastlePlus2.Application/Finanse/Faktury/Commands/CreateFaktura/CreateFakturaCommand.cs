using System;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Commands.CreateFaktura
{
    public class CreateFakturaCommand : IRequest<FakturaDto>
    {
        public string NumerFaktury { get; set; } = string.Empty;
        public long IdPodmiotu { get; set; }
        public DateTime DataWystawienia { get; set; }
        public DateTime? DataSprzedazy { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public decimal? KwotaNetto { get; set; }
        public decimal? KwotaBrutto { get; set; }
    }
}
