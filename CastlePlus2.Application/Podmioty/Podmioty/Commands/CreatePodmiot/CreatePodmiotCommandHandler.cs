using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Domain.Entities.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.CreatePodmiot
{
    public class CreatePodmiotCommandHandler : IRequestHandler<CreatePodmiotCommand, PodmiotDto>
    {
        private readonly IPodmiotRepository _repo;
        private readonly IMapper _mapper;

        public CreatePodmiotCommandHandler(IPodmiotRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PodmiotDto> Handle(CreatePodmiotCommand request, CancellationToken ct)
        {
            var entity = new Podmiot
            {
                Nazwa = request.Nazwa,
                NIP = request.NIP,
                REGON = request.REGON,
                PESEL = request.PESEL,
                TypPodmiotu = request.TypPodmiotu
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<PodmiotDto>(entity);
        }
    }
}
