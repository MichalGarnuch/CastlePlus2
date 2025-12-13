using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.CreateKategoriaKosztu
{
    public class CreateKategoriaKosztuCommandHandler
        : IRequestHandler<CreateKategoriaKosztuCommand, KategoriaKosztuDto>
    {
        private readonly IKategoriaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public CreateKategoriaKosztuCommandHandler(IKategoriaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<KategoriaKosztuDto> Handle(CreateKategoriaKosztuCommand request, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(request.Kod))
                throw new InvalidOperationException("Kod jest wymagany.");

            if (request.Kod.Length > 20)
                throw new InvalidOperationException("Kod może mieć maksymalnie 20 znaków.");

            if (string.IsNullOrWhiteSpace(request.Nazwa))
                throw new InvalidOperationException("Nazwa jest wymagana.");

            if (request.Nazwa.Length > 100)
                throw new InvalidOperationException("Nazwa może mieć maksymalnie 100 znaków.");

            var exists = await _repo.ExistsByKodAsync(request.Kod, ct);
            if (exists)
                throw new InvalidOperationException("Kategoria o takim Kod już istnieje (unikalny indeks).");

            var entity = new KategoriaKosztu
            {
                Kod = request.Kod,
                Nazwa = request.Nazwa
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<KategoriaKosztuDto>(entity);
        }
    }
}
