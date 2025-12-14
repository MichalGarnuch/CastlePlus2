namespace CastlePlus2.Contracts.DTOs.Media
{
    public class LicznikDto
    {
        public long IdLicznika { get; set; }
        public long IdPrzylacza { get; set; }
        public long? IdLicznikaNadrzednego { get; set; }

        public string NumerNV { get; set; } = string.Empty;
        public string KodJednostki { get; set; } = string.Empty;

        public decimal? WspolczynnikPrzeliczeniowy { get; set; }
        public bool Aktywny { get; set; }
    }
}
