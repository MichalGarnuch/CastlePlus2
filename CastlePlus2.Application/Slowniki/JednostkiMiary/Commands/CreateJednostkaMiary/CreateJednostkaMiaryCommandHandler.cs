using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Domain.Entities.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.CreateJednostkaMiary
{
    public class CreateJednostkaMiaryCommandHandler : IRequestHandler<CreateJednostkaMiaryCommand, JednostkaMiaryDto>
    {
        private readonly IJednostkaMiaryRepository _repo;
        private readonly IMapper _mapper;

        public CreateJednostkaMiaryCommandHandler(IJednostkaMiaryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<JednostkaMiaryDto> Handle(CreateJednostkaMiaryCommand request, CancellationToken ct)
        {
            var existing = await _repo.GetByKodAsync(request.KodJednostki, ct);
            if (existing != null)
                return _mapper.Map<JednostkaMiaryDto>(existing);

            var entity = new JednostkaMiary
            {
                KodJednostki = request.KodJednostki,
                Nazwa = request.Nazwa
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<JednostkaMiaryDto>(entity);
        }
    }
}
