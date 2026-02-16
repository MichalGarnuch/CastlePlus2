using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Queries.GetFakturaWydrukTemplates
{
    public class GetFakturaWydrukTemplatesQuery : IRequest<List<FakturaWydrukTemplateDto>>
    {
    }
}