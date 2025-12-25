using System;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.DeleteUmowaNajmu
{
    public sealed record DeleteUmowaNajmuCommand(Guid Id) : IRequest<bool>;
}