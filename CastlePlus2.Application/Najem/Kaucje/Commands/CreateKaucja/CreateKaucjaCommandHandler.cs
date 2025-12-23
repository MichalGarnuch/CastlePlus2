using System;
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
            if (request.IdUmowyNajmu == Guid.Empty)
                throw new InvalidOperationException("IdUmowyNajmu jest wymagany.");

            if (string.IsNullOrWhiteSpace(request.RodzajOperacji))
                throw new InvalidOperationException("RodzajOperacji jest wymagany.");

            if (request.RodzajOperacji.Length > 20)
                throw new InvalidOperationException("RodzajOperacji może mieć maksymalnie 20 znaków.");

            if (string.IsNullOrWhiteSpace(request.KodWaluty))
                throw new InvalidOperationException("KodWaluty jest wymagany.");

            if (request.KodWaluty.Trim().Length != 3)
                throw new InvalidOperationException("KodWaluty musi mieć dokładnie 3 znaki.");

            var entity = new Kaucja
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                RodzajOperacji = request.RodzajOperacji.Trim(),
                Kwota = request.Kwota,
                KodWaluty = request.KodWaluty.Trim().ToUpperInvariant(),
                DataOperacji = request.DataOperacji
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<KaucjaDto>(entity);
        }
    }
}