using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Queries.GetAllPomieszczenia
{
    public class GetAllPomieszczeniaQueryHandler
        : IRequestHandler<GetAllPomieszczeniaQuery, List<PomieszczenieDto>>
    {
        private readonly IPomieszczenieRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPomieszczeniaQueryHandler(IPomieszczenieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PomieszczenieDto>> Handle(GetAllPomieszczeniaQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync(cancellationToken);
            return list.Select(_mapper.Map<PomieszczenieDto>).ToList();
        }
    }
}
