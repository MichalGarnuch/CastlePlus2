using AutoMapper;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.UpdateSkladnikCzynszu
{
    public class UpdateSkladnikCzynszuCommandHandler : IRequestHandler<UpdateSkladnikCzynszuCommand, SkladnikCzynszuDto?>
    {
        private readonly ISkladnikCzynszuRepository _repo;
        private readonly IMapper _mapper;

        public UpdateSkladnikCzynszuCommandHandler(ISkladnikCzynszuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SkladnikCzynszuDto?> Handle(UpdateSkladnikCzynszuCommand cmd, CancellationToken ct)
        {
            if (cmd.IdSkladnikaCzynszu <= 0)
            {
                return null;
            }

            var req = cmd.Request ?? throw new InvalidOperationException("Brak request body.");

            req.Nazwa = (req.Nazwa ?? string.Empty).Trim();
            req.KodJednostki = (req.KodJednostki ?? string.Empty).Trim();
            req.KodIndeksacji = string.IsNullOrWhiteSpace(req.KodIndeksacji) ? null : req.KodIndeksacji.Trim();

            if (req.IdUmowyNajmu == Guid.Empty)
                throw new InvalidOperationException("IdUmowyNajmu jest wymagane.");

            if (req.Nazwa.Length == 0)
                throw new InvalidOperationException("Nazwa jest wymagana.");

            if (req.Nazwa.Length > 100)
                throw new InvalidOperationException("Nazwa max 100 znaków.");

            if (req.KodJednostki.Length == 0)
                throw new InvalidOperationException("KodJednostki jest wymagany.");

            if (req.KodJednostki.Length > 20)
                throw new InvalidOperationException("KodJednostki max 20 znaków.");

            if (req.KodIndeksacji != null && req.KodIndeksacji.Length > 20)
                throw new InvalidOperationException("KodIndeksacji max 20 znaków.");

            if (req.OdDnia == default)
                throw new InvalidOperationException("OdDnia jest wymagane.");

            var entity = await _repo.GetByIdAsync(cmd.IdSkladnikaCzynszu, ct);
            if (entity is null)
            {
                return null;
            }

            entity.IdUmowyNajmu = req.IdUmowyNajmu;
            entity.Nazwa = req.Nazwa;
            entity.KodJednostki = req.KodJednostki;
            entity.Stawka = req.Stawka;
            entity.IloscBazowa = req.IloscBazowa;
            entity.KodIndeksacji = req.KodIndeksacji;
            entity.OdDnia = req.OdDnia;
            entity.DoDnia = req.DoDnia;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<SkladnikCzynszuDto>(entity);
        }
    }
}