using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Domain.Entities.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.CreateUmowaNajmu
{
    public class CreateUmowaNajmuCommandHandler : IRequestHandler<CreateUmowaNajmuCommand, UmowaNajmuDto>
    {
        private readonly IUmowaNajmuRepository _repo;
        private readonly IMapper _mapper;

        public CreateUmowaNajmuCommandHandler(IUmowaNajmuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UmowaNajmuDto> Handle(CreateUmowaNajmuCommand request, CancellationToken ct)
        {
            var entity = new UmowaNajmu
            {
                IdNajemcy = request.IdNajemcy,
                IdWynajmujacego = request.IdWynajmujacego,
                DataZawarcia = request.DataZawarcia.Date,
                DataPoczatku = request.DataPoczatku.Date,
                DataZakonczenia = request.DataZakonczenia?.Date,
                KodWaluty = request.KodWaluty,
                KodIndeksacji = request.KodIndeksacji
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<UmowaNajmuDto>(entity);
        }
    }
}
