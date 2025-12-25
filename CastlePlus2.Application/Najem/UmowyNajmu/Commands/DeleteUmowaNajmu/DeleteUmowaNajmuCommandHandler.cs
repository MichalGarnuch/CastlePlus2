using CastlePlus2.Application.Interfaces.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.DeleteUmowaNajmu
{
    public sealed class DeleteUmowaNajmuCommandHandler : IRequestHandler<DeleteUmowaNajmuCommand, bool>
    {
        private readonly IUmowaNajmuRepository _repo;

        public DeleteUmowaNajmuCommandHandler(IUmowaNajmuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteUmowaNajmuCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}