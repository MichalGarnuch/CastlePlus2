using CastlePlus2.Application.Interfaces.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.DeleteZasobUITekst;

public sealed class DeleteZasobUITekstCommandHandler : IRequestHandler<DeleteZasobUITekstCommand, bool>
{
    private readonly IZasobUITekstRepository _repo;

    public DeleteZasobUITekstCommandHandler(IZasobUITekstRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteZasobUITekstCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetForUpdateAsync(request.IdZasobuTekstu, cancellationToken);
        if (entity is null)
            return false;

        _repo.Remove(entity);
        await _repo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
