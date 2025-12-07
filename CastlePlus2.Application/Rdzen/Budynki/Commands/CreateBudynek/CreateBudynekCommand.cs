using System;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.CreateBudynek
{
    /// <summary>
    /// Komenda tworząca nowy budynek na nieruchomości.
    /// </summary>
    public class CreateBudynekCommand : IRequest<BudynekDto>
    {
        public Guid IdNieruchomosci { get; set; }
        public string KodBudynku { get; set; } = string.Empty;
        public long? IdAdresu { get; set; }

        /// <summary>
        /// Łączna liczba kondygnacji – trafia do kolumny [Kondygnacje].
        /// </summary>
        public short? Kondygnacje { get; set; }

        public decimal? PowierzchniaUzytkowa { get; set; }
    }
}
