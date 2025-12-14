using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Domain.Entities.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.CreateSkladnikCzynszu
{
    public class CreateSkladnikCzynszuCommandHandler : IRequestHandler<CreateSkladnikCzynszuCommand, SkladnikCzynszuDto>
    {
        private readonly ISkladnikCzynszuRepository _repo;
        private readonly IMapper _mapper;

        public CreateSkladnikCzynszuCommandHandler(ISkladnikCzynszuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SkladnikCzynszuDto> Handle(CreateSkladnikCzynszuCommand request, CancellationToken ct)
        {
            var entity = new SkladnikCzynszu
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                Nazwa = request.Nazwa,
                KodJednostki = request.KodJednostki,
                Stawka = request.Stawka,
                IloscBazowa = request.IloscBazowa,
                KodIndeksacji = request.KodIndeksacji,
                OdDnia = request.OdDnia,
                DoDnia = request.DoDnia
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<SkladnikCzynszuDto>(entity);
        }
    }
}
