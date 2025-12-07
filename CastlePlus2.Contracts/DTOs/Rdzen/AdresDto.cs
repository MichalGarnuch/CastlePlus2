using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Contracts.DTOs.Rdzen
{
    /// <summary>
    /// DTO dla encji Adres – używane w warstwie API / Client.
    /// </summary>
    public class AdresDto
    {
        public long IdAdresu { get; set; }
        public string Kraj { get; set; } = string.Empty;
        public string? Wojewodztwo { get; set; }
        public string? Powiat { get; set; }
        public string? Gmina { get; set; }
        public string Miejscowosc { get; set; } = string.Empty;
        public string? Ulica { get; set; }
        public string? Numer { get; set; }
        public string? KodPocztowy { get; set; }
        public string? AdresPelny { get; set; }

        /// <summary>
        /// Pomocnicza, sformatowana wersja adresu – do list, gridów itp.
        /// </summary>
        public string AdresSformatowany =>
            $"{(string.IsNullOrWhiteSpace(Ulica) ? string.Empty : Ulica + "\\")}{Numer}, {KodPocztowy} {Miejscowosc}".Trim(' ', ',');
    }
}