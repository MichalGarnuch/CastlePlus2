using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.UpdatePrzedmiotNajmu
{
    public class UpdatePrzedmiotNajmuCommandHandler : IRequestHandler<UpdatePrzedmiotNajmuCommand, PrzedmiotNajmuDto?>
    {
        private readonly IPrzedmiotNajmuRepository _repo;
        private readonly IMapper _mapper;

        public UpdatePrzedmiotNajmuCommandHandler(IPrzedmiotNajmuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PrzedmiotNajmuDto?> Handle(UpdatePrzedmiotNajmuCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdPrzedmiotuNajmu, ct);
            if (entity == null)
            {
                return null;
            }

            var payload = request.Request;
            entity.IdUmowyNajmu = payload.IdUmowyNajmu;
            entity.IdEncji = payload.IdEncji;
            entity.UdzialProcent = payload.UdzialProcent;
            entity.OdDnia = payload.OdDnia;
            entity.DoDnia = payload.DoDnia;

            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<PrzedmiotNajmuDto>(entity);
        }
    }
}