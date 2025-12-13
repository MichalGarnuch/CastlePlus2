using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Queries.GetKategoriaKosztuById
{
    public class GetKategoriaKosztuByIdQueryHandler
        : IRequestHandler<GetKategoriaKosztuByIdQuery, KategoriaKosztuDto?>
    {
        private readonly IKategoriaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public GetKategoriaKosztuByIdQueryHandler(IKategoriaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<KategoriaKosztuDto?> Handle(GetKategoriaKosztuByIdQuery request, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(request.Id, ct);
            return entity is null ? null : _mapper.Map<KategoriaKosztuDto>(entity);
        }
    }
}
