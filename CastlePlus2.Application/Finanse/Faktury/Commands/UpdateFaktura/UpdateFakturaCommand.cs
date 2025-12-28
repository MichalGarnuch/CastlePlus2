using System;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Commands.UpdateFaktura
{
    // Standard: Command płaski, Controller mapuje Request -> Command
    public sealed class UpdateFakturaCommand : IRequest<bool>
    {
        public long IdFaktury { get; set; }
        public string NumerFaktury { get; set; } = string.Empty;
        public long IdPodmiotu { get; set; }
        public DateTime DataWystawienia { get; set; }
        public DateTime? DataSprzedazy { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public decimal? KwotaNetto { get; set; }
        public decimal? KwotaBrutto { get; set; }
    }
}