using System;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.UpdateUmowaNajmu
{
    public sealed record UpdateUmowaNajmuCommand(Guid Id, UpdateUmowaNajmuRequest Request) : IRequest<UmowaNajmuDto?>;
}