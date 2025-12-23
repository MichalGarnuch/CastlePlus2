using System;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.UpdateKaucja
{
    public class UpdateKaucjaCommandHandler : IRequestHandler<UpdateKaucjaCommand, KaucjaDto?>
    {
        private readonly IKaucjaRepository _repo;
        private readonly IMapper _mapper;

        public UpdateKaucjaCommandHandler(IKaucjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<KaucjaDto?> Handle(UpdateKaucjaCommand request, CancellationToken ct)
        {
            if (request.IdOperacjiKaucji <= 0)
                return null;

            var entity = await _repo.GetForUpdateAsync(request.IdOperacjiKaucji, ct);
            if (entity is null)
                return null;

            var req = request.Request;

            if (req.IdUmowyNajmu == Guid.Empty)
                throw new InvalidOperationException("IdUmowyNajmu jest wymagany.");

            if (string.IsNullOrWhiteSpace(req.RodzajOperacji))
                throw new InvalidOperationException("RodzajOperacji jest wymagany.");

            if (req.RodzajOperacji.Length > 20)
                throw new InvalidOperationException("RodzajOperacji może mieć maksymalnie 20 znaków.");

            if (string.IsNullOrWhiteSpace(req.KodWaluty))
                throw new InvalidOperationException("KodWaluty jest wymagany.");

            if (req.KodWaluty.Trim().Length != 3)
                throw new InvalidOperationException("KodWaluty musi mieć dokładnie 3 znaki.");

            entity.IdUmowyNajmu = req.IdUmowyNajmu;
            entity.RodzajOperacji = req.RodzajOperacji.Trim();
            entity.Kwota = req.Kwota;
            entity.KodWaluty = req.KodWaluty.Trim().ToUpperInvariant();
            entity.DataOperacji = req.DataOperacji;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<KaucjaDto>(entity);
        }
    }
}