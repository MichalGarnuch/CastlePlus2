using System;
using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.DTOs.Slowniki;

namespace CastlePlus2.Contracts.DTOs.Finanse
{
    public class PlatnoscContextDto
    {
        public List<PodmiotDto> Podmioty { get; set; } = new();
        public List<WalutaDto> Waluty { get; set; } = new();
        public List<FakturaDoRozliczeniaDto> Faktury { get; set; } = new();
    }

    public class FakturaDoRozliczeniaDto
    {
        public long IdFaktury { get; set; }
        public string NumerFaktury { get; set; } = string.Empty;
        public long IdPodmiotu { get; set; }
        public DateTime DataWystawienia { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public decimal KwotaBrutto { get; set; }
        public decimal KwotaRozliczona { get; set; }
        public decimal KwotaPozostala { get; set; }
    }
}