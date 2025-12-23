using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.UpdateEncja
{
    public class UpdateEncjaCommandHandler : IRequestHandler<UpdateEncjaCommand, EncjaDto?>
    {
        private readonly IEncjaRepository _repo;
        private readonly IMapper _mapper;

        public UpdateEncjaCommandHandler(IEncjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EncjaDto?> Handle(UpdateEncjaCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.Id, ct);
            if (entity is null)
            {
                return null;
            }

            entity.TypEncji = request.Request.TypEncji;
            entity.KodEncji = request.Request.KodEncji;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<EncjaDto>(entity);
        }
    }
}