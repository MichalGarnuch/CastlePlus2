using CastlePlus2.Application.Interfaces.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.UpdateJednostkaMiary
{
    public class UpdateJednostkaMiaryCommandHandler : IRequestHandler<UpdateJednostkaMiaryCommand>
    {
        private readonly IJednostkaMiaryRepository _repo;

        public UpdateJednostkaMiaryCommandHandler(IJednostkaMiaryRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(UpdateJednostkaMiaryCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetByKodAsync(request.KodJednostki, ct);
            if (entity is null)
                throw new KeyNotFoundException($"JednostkaMiary '{request.KodJednostki}' nie istnieje.");

            entity.Nazwa = request.Nazwa;

            await _repo.SaveChangesAsync(ct);
        }
    }
}
