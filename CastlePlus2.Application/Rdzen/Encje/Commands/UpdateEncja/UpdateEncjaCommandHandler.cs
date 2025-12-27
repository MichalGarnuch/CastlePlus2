using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Commands.UpdateEncja
{
    public class UpdateEncjaCommandHandler : IRequestHandler<UpdateEncjaCommand, bool>
    {
        private readonly IEncjaRepository _repo;

        public UpdateEncjaCommandHandler(IEncjaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateEncjaCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.Id, ct);
            if (entity is null)
            {
                return false;
            }

            entity.TypEncji = request.TypEncji;
            entity.KodEncji = request.KodEncji;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}