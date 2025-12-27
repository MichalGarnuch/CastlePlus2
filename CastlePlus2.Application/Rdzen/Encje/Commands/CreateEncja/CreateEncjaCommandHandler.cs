using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.CreateEncja
{
    /// <summary>
    /// Handler tworzący nową Encję w rdzeniu.
    /// </summary>
    public class CreateEncjaCommandHandler : IRequestHandler<CreateEncjaCommand, EncjaDto>
    {
        private readonly IEncjaRepository _repo;
        private readonly IMapper _mapper;

        public CreateEncjaCommandHandler(IEncjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EncjaDto> Handle(CreateEncjaCommand request, CancellationToken ct)
        {
            var entity = new Encja
            {
                Id = Guid.NewGuid(),
                TypEncji = request.TypEncji,
                KodEncji = request.KodEncji
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<EncjaDto>(entity);
        }
    }
}