using System;

namespace CastlePlus2.Contracts.Reports;

public sealed record PodsumowanieOperacyjneRow(
    DateTime GeneratedAt,
    int LiczbaNieruchomosci,
    int LiczbaBudynkow,
    int LiczbaLokali,
    int LiczbaPodmiotow,
    int LiczbaUmowNajmu);