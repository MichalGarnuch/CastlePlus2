using MediatR;

namespace CastlePlus2.Application.Dokumenty.Dokumenty.Commands.DeleteDokument
{
    public record DeleteDokumentCommand(long IdDokumentu) : IRequest<bool>;
}
