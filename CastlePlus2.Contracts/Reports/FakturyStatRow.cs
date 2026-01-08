using System;

namespace CastlePlus2.Contracts.Reports;

public sealed record FakturyStatRow(
    string Numer,
    DateTime DataWystawienia,
    string? Kontrahent,
    string Waluta,
    decimal? Netto,
    decimal? Brutto);