using System;
using System.Collections.Generic;
using System.Linq;
using CastlePlus2.Application.Interfaces.Reports;

namespace CastlePlus2.Application.Reports;

public sealed class ReportRegistry : IReportRegistry
{
    private readonly Dictionary<string, IReportDefinition> _definitions;

    public ReportRegistry(IEnumerable<IReportDefinition> definitions)
    {
        _definitions = definitions.ToDictionary(
            definition => definition.Key,
            definition => definition,
            StringComparer.OrdinalIgnoreCase);
    }

    public IReportDefinition GetByKey(string key)
    {
        if (!_definitions.TryGetValue(key, out var definition))
        {
            throw new ArgumentException($"Nieznany klucz raportu: {key}", nameof(key));
        }

        return definition;
    }

    public IReadOnlyList<IReportDefinition> GetAll() => _definitions.Values.ToList();
}