using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Domain.Entities.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Commands.CreateOdczyt
{
    public class CreateOdczytCommandHandler : IRequestHandler<CreateOdczytCommand, OdczytDto>
    {
        private readonly IOdczytRepository _repo;
        private readonly IMapper _mapper;

        public CreateOdczytCommandHandler(IOdczytRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OdczytDto> Handle(CreateOdczytCommand request, CancellationToken ct)
        {
            request.Zrodlo = request.Zrodlo?.Trim();

            if (request.IdLicznika <= 0)
                throw new InvalidOperationException("IdLicznika musi być > 0.");

            // date w SQL -> porządkujemy do .Date, żeby nie wpadł czas
            var data = request.DataOdczytu.Date;
            if (data == DateTime.MinValue.Date)
                throw new InvalidOperationException("DataOdczytu jest wymagana.");

            // Wskazanie: nie narzucam “musi rosnąć”, bo to już logika biznesowa (może być korekta).
            // Pilnujemy tylko, żeby nie było absurdów.
            if (request.Wskazanie < 0)
                throw new InvalidOperationException("Wskazanie nie może być ujemne.");

            if (request.Zrodlo is not null && request.Zrodlo.Length > 20)
                throw new InvalidOperationException("Zrodlo max 20 znaków.");

            // FK -> Licznik
            if (!await _repo.LicznikExistsAsync(request.IdLicznika, ct))
                throw new InvalidOperationException("Nie istnieje Licznik o podanym IdLicznika.");

            // UX -> (IdLicznika, DataOdczytu) unikalne
            if (await _repo.ExistsForLicznikAndDateAsync(request.IdLicznika, data, ct))
                throw new InvalidOperationException("Istnieje już odczyt dla tego licznika w tej dacie (unikalne: IdLicznika+DataOdczytu).");

            var entity = new Odczyt
            {
                IdLicznika = request.IdLicznika,
                DataOdczytu = data,
                Wskazanie = request.Wskazanie,
                Zrodlo = request.Zrodlo
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<OdczytDto>(entity);
        }
    }
}
