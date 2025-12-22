using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Contracts.Requests.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Commands.UpdatePrzylacze
{
    public sealed class UpdatePrzylaczeCommand : IRequest<PrzylaczeDto?>
    {
        public long IdPrzylacza { get; }
        public UpdatePrzylaczeRequest Request { get; }

        public UpdatePrzylaczeCommand(long idPrzylacza, UpdatePrzylaczeRequest request)
        {
            IdPrzylacza = idPrzylacza;
            Request = request;
        }
    }
}
