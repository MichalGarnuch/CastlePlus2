using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Queries.GetPowiazanieZleceniaById
{
    public class GetPowiazanieZleceniaByIdQueryHandler
        : IRequestHandler<GetPowiazanieZleceniaByIdQuery, PowiazanieZleceniaDto?>
    {
        private readonly IPowiazanieZleceniaRepository _repo;
        private readonly IMapper _mapper;

        public GetPowiazanieZleceniaByIdQueryHandler(IPowiazanieZleceniaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PowiazanieZleceniaDto?> Handle(GetPowiazanieZleceniaByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.IdPowiazania, ct);
            return entity is null ? null : _mapper.Map<PowiazanieZleceniaDto>(entity);
        }
    }
}
