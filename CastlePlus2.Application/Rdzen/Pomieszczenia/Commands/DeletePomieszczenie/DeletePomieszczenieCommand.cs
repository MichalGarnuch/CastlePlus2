using System;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.DeletePomieszczenie
{
    public class DeletePomieszczenieCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
