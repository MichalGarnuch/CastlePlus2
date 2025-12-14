using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Queries.GetOdczytById
{
    public class GetOdczytByIdQueryHandler : IRequestHandler<GetOdczytByIdQuery, OdczytDto?>
    {
        private readonly IOdczytRepository _repo;
        private readonly IMapper _mapper;

        public GetOdczytByIdQueryHandler(IOdczytRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OdczytDto?> Handle(GetOdczytByIdQuery request, CancellationToken ct)
        {
            if (request.IdOdczytu <= 0)
                return null;

            var entity = await _repo.GetByIdAsync(request.IdOdczytu, ct);
            return entity is null ? null : _mapper.Map<OdczytDto>(entity);
        }
    }
}
