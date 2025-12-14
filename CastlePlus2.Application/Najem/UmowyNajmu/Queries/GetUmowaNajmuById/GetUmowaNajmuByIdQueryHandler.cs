using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Queries.GetUmowaNajmuById
{
    public class GetUmowaNajmuByIdQueryHandler : IRequestHandler<GetUmowaNajmuByIdQuery, UmowaNajmuDto?>
    {
        private readonly IUmowaNajmuRepository _repo;
        private readonly IMapper _mapper;

        public GetUmowaNajmuByIdQueryHandler(IUmowaNajmuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UmowaNajmuDto?> Handle(GetUmowaNajmuByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id, ct);
            return entity == null ? null : _mapper.Map<UmowaNajmuDto>(entity);
        }
    }
}
