using AutoMapper;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Queries.GetAllZleceniaPracy
{
    public class GetAllZleceniaPracyQueryHandler : IRequestHandler<GetAllZleceniaPracyQuery, List<ZleceniePracyDto>>
    {
        private readonly IZleceniePracyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllZleceniaPracyQueryHandler(IZleceniePracyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ZleceniePracyDto>> Handle(GetAllZleceniaPracyQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync(cancellationToken);
            return list.Select(x => _mapper.Map<ZleceniePracyDto>(x)).ToList();
        }
    }
}