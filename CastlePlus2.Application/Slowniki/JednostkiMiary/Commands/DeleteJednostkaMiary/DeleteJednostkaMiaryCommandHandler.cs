using CastlePlus2.Application.Interfaces.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.DeleteJednostkaMiary
{
    public class DeleteJednostkaMiaryCommandHandler : IRequestHandler<DeleteJednostkaMiaryCommand>
    {
        private readonly IJednostkaMiaryRepository _repo;

        public DeleteJednostkaMiaryCommandHandler(IJednostkaMiaryRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteJednostkaMiaryCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByKodAsync(request.KodJednostki, ct);
            if (entity is null)
                return; // idempotentny DELETE

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
        }
    }
}
