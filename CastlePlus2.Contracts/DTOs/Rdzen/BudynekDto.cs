using System;

namespace CastlePlus2.Contracts.DTOs.Rdzen
{
    /// <summary>
    /// DTO dla encji Budynek – używany w warstwie API / Frontend.
    /// </summary>
    public class BudynekDto
    {
        /// <summary>
        /// IdEncji (klucz główny z tabeli rdzen.Encja / rdzen.Budynek).
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id nieruchomości, do której należy budynek.
        /// </summary>
        public Guid IdNieruchomosci { get; set; }

        /// <summary>
        /// Kod budynku w obrębie nieruchomości.
        /// </summary>
        public string KodBudynku { get; set; } = string.Empty;

        /// <summary>
        /// Opcjonalny IdAdresu – jeśli budynek ma własny adres.
        /// </summary>
        public long? IdAdresu { get; set; }

        /// <summary>
        /// Łączna liczba kondygnacji (nad + podziemnych).
        /// Odpowiada kolumnie [Kondygnacje] w SQL.
        /// </summary>
        public short? Kondygnacje { get; set; }

        public decimal? PowierzchniaUzytkowa { get; set; }

        // Na tym etapie NIE zagnieżdżamy całego AdresDto / NieruchomoscDto,
        // żeby zachować prostotę (tak jak początkowy etap dla Nieruchomosci).
    }
}
