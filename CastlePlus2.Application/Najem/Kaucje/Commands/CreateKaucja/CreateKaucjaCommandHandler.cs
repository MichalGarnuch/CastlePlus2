using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Domain.Entities.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.CreateKaucja
{
    public class CreateKaucjaCommandHandler : IRequestHandler<CreateKaucjaCommand, KaucjaDto>
    {
        private readonly IKaucjaRepository _repo;
        private readonly IMapper _mapper;

        public CreateKaucjaCommandHandler(IKaucjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<KaucjaDto> Handle(CreateKaucjaCommand request, CancellationToken ct)
        {
            var entity = new Kaucja
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                Kwota = request.Kwota,
                KodWaluty = request.KodWaluty,
                DataWplaty = request.DataWplaty,
                DataZwrotu = request.DataZwrotu,
                Status = request.Status
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<KaucjaDto>(entity);
        }
    }
}
