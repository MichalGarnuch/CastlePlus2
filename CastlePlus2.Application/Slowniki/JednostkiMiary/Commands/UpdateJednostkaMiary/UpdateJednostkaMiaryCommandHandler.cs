using CastlePlus2.Application.Interfaces.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.UpdateJednostkaMiary
{
    public sealed class UpdateJednostkaMiaryCommandHandler : IRequestHandler<UpdateJednostkaMiaryCommand, bool>
    {
        private readonly IJednostkaMiaryRepository _repo;

        public UpdateJednostkaMiaryCommandHandler(IJednostkaMiaryRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateJednostkaMiaryCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByKodAsync(request.KodJednostki, ct);
            if (entity is null) return false;

            entity.Nazwa = request.Nazwa;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}