# CastlePlus2: kontrolowany koszt i odporna weryfikacja haseł

Branch: `wersja-inzynierska-2026`. Stan bazowy: `master` / `6a9b42d378ef6463313e6ef2fa961fb74cc44106`. Data wykonania: 29.10.2026.

To autorski wariant rozwojowy konkretnego fragmentu projektu. Branch zachowuje pełne drzewo kodu z wersji bazowej i zawiera opisane poniżej zmiany. [Wcześniejszy audyt](https://github.com/MichalGarnuch/CastlePlus2/tree/audyt-edukacyjny-2026) jest na osobnym branchu. Nie wykonywano merge ani wdrożenia.

## Dlaczego parser hasha jest częścią bezpieczeństwa

Hash hasła to nie tylko ciąg do porównania. Zapis zawiera algorytm, sól i liczbę iteracji, które decydują o koszcie obliczeń. Nieograniczona wartość iteracji albo niepoprawny Base64 może powodować nadmierną pracę lub wyjątek zamiast zwykłej odmowy logowania.

W `PasswordHashService` nowe hasła używają 600000 iteracji PBKDF2. Weryfikator nadal obsługuje starsze, poprawnie zapisane hashe: parametry odczytuje z ich formatu. Nie ma resetu kont ani zmiany istniejących rekordów bazy.

## Kontrakt wejścia

Weryfikacja odrzuca pusty i zbyt długi zapis, błędny podział pól, niepoprawne liczby oraz nieprawidłowy Base64. Dopuszczalna liczba iteracji wynosi 1–2000000. Sól ma dokładnie 16 bajtów, klucz dokładnie 32 bajty. Porównanie pozostaje wykonywane mechanizmem stałoczasowym dla kluczy. Błąd formatu daje `false`, a nie awarię żądania.

Górny limit kosztu jest niezależny od domyślnego kosztu nowych zapisów. Dzięki temu historyczny hash zachowuje zgodność, a przypadkowo uszkodzony rekord nie może wymusić miliardów iteracji. Wartości są polityką do pomiaru na docelowym sprzęcie: mocniejszy hash nie zwalnia z ograniczania liczby prób logowania.

## Testowanie rzeczywistej usługi

`engineering-tests/PasswordHash.Tests.csproj` dołącza produkcyjny plik usługi i jego interfejs. Program testowy sprawdza poprawne i niepoprawne hasło, Unicode, zgodność weryfikacji i uszkodzone formaty. Nie tworzy drugiej implementacji PBKDF2 jako pozornego testu.

Docelowe polecenie to `dotnet run --project engineering-tests/PasswordHash.Tests.csproj`. W tej sesji ten sam kod skompilowano bezpośrednio Roslynem i uruchomiono na .NET 8, ponieważ MSBuild środowiska nie przechodził inicjalizacji informacji procesu.

## Następny etap

Dodałbym wynik weryfikacji rozróżniający `failed`, `success` i `successNeedsRehash`, a po poprawnym logowaniu atomowo aktualizował starszy hash. Kolejne decyzje to limit prób, audyt odmów bez logowania hasła, rotacja sesji i docelowo standardowy magazyn Identity/IdP. Parametr kosztu trzeba mierzyć percentylami czasu logowania i budżetem obciążenia, a nie tylko wartością pojedynczego benchmarku.

## Weryfikacja wykonana w tej sesji

| Sprawdzenie | Wynik i znaczenie |
|---|---|
| Bezpośrednia kompilacja Roslyn i wykonanie engineering-tests/Program.cs | PASS: Password hash contracts passed |
| dotnet run / pełny build | NIEWYKONANY: awaria inicjalizacji MSBuild w środowisku |

### Granice weryfikacji

Nie wykonano pełnej solucji, testów obciążenia logowania ani integracji kont z bazą. Nie wdrożono automatycznego rehash historycznych rekordów; ograniczenie kosztu akceptuje nadal poprawne starsze hashe.

## Mapa zmian

- [`engineering-tests/PasswordHash.Tests.csproj`](engineering-tests/PasswordHash.Tests.csproj)
- [`engineering-tests/Program.cs`](engineering-tests/Program.cs)
- [`CastlePlus2.Infrastructure/Services/Auth/PasswordHashService.cs`](CastlePlus2.Infrastructure/Services/Auth/PasswordHashService.cs)
