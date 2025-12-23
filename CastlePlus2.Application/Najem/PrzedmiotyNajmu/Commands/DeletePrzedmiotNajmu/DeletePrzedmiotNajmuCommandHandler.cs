using CastlePlus2.Application.Interfaces.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.DeletePrzedmiotNajmu
{
    public class DeletePrzedmiotNajmuCommandHandler : IRequestHandler<DeletePrzedmiotNajmuCommand, bool>
    {
        private readonly IPrzedmiotNajmuRepository _repo;

        public DeletePrzedmiotNajmuCommandHandler(IPrzedmiotNajmuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeletePrzedmiotNajmuCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdPrzedmiotuNajmu, ct);
            if (entity == null)
            {
                return false;
            }

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}