using CastlePlus2.Application.Interfaces.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.UpdatePodmiot
{
    public class UpdatePodmiotCommandHandler : IRequestHandler<UpdatePodmiotCommand, bool>
    {
        private readonly IPodmiotRepository _repo;

        public UpdatePodmiotCommandHandler(IPodmiotRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdatePodmiotCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(cmd.IdPodmiotu, ct);
            if (entity is null)
                return false;

            entity.Nazwa = cmd.Nazwa;
            entity.NIP = cmd.NIP;
            entity.REGON = cmd.REGON;
            entity.PESEL = cmd.PESEL;
            entity.TypPodmiotu = cmd.TypPodmiotu;

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}