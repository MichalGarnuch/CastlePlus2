# Export Engine v2

## Wspierane formaty

| Format | Rozszerzenie | MIME |
| --- | --- | --- |
| CSV | `.csv` | `text/csv` |
| XLSX | `.xlsx` | `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet` |
| PDF | `.pdf` | `application/pdf` |
| DOCX | `.docx` | `application/vnd.openxmlformats-officedocument.wordprocessingml.document` |

## Zasady architektury

- **Application** zawiera tylko kontrakty/interfejsy (brak bibliotek eksportu).
- **Infrastructure** implementuje eksporty i zawiera wszystkie paczki NuGet dla CSV/XLSX/PDF/DOCX.
- **API** udostêpnia eksport przez `File(...)` i zwraca binarny plik z nag³ówkiem `Content-Disposition`.
- **Client** pobiera plik przez URL (nawigacja przez `IReportExportUrlService`).

## Gdzie zapisywany jest plik

- **Web**: plik jest pobierany przez przegl¹darkê do folderu pobrañ u¿ytkownika (nag³ówek `Content-Disposition`).
- **API**: domyœlnie nic nie zapisuje na dysk (stateless).

## NuGety i licencje

- CsvHelper `33.0.1` — CSV (MIT).
- ClosedXML `0.104.2` — XLSX (MIT).
- DocumentFormat.OpenXml `3.0.2` — DOCX (MIT).
- PdfSharpCore `1.3.67` — PDF rendering (MIT).
- MigraDocCore.DocumentObjectModel `1.3.67` — dokumenty PDF (MIT).
- MigraDocCore.Rendering `1.3.67` — renderowanie PDF (MIT).