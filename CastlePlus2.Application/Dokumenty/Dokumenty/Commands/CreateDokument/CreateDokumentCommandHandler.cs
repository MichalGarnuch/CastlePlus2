using AutoMapper;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Domain.Entities.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Commands.CreateDokument
{
    public class CreateDokumentCommandHandler
        : IRequestHandler<CreateDokumentCommand, DokumentDto>
    {
        private readonly IDokumentRepository _repo;
        private readonly IMapper _mapper;

        public CreateDokumentCommandHandler(
            IDokumentRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DokumentDto> Handle(
            CreateDokumentCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Nazwa))
                throw new InvalidOperationException("Nazwa dokumentu jest wymagana.");

            if (string.IsNullOrWhiteSpace(request.SciezkaPliku))
                throw new InvalidOperationException("SciezkaPliku jest wymagana.");

            var entity = new Dokument
            {
                IdEncjiOwner = request.IdEncjiOwner,
                Nazwa = request.Nazwa,
                Opis = request.Opis,
                SciezkaPliku = request.SciezkaPliku
                // DataUtworzenia – ustawiane przez SQL DEFAULT
            };

            await _repo.AddAsync(entity, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DokumentDto>(entity);
        }
    }
}
