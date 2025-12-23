using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.DeletePrzedmiotNajmu
{
    public record DeletePrzedmiotNajmuCommand(long IdPrzedmiotuNajmu) : IRequest<bool>;
}