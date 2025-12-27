using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.UpdatePrzypisanieAdresu
{
    public sealed class UpdatePrzypisanieAdresuCommand : IRequest<bool>
    {
        public long IdPrzypisaniaAdresu { get; set; }

        public Guid IdEncji { get; set; }
        public long IdAdresu { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}
