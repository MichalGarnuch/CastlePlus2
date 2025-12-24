using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.UpdateSkladnikCzynszu
{
    public class UpdateSkladnikCzynszuCommand : IRequest<SkladnikCzynszuDto?>
    {
        public long IdSkladnikaCzynszu { get; }
        public UpdateSkladnikCzynszuRequest Request { get; }

        public UpdateSkladnikCzynszuCommand(long idSkladnikaCzynszu, UpdateSkladnikCzynszuRequest request)
        {
            IdSkladnikaCzynszu = idSkladnikaCzynszu;
            Request = request;
        }
    }
}