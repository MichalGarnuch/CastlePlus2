using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Nieruchomosci.Commands.CreateNieruchomosc
{
    /// <summary>
    /// Handler obsługujący komendę CreateNieruchomoscCommand.
    /// 
    /// Zadania handlera:
    /// 1. Utworzyć nową encję Nieruchomosc na podstawie danych z komendy.
    /// 2. Wygenerować nowy GUID dla Id (jeśli nie zrobi tego baza).
    /// 3. Zapisać encję w repozytorium (które używa EF Core).
    /// 4. Zwrócić Id nowo utworzonej nieruchomości.
    /// 
    /// Handler NIE:
    /// - nie zna szczegółów EF Core – używa tylko repozytorium.
    /// - nie kontaktuje się z kontrolerem ani UI – to robi MediatR w Api.
    /// </summary>
    public class CreateNieruchomoscCommandHandler : IRequestHandler<CreateNieruchomoscCommand, Guid>
    {
        private readonly INieruchomoscRepository _nieruchomoscRepository;

        /// <summary>
        /// Handler przyjmuje repozytorium przez DI.
        /// </summary>
        /// <param name="nieruchomoscRepository">Abstrakcja dostępu do danych nieruchomości.</param>
        public CreateNieruchomoscCommandHandler(INieruchomoscRepository nieruchomoscRepository)
        {
            _nieruchomoscRepository = nieruchomoscRepository;
        }

        /// <summary>
        /// Główna logika komendy.
        /// </summary>
        /// <param name="request">Dane wejściowe komendy (Nazwa, Id adresu, Id właściciela).</param>
        /// <param name="cancellationToken">Token anulowania dla operacji async.</param>
        /// <returns>Id nowo utworzonej nieruchomości.</returns>
        public async Task<Guid> Handle(CreateNieruchomoscCommand request, CancellationToken cancellationToken)
        {
            // 1. Walidacja "ręczna" – później przeniesiemy do FluentValidation.
            if (string.IsNullOrWhiteSpace(request.Nazwa))
            {
                // W prawdziwej aplikacji rzucilibyśmy tutaj customowy wyjątek domenowy
                // lub użyli FluentValidation (np. CreateNieruchomoscCommandValidator).
                throw new ArgumentException("Nazwa nieruchomości jest wymagana.");
            }

            // 2. Utworzenie nowej encji domenowej.
            var entity = new Nieruchomosc
            {
                // Id generujemy tutaj – jeżeli w bazie jest DEFAULT NEWID(), można to pominąć,
                // ale generowanie po stronie aplikacji daje większą kontrolę.
                Id = Guid.NewGuid(),
                Nazwa = request.Nazwa,
                IdAdresuGlownego = request.IdAdresuGlownego,
                IdPodmiotuWlasciciela = request.IdPodmiotuWlasciciela,
                Geometria = null // Na razie brak obsługi geometrii w Command – dodamy później.
            };

            // 3. Dodanie do repozytorium (jeszcze bez zapisu).
            await _nieruchomoscRepository.AddAsync(entity, cancellationToken);

            // 4. Zapis zmian w bazie.
            await _nieruchomoscRepository.SaveChangesAsync(cancellationToken);

            // 5. Zwrócenie Id nowo utworzonej nieruchomości.
            return entity.Id;
        }
    }
}

