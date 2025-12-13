using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Queries.GetPowiazanieDokumentuById
{
    public class GetPowiazanieDokumentuByIdQueryHandler
        : IRequestHandler<GetPowiazanieDokumentuByIdQuery, PowiazanieDokumentuDto?>
    {
        private readonly IPowiazanieDokumentuRepository _repo;
        private readonly IMapper _mapper;

        public GetPowiazanieDokumentuByIdQueryHandler(IPowiazanieDokumentuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PowiazanieDokumentuDto?> Handle(GetPowiazanieDokumentuByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.IdPowiazania, ct);
            return entity is null ? null : _mapper.Map<PowiazanieDokumentuDto>(entity);
        }
    }
}
