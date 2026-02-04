using FluentValidation;
using System.Linq;

namespace CastlePlus2.Application.Podmioty.Podmioty.Commands.UpdatePodmiot
{
    public sealed class UpdatePodmiotCommandValidator : AbstractValidator<UpdatePodmiotCommand>
    {
        public UpdatePodmiotCommandValidator()
        {
            // Rule from SQL: podmioty.Podmiot.IdPodmiotu bigint NOT NULL
            RuleFor(x => x.IdPodmiotu)
                .NotEmpty();

            // Rule from SQL: podmioty.Podmiot.Nazwa nvarchar(200) NOT NULL
            RuleFor(x => x.Nazwa)
                .NotEmpty()
                .MaximumLength(200);

            // Rule from SQL: podmioty.Podmiot.NIP nvarchar(20) NULL
            RuleFor(x => x.NIP)
                .MaximumLength(20)
                .Must(BeValidNip)
                .When(x => !string.IsNullOrWhiteSpace(x.NIP))
                .WithMessage("NIP musi mieć 10 cyfr.");

            // Rule from SQL: podmioty.Podmiot.REGON nvarchar(20) NULL
            RuleFor(x => x.REGON)
                .MaximumLength(20)
                .Must(BeValidRegon)
                .When(x => !string.IsNullOrWhiteSpace(x.REGON))
                .WithMessage("REGON musi mieć 9 lub 14 cyfr.");

            // Rule from SQL: podmioty.Podmiot.PESEL nvarchar(11) NULL
            RuleFor(x => x.PESEL)
                .MaximumLength(11)
                .Must(BeValidPesel)
                .When(x => !string.IsNullOrWhiteSpace(x.PESEL))
                .WithMessage("PESEL musi mieć 11 cyfr.");

            // Rule from SQL: podmioty.Podmiot.TypPodmiotu nvarchar(30) NOT NULL
            RuleFor(x => x.TypPodmiotu)
                .NotEmpty()
                .MaximumLength(30);
        }

        private static bool BeValidNip(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return true;
            var normalized = NormalizeDigits(value);
            return normalized.Length == 10 && normalized.All(char.IsDigit);
        }

        private static bool BeValidRegon(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return true;
            var normalized = NormalizeDigits(value);
            return (normalized.Length == 9 || normalized.Length == 14) && normalized.All(char.IsDigit);
        }

        private static bool BeValidPesel(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return true;
            var normalized = NormalizeDigits(value);
            return normalized.Length == 11 && normalized.All(char.IsDigit);
        }

        private static string NormalizeDigits(string value)
        {
            return new string(value.Where(c => c != ' ' && c != '-').ToArray());
        }
    }
}