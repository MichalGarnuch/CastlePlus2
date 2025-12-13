using AutoMapper;
using CastlePlus2.Application.Interfaces;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Queries.GetPrzypisanieAdresuById
{
    public sealed class GetPrzypisanieAdresuByIdQueryHandler
        : IRequestHandler<GetPrzypisanieAdresuByIdQuery, PrzypisanieAdresuDto?>
    {
        private readonly IPrzypisanieAdresuRepository _repo;
        private readonly IMapper _mapper;

        public GetPrzypisanieAdresuByIdQueryHandler(IPrzypisanieAdresuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PrzypisanieAdresuDto?> Handle(GetPrzypisanieAdresuByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            return entity == null ? null : _mapper.Map<PrzypisanieAdresuDto>(entity);
        }
    }
}
