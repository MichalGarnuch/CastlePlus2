using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.DeletePowiazanieDokumentu
{
    public record DeletePowiazanieDokumentuCommand(long IdPowiazania) : IRequest<bool>;
}
