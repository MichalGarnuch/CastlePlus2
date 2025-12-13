using System;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.CreatePowiazanieZlecenia
{
    public class CreatePowiazanieZleceniaCommand : IRequest<PowiazanieZleceniaDto>
    {
        public long IdZlecenia { get; set; }
        public Guid IdEncji { get; set; }
    }
}
