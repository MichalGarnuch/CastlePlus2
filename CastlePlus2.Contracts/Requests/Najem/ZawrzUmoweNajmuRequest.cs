using System;

namespace CastlePlus2.Contracts.Requests.Najem
{
    public class ZawrzUmoweNajmuRequest
    {
        public Guid IdLokalu { get; set; }
        public long IdWynajmujacego { get; set; }
        public long IdNajemcy { get; set; }
        public DateOnly DataZawarcia { get; set; }
        public DateOnly DataPoczatku { get; set; }
        public DateOnly? DataZakonczenia { get; set; }
        public string? KodEncji { get; set; }
        public string KodWaluty { get; set; } = string.Empty;
        public string? KodIndeksacji { get; set; }
        public string NazwaCzynszu { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;
        public decimal Stawka { get; set; }
        public decimal? IloscBazowa { get; set; }
        public decimal? KwotaKaucji { get; set; }
        public decimal? UdzialProcent { get; set; }
    }
}
