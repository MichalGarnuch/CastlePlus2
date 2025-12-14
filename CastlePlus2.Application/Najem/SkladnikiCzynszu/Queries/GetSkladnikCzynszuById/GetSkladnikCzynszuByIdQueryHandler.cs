using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Queries.GetSkladnikCzynszuById
{
    public class GetSkladnikCzynszuByIdQueryHandler : IRequestHandler<GetSkladnikCzynszuByIdQuery, SkladnikCzynszuDto?>
    {
        private readonly ISkladnikCzynszuRepository _repo;
        private readonly IMapper _mapper;

        public GetSkladnikCzynszuByIdQueryHandler(ISkladnikCzynszuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SkladnikCzynszuDto?> Handle(GetSkladnikCzynszuByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id, ct);
            return entity == null ? null : _mapper.Map<SkladnikCzynszuDto>(entity);
        }
    }
}
