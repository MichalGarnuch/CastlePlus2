using CastlePlus2.Application.Interfaces.Finanse;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CastlePlus2.Infrastructure.Services.Exports
{
    public class FakturaDocxTemplateRenderer : IFakturaDocxTemplateRenderer
    {
        public byte[] Render(
            byte[] templateBytes,
            IReadOnlyDictionary<string, string> placeholders,
            IReadOnlyList<IReadOnlyDictionary<string, string>> itemRows)
        {
            using var stream = new MemoryStream();
            stream.Write(templateBytes, 0, templateBytes.Length);

            using (var document = WordprocessingDocument.Open(stream, true))
            {
                var body = document.MainDocumentPart?.Document.Body
                    ?? throw new InvalidOperationException("Brak Body w szablonie DOCX.");

                ReplaceTextPlaceholders(body, placeholders);
                ReplaceTableRows(body, itemRows);
                document.MainDocumentPart!.Document.Save();
            }

            return stream.ToArray();
        }

        private static void ReplaceTextPlaceholders(Body body, IReadOnlyDictionary<string, string> placeholders)
        {
            foreach (var text in body.Descendants<Text>())
            {
                if (string.IsNullOrEmpty(text.Text))
                    continue;

                foreach (var (key, value) in placeholders)
                {
                    var token = WrapToken(key);
                    if (text.Text.Contains(token, StringComparison.Ordinal))
                        text.Text = text.Text.Replace(token, value ?? string.Empty, StringComparison.Ordinal);
                }
            }
        }

        private static void ReplaceTableRows(Body body, IReadOnlyList<IReadOnlyDictionary<string, string>> itemRows)
        {
            var keys = itemRows.SelectMany(x => x.Keys).Distinct().ToList();
            if (keys.Count == 0)
                return;

            foreach (var table in body.Descendants<Table>())
            {
                var templateRow = table.Elements<TableRow>()
                    .FirstOrDefault(row => ContainsAnyToken(row, keys));

                if (templateRow is null)
                    continue;

                foreach (var rowData in itemRows)
                {
                    var clone = (TableRow)templateRow.CloneNode(true);
                    foreach (var text in clone.Descendants<Text>())
                    {
                        foreach (var (key, value) in rowData)
                        {
                            var token = WrapToken(key);
                            if (text.Text.Contains(token, StringComparison.Ordinal))
                            {
                                text.Text = text.Text.Replace(token, value ?? string.Empty, StringComparison.Ordinal);
                            }
                        }
                    }

                    table.InsertBefore(clone, templateRow);
                }

                templateRow.Remove();
                break;
            }
        }

        private static bool ContainsAnyToken(TableRow row, IReadOnlyList<string> keys)
        {
            var rowText = string.Join("", row.Descendants<Text>().Select(x => x.Text));
            return keys.Any(key => rowText.Contains(WrapToken(key), StringComparison.Ordinal));
        }

        private static string WrapToken(string key) => $"{{{{{key}}}}}";
    }
}