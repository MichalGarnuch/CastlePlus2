namespace CastlePlus2.Domain.Entities.Najem
{
    /// <summary>
    /// Składnik czynszu (np. czynsz bazowy, opłata eksploatacyjna).
    /// Tabela: [najem].[SkladnikCzynszu]
    /// </summary>
    public class SkladnikCzynszu
    {
        public long IdSkladnikaCzynszu { get; set; }
        public Guid IdUmowyNajmu { get; set; }

        public string Nazwa { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;

        public decimal Stawka { get; set; }
        public decimal? IloscBazowa { get; set; }

        public string? KodIndeksacji { get; set; }

        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}
