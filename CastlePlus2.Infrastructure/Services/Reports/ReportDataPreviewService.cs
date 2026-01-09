using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Contracts.Reports;
using CastlePlus2.Infrastructure.Services.Exports;
using CastlePlus2.Infrastructure.Services.Reports.Definitions;

namespace CastlePlus2.Infrastructure.Services.Reports;

public sealed class ReportDataPreviewService : IReportDataPreviewService
{
    private static readonly CultureInfo LabelCulture = CultureInfo.GetCultureInfo("pl-PL");

    // jeżeli chcesz summary tylko dla "podsumowanie" – trzymaj to po kluczu, a nie po typie,
    // bo typy definicji są szczegółem Infrastructure
    private const string PodsumowanieKey = "podsumowanie";

    private static readonly string[] PodsumowanieSummaryKeys =
    {
        "GeneratedAt",
        "LiczbaNieruchomosci",
        "LiczbaBudynkow",
        "LiczbaLokali",
        "LiczbaPodmiotow",
        "LiczbaUmowNajmu"
    };

    private readonly IReportRegistry _reportRegistry;

    public ReportDataPreviewService(IReportRegistry reportRegistry)
    {
        _reportRegistry = reportRegistry;
    }

    public async Task<ReportDataPreviewResponse> BuildAsync(string reportKey, int take, CancellationToken ct)
    {
        var definition = _reportRegistry.GetByKey(reportKey);
        var generatedAtUtc = DateTime.UtcNow;

        // budowanie wierszy (faktury mają "take", reszta zwykle nie)
        var rowsObjects = definition is FakturyReportDefinition fakturyDefinition
            ? await fakturyDefinition.BuildRowsAsync(take, ct)
            : await definition.BuildRowsAsync(ct);

        var properties = GetExportableProperties(definition.RowType);
        var columns = properties.Select(GetColumnLabel).ToList();
        var rows = BuildRows(rowsObjects, properties);
        var summary = BuildSummary(definition, rowsObjects, properties);

        return new ReportDataPreviewResponse(
            definition.Key,
            definition.Title,
            generatedAtUtc,
            columns,
            rows,
            summary);
    }

    private static IReadOnlyList<IReadOnlyList<string>> BuildRows(
        IReadOnlyList<object> rowsObjects,
        IReadOnlyList<PropertyInfo> properties)
    {
        var rows = new List<IReadOnlyList<string>>(rowsObjects.Count);

        foreach (var row in rowsObjects)
        {
            var cells = properties
                .Select(p => ExportCommon.FormatValue(p.GetValue(row)))
                .ToList();

            rows.Add(cells);
        }

        return rows;
    }

    private static IReadOnlyDictionary<string, string>? BuildSummary(
        IReportDefinition definition,
        IReadOnlyList<object> rowsObjects,
        IReadOnlyList<PropertyInfo> properties)
    {
        if (!string.Equals(definition.Key, PodsumowanieKey, StringComparison.OrdinalIgnoreCase))
            return null;

        var row = rowsObjects.FirstOrDefault();
        if (row is null)
            return new Dictionary<string, string>();

        var propertyLookup = properties.ToDictionary(p => p.Name, StringComparer.Ordinal);
        var summary = new Dictionary<string, string>();

        foreach (var propertyName in PodsumowanieSummaryKeys)
        {
            if (!propertyLookup.TryGetValue(propertyName, out var prop))
                continue;

            var label = GetColumnLabel(prop);
            summary[label] = ExportCommon.FormatValue(prop.GetValue(row));
        }

        return summary;
    }

    private static IReadOnlyList<PropertyInfo> GetExportableProperties(Type rowType)
    {
        // ExportCommon.GetExportableProperties<T>() jest internal w Infrastructure,
        // więc w tej samej assembly możesz go spokojnie wołać refleksją.
        var method = typeof(ExportCommon).GetMethod(
            "GetExportableProperties",
            BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

        if (method is null)
            throw new InvalidOperationException("Brak metody ExportCommon.GetExportableProperties.");

        var generic = method.MakeGenericMethod(rowType);
        return (PropertyInfo[])generic.Invoke(null, null)!;
    }

    private static string GetColumnLabel(PropertyInfo property)
    {
        var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
        if (!string.IsNullOrWhiteSpace(displayAttribute?.GetName()))
            return displayAttribute.GetName()!;

        var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>();
        if (!string.IsNullOrWhiteSpace(displayNameAttribute?.DisplayName))
            return displayNameAttribute.DisplayName;

        return HumanizePropertyName(property.Name);
    }

    private static string HumanizePropertyName(string name)
    {
        var withSpaces = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
        var words = withSpaces.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (words.Length <= 1)
            return withSpaces;

        for (var i = 1; i < words.Length; i++)
            words[i] = LabelCulture.TextInfo.ToLower(words[i]);

        return string.Join(' ', words);
    }
}
