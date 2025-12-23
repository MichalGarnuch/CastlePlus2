using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Commands.UpdateFaktura
{
    public class UpdateFakturaCommandHandler : IRequestHandler<UpdateFakturaCommand, FakturaDto?>
    {
        private readonly IFakturaRepository _repo;
        private readonly IMapper _mapper;

        public UpdateFakturaCommandHandler(IFakturaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<FakturaDto?> Handle(UpdateFakturaCommand cmd, CancellationToken ct)
        {
            if (cmd.IdFaktury <= 0)
                return null;

            var req = cmd.Request ?? throw new InvalidOperationException("Brak request body.");

            req.NumerFaktury = (req.NumerFaktury ?? string.Empty).Trim();
            req.KodWaluty = (req.KodWaluty ?? string.Empty).Trim().ToUpperInvariant();

            if (req.NumerFaktury.Length == 0)
                throw new InvalidOperationException("NumerFaktury jest wymagany.");

            if (req.NumerFaktury.Length > 60)
                throw new InvalidOperationException("NumerFaktury max 60 znaków.");

            if (req.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (req.DataWystawienia == default)
                throw new InvalidOperationException("DataWystawienia jest wymagana.");

            if (req.KodWaluty.Length != 3)
                throw new InvalidOperationException("KodWaluty musi mieć dokładnie 3 znaki.");

            // UNIQUE: NumerFaktury
            if (await _repo.ExistsByNumerAsync(req.NumerFaktury, cmd.IdFaktury, ct))
                throw new InvalidOperationException("Istnieje już faktura o podanym NumerFaktury.");

            var entity = await _repo.GetForUpdateAsync(cmd.IdFaktury, ct);
            if (entity is null)
                return null;

            entity.NumerFaktury = req.NumerFaktury;
            entity.IdPodmiotu = req.IdPodmiotu;
            entity.DataWystawienia = req.DataWystawienia.Date;
            entity.DataSprzedazy = req.DataSprzedazy?.Date;
            entity.KodWaluty = req.KodWaluty;
            entity.KwotaNetto = req.KwotaNetto;
            entity.KwotaBrutto = req.KwotaBrutto;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<FakturaDto>(entity);
        }
    }
}
