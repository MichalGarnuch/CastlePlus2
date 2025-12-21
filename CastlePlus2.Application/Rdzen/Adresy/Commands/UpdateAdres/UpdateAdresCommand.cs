using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Commands.UpdateAdres
{
    public class UpdateAdresCommand : IRequest<bool>
    {
        public long IdAdresu { get; set; }

        public string Kraj { get; set; } = "Polska";
        public string? Wojewodztwo { get; set; }
        public string? Powiat { get; set; }
        public string? Gmina { get; set; }
        public string Miejscowosc { get; set; } = string.Empty;
        public string? Ulica { get; set; }
        public string? Numer { get; set; }
        public string? KodPocztowy { get; set; }
        public string? AdresPelny { get; set; }
    }
}
