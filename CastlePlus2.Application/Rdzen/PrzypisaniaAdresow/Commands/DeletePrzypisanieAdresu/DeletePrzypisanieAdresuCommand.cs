using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.DeletePrzypisanieAdresu
{
    public sealed record DeletePrzypisanieAdresuCommand(long IdPrzypisaniaAdresu) : IRequest<bool>;
}
