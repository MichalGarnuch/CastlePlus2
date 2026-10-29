# CastlePlus2 — przewodnik i audyt edukacyjny

**Stan przeglądu:** 29 października 2026 r.  
**Gałąź bazowa:** `master`  
**Analizowany commit:** `6a9b42d378ef6463313e6ef2fa961fb74cc44106`  
**Mapa wszystkich plików śledzonych przez Git:** [1](AUDYT_MAPA_01.md) · [2](AUDYT_MAPA_02.md) · [3](AUDYT_MAPA_03.md) · [4](AUDYT_MAPA_04.md) · [5](AUDYT_MAPA_05.md)

> To jest przegląd reprezentatywnych części projektu z inwentaryzacją wszystkich śledzonych plików, a nie pełna, ręczna weryfikacja każdego wiersza ani certyfikat bezpieczeństwa. Mapa jest automatyczna; wymienienie pliku nie oznacza, że został przetestowany lub dokładnie przeczytany. Wyniki odnoszą się do powyższego commita. Inne gałęzie mogą zawierać inne wersje.

## Co robi projekt

Rozbudowana druga wersja systemu do zarządzania nieruchomościami, najmem, finansami i dokumentami.

## Jak czytać kod i jak przepływają dane

[`CastlePlus2.Api/Program.cs`](CastlePlus2.Api/Program.cs) rejestruje bazę, Mediatora, walidatory i repozytoria. Kontrolery API przesyłają komendy do [`CastlePlus2.Application/`](CastlePlus2.Application); encje są w Domain, implementacje bazowe w Infrastructure, a DTO i żądania w Contracts. Klient ma własne widoki i usługi.

Podczas czytania zacznij od punktu wejścia, przejdź do funkcji odpowiadającej za żądanie lub działanie użytkownika, a potem do operacji na danych i wyniku. Mapa plików podaje ścieżki i automatycznie rozpoznane nazwy wybranych deklaracji.

## Co jest wartościowe w obecnym rozwiązaniu

[`CastlePlus2.Api/Controllers/Auth/AuthController.cs`](CastlePlus2.Api/Controllers/Auth/AuthController.cs) oddziela publiczne logowanie/odświeżenie tokenu od operacji wymagających roli administratora. Rejestracja walidatorów przez pipeline to czytelne miejsce na walidację wejścia.

## Ustalenia, luki i proponowany rozwój

- **Do sprawdzenia — rozmiar i uprawnienia:** bardzo rozbudowane `Program.cs` centralnie rejestruje wiele usług. Dzieląc rejestracje według modułów i sprawdzając polityki dla każdej operacji CRUD łatwiej ograniczyć regresje.

- **Do rozwoju:** README zawiera wyłącznie nazwę projektu. Brakuje w nim instrukcji uruchomienia zależności, konfiguracji, migracji oraz opisu testowych kont i procedury odtworzenia środowiska.

- **Zakres:** to ponad tysiąc plików; przejrzano architekturę i reprezentatywną ścieżkę auth, nie każdą regułę najmu i finansów.

## Sprawdzenia i ich granice

Nie uruchomiono builda ani testów .NET/SQL Server z powodu braku SDK i bazy. Nie stwierdzono gotowości produkcyjnej na podstawie samej struktury.

W kopii roboczej dostępne pliki Pythona sprawdzono dodatkowo parserem składni; sprawdzono też składnię odczytanych plików `package.json` i `composer.json`. Nie zastępuje to uruchomienia programu. Kod źródłowy tej gałęzi jest taki sam jak w commicie bazowym; dodane są wyłącznie dokumenty audytu i mapy.

## Jak zaplanować kolejne prace

Najpierw zajmij się punktami oznaczonymi jako krytyczne lub wysokie, następnie ustal, które uwagi wymagają testu na pełnym środowisku. Dla każdej zmiany przygotuj osobną gałąź, konkretny przypadek odtwarzający problem i test rezultatu. Nie traktuj propozycji jako potwierdzenia, że poprawka została już wdrożona.
