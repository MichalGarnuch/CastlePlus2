using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Queries.GetKontaktById
{
    public class GetKontaktByIdQueryHandler : IRequestHandler<GetKontaktByIdQuery, KontaktDto?>
    {
        private readonly IKontaktRepository _repo;
        private readonly IMapper _mapper;

        public GetKontaktByIdQueryHandler(IKontaktRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<KontaktDto?> Handle(GetKontaktByIdQuery request, CancellationToken ct)
        {
            if (request.IdKontaktu <= 0)
                return null;

            var entity = await _repo.GetByIdAsync(request.IdKontaktu, ct);
            return entity is null ? null : _mapper.Map<KontaktDto>(entity);
        }
    }
}
