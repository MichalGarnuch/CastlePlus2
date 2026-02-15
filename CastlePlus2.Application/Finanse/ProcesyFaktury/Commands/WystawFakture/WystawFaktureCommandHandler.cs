using CastlePlus2.Application.Finanse.ProcesyFaktury.Common;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.WystawFakture
{
    public class WystawFaktureCommandHandler : IRequestHandler<WystawFaktureCommand, WystawFaktureResultDto>
    {
        private readonly IFakturaCreationService _fakturaCreationService;

        public WystawFaktureCommandHandler(IFakturaCreationService fakturaCreationService)
        {
            _fakturaCreationService = fakturaCreationService;
        }

        public Task<WystawFaktureResultDto> Handle(WystawFaktureCommand request, CancellationToken ct)
            => _fakturaCreationService.CreateAsync(request, ct);
    }
}