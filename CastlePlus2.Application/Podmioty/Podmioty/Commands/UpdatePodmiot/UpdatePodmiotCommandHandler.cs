using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.UpdatePodmiot
{
    public class UpdatePodmiotCommandHandler : IRequestHandler<UpdatePodmiotCommand, PodmiotDto?>
    {
        private readonly IPodmiotRepository _repo;
        private readonly IMapper _mapper;

        public UpdatePodmiotCommandHandler(IPodmiotRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PodmiotDto?> Handle(UpdatePodmiotCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(cmd.IdPodmiotu, ct);
            if (entity is null)
                return null;

            var r = cmd.Request;

            entity.Nazwa = r.Nazwa;
            entity.NIP = r.NIP;
            entity.REGON = r.REGON;
            entity.PESEL = r.PESEL;
            entity.TypPodmiotu = r.TypPodmiotu;

            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<PodmiotDto>(entity);
        }
    }
}
