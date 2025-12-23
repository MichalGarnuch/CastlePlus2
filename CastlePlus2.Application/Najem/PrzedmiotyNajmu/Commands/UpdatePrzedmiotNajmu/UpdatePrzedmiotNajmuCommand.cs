using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.UpdatePrzedmiotNajmu
{
    public class UpdatePrzedmiotNajmuCommand : IRequest<PrzedmiotNajmuDto?>
    {
        public long IdPrzedmiotuNajmu { get; set; }
        public UpdatePrzedmiotNajmuRequest Request { get; set; } = new();
    }
}