using CastlePlus2.Application.Interfaces.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.DeleteJednostkaMiary
{
    public sealed class DeleteJednostkaMiaryCommandHandler : IRequestHandler<DeleteJednostkaMiaryCommand, bool>
    {
        private readonly IJednostkaMiaryRepository _repo;

        public DeleteJednostkaMiaryCommandHandler(IJednostkaMiaryRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteJednostkaMiaryCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByKodAsync(request.KodJednostki, ct);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}