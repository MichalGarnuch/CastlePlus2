using System.Collections.Generic;

namespace CastlePlus2.Application.Interfaces.Reports;

public interface IReportRegistry
{
    IReportDefinition GetByKey(string key);
    IReadOnlyList<IReportDefinition> GetAll();
}