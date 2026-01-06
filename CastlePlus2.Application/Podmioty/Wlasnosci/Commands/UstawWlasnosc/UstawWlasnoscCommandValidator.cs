using FluentValidation;
using System;
using System.Globalization;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.UstawWlasnosc
{
    public class UstawWlasnoscCommandValidator : AbstractValidator<UstawWlasnoscCommand>
    {
        public UstawWlasnoscCommandValidator()
        {
            RuleFor(x => x.IdEncji)
                .NotEmpty();
            // Rule from SQL: podmioty.Wlasnosc.IdEncji uniqueidentifier NOT NULL

            RuleFor(x => x.Udzialy)
                .NotNull()
                .NotEmpty();
            // Rule (process input): lista udziałów wymagana

            RuleForEach(x => x.Udzialy).ChildRules(item =>
            {
                item.RuleFor(i => i.IdPodmiotu)
                    .GreaterThan(0);
                // Rule from SQL: podmioty.Wlasnosc.IdPodmiotu bigint NOT NULL

                item.RuleFor(i => i.UdzialProcent)
                    .GreaterThan(0m)
                    .LessThanOrEqualTo(100m)
                    .Must(v => HasPrecisionScale(v, precision: 7, scale: 4))
                    .WithMessage("UdzialProcent musi mieć maks. 4 miejsca po przecinku i mieścić się w formacie decimal(7,4).");
                // Rule from SQL: podmioty.Wlasnosc.UdzialProcent decimal(7,4) NOT NULL

                item.RuleFor(i => i.OdDnia)
                    .Must(d => d != default);
                // Rule from SQL: podmioty.Wlasnosc.OdDnia date NOT NULL

                item.RuleFor(i => i.DoDnia)
                    .Must((i, doDnia) => !doDnia.HasValue || doDnia.Value >= i.OdDnia);
                // Rule from SQL: podmioty.Wlasnosc.DoDnia date NULL
            });
        }

        private static bool HasPrecisionScale(decimal value, int precision, int scale)
        {
            // Normalizujemy znak
            value = Math.Abs(value);

            // InvariantCulture, bez separatorów tysięcy
            var s = value.ToString(CultureInfo.InvariantCulture);

            // decimal nie powinien iść w notację naukową, ale na wszelki wypadek:
            if (s.Contains('E') || s.Contains('e'))
                return false;

            var parts = s.Split('.');
            var intPart = parts[0].TrimStart('0');
            var fracPart = parts.Length > 1 ? parts[1].TrimEnd('0') : string.Empty;

            var intDigits = string.IsNullOrEmpty(intPart) ? 1 : intPart.Length; // "0" liczymy jako 1 cyfrę
            var fracDigits = fracPart.Length;

            if (fracDigits > scale)
                return false;

            return (intDigits + fracDigits) <= precision;
        }
    }
}
