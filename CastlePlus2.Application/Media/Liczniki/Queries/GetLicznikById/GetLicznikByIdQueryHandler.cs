using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Queries.GetLicznikById
{
    public class GetLicznikByIdQueryHandler : IRequestHandler<GetLicznikByIdQuery, LicznikDto?>
    {
        private readonly ILicznikRepository _repo;
        private readonly IMapper _mapper;

        public GetLicznikByIdQueryHandler(ILicznikRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LicznikDto?> Handle(GetLicznikByIdQuery request, CancellationToken ct)
        {
            if (request.IdLicznika <= 0)
                return null;

            var entity = await _repo.GetByIdAsync(request.IdLicznika, ct);
            return entity is null ? null : _mapper.Map<LicznikDto>(entity);
        }
    }
}
