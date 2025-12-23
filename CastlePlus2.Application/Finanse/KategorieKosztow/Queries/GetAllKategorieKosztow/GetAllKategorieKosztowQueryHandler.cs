using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Queries.GetAllKategorieKosztow
{
    public class GetAllKategorieKosztowQueryHandler : IRequestHandler<GetAllKategorieKosztowQuery, List<KategoriaKosztuDto>>
    {
        private readonly IKategoriaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public GetAllKategorieKosztowQueryHandler(IKategoriaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<KategoriaKosztuDto>> Handle(GetAllKategorieKosztowQuery request, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list.Select(x => _mapper.Map<KategoriaKosztuDto>(x)).ToList();
        }
    }
}
