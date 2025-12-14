using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Domain.Entities.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.CreatePrzedmiotNajmu
{
    public class CreatePrzedmiotNajmuCommandHandler : IRequestHandler<CreatePrzedmiotNajmuCommand, PrzedmiotNajmuDto>
    {
        private readonly IPrzedmiotNajmuRepository _repo;
        private readonly IMapper _mapper;

        public CreatePrzedmiotNajmuCommandHandler(IPrzedmiotNajmuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PrzedmiotNajmuDto> Handle(CreatePrzedmiotNajmuCommand request, CancellationToken ct)
        {
            var entity = new PrzedmiotNajmu
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                IdEncji = request.IdEncji,
                UdzialProcent = request.UdzialProcent,
                OdDnia = request.OdDnia,
                DoDnia = request.DoDnia
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<PrzedmiotNajmuDto>(entity);
        }
    }
}
