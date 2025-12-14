using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetPrzedmiotNajmuById
{
    public class GetPrzedmiotNajmuByIdQueryHandler : IRequestHandler<GetPrzedmiotNajmuByIdQuery, PrzedmiotNajmuDto?>
    {
        private readonly IPrzedmiotNajmuRepository _repo;
        private readonly IMapper _mapper;

        public GetPrzedmiotNajmuByIdQueryHandler(IPrzedmiotNajmuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PrzedmiotNajmuDto?> Handle(GetPrzedmiotNajmuByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id, ct);
            return entity == null ? null : _mapper.Map<PrzedmiotNajmuDto>(entity);
        }
    }
}
