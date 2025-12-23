using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.CreatePrzedmiotNajmu
{
    public class CreatePrzedmiotNajmuCommand : IRequest<PrzedmiotNajmuDto>
    {
        public CreatePrzedmiotNajmuRequest Request { get; set; } = new();
    }
}