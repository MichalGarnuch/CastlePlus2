using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Commands.CreateAdres
{
    /// <summary>
    /// Komenda tworząca nowy adres.
    /// Zwraca IdAdresu (long).
    /// </summary>
    public class CreateAdresCommand : IRequest<long>
    {
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

