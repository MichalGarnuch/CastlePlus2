using AutoMapper;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Queries.GetDokumentById
{
    public class GetDokumentByIdQueryHandler
        : IRequestHandler<GetDokumentByIdQuery, DokumentDto?>
    {
        private readonly IDokumentRepository _repo;
        private readonly IMapper _mapper;

        public GetDokumentByIdQueryHandler(
            IDokumentRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DokumentDto?> Handle(
            GetDokumentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            return entity is null ? null : _mapper.Map<DokumentDto>(entity);
        }
    }
}
