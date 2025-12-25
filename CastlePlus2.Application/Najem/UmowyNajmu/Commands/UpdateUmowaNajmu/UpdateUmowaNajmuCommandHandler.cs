using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.UpdateUmowaNajmu
{
    public sealed class UpdateUmowaNajmuCommandHandler : IRequestHandler<UpdateUmowaNajmuCommand, UmowaNajmuDto?>
    {
        private readonly IUmowaNajmuRepository _repo;
        private readonly IMapper _mapper;

        public UpdateUmowaNajmuCommandHandler(IUmowaNajmuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UmowaNajmuDto?> Handle(UpdateUmowaNajmuCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return null;

            entity.IdWynajmujacego = cmd.Request.IdWynajmujacego;
            entity.IdNajemcy = cmd.Request.IdNajemcy;
            entity.DataZawarcia = cmd.Request.DataZawarcia.Date;
            entity.DataPoczatku = cmd.Request.DataPoczatku.Date;
            entity.DataZakonczenia = cmd.Request.DataZakonczenia?.Date;
            entity.KodWaluty = cmd.Request.KodWaluty;
            entity.KodIndeksacji = cmd.Request.KodIndeksacji;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<UmowaNajmuDto>(entity);
        }
    }
}