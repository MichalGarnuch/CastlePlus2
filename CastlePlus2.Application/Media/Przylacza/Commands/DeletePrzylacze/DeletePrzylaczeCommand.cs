using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Commands.DeletePrzylacze
{
    public sealed class DeletePrzylaczeCommand : IRequest<bool>
    {
        public long IdPrzylacza { get; }

        public DeletePrzylaczeCommand(long idPrzylacza)
        {
            IdPrzylacza = idPrzylacza;
        }
    }
}
