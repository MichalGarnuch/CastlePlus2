using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.UpdateKaucja
{
    public class UpdateKaucjaCommand : IRequest<KaucjaDto?>
    {
        public long IdOperacjiKaucji { get; }
        public UpdateKaucjaRequest Request { get; }

        public UpdateKaucjaCommand(long idKaucji, UpdateKaucjaRequest request)
        {
            IdOperacjiKaucji = idKaucji;
            Request = request;
        }
    }
}