using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Queries.GetZleceniePracyById
{
    public class GetZleceniePracyByIdQueryHandler : IRequestHandler<GetZleceniePracyByIdQuery, ZleceniePracyDto?>
    {
        private readonly IZleceniePracyRepository _repository;
        private readonly IMapper _mapper;

        public GetZleceniePracyByIdQueryHandler(IZleceniePracyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ZleceniePracyDto?> Handle(GetZleceniePracyByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.IdZlecenia, cancellationToken);
            return entity is null ? null : _mapper.Map<ZleceniePracyDto>(entity);
        }
    }
}
