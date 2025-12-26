using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Queries.GetPowiazaniaZlecenia
{
    public sealed class GetPowiazaniaZleceniaQueryHandler : IRequestHandler<GetPowiazaniaZleceniaQuery, List<PowiazanieZleceniaDto>>
    {
        private readonly IPowiazanieZleceniaRepository _repo;
        private readonly IMapper _mapper;

        public GetPowiazaniaZleceniaQueryHandler(IPowiazanieZleceniaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<PowiazanieZleceniaDto>> Handle(GetPowiazaniaZleceniaQuery request, CancellationToken ct)
        {
            var entities = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<PowiazanieZleceniaDto>>(entities);
        }
    }
}