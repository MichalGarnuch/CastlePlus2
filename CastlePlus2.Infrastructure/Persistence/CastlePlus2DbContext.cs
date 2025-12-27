using CastlePlus2.Domain.Entities.Dokumenty;
using CastlePlus2.Domain.Entities.Finanse;
using CastlePlus2.Domain.Entities.Media;
using CastlePlus2.Domain.Entities.Najem;
using CastlePlus2.Domain.Entities.Podmioty;
using CastlePlus2.Domain.Entities.Rdzen;
using CastlePlus2.Domain.Entities.Slowniki;
using CastlePlus2.Domain.Entities.Utrzymanie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Infrastructure.Persistence
{
    /// <summary>
    /// Główny kontekst EF Core dla systemu CastlePlus2.
    /// - Ten kontekst reprezentuje połączenie z bazą CastlePlus2 w SQL Server.
    /// - Każda tabela w bazie będzie miała odpowiadający jej DbSet w tym kontekście.
    /// - DbContext jest używany wyłącznie w warstwie Infrastructure oraz w implementacjach repozytoriów.
    /// </summary>
    public class CastlePlus2DbContext : DbContext
    {
        /// <summary>
        /// Konstruktor używany przez DI (Dependency Injection).
        /// - DbContextOptions zawiera m.in. connection string i konfigurację providera (SqlServer).
        /// - W Program.cs będziemy rejestrować ten kontekst w kontenerze DI.
        /// </summary>
        /// <param name="options">Opcje kontekstu przekazywane przez kontener DI.</param>
        public CastlePlus2DbContext(DbContextOptions<CastlePlus2DbContext> options)
            : base(options)
        {
        }

        // -------------------------------------------------------------
        // DbSet'y – tutaj dodajemy zbiory encji odwzorowujące tabele.
        // -------------------------------------------------------------

        // RDZEN
        public DbSet<Nieruchomosc> Nieruchomosci { get; set; } = null!;
        public DbSet<Encja> Encje { get; set; } = null!;
        public DbSet<Adres> Adresy { get; set; } = null!;
        public DbSet<Budynek> Budynki { get; set; } = null!;
        public DbSet<Lokal> Lokale { get; set; } = null!;
        public DbSet<Pomieszczenie> Pomieszczenia { get; set; } = default!;
        public DbSet<PrzypisanieAdresu> PrzypisaniaAdresow => Set<PrzypisanieAdresu>();

        //UTRZYMANIE
        public DbSet<ZleceniePracy> ZleceniaPracy { get; set; } = null!;
        public DbSet<PowiazanieZlecenia> PowiazaniaZlecenia { get; set; } = null!;

        //DOKUMENTY
        public DbSet<Dokument> Dokumenty { get; set; } = null!;
        public DbSet<PowiazanieDokumentu> PowiazaniaDokumentow { get; set; } = null!;

        //FINANSE
        public DbSet<AlokacjaKosztu> AlokacjeKosztow { get; set; } = null!;
        public DbSet<KategoriaKosztu> KategorieKosztow { get; set; } = null!;
        public DbSet<Faktura> Faktury { get; set; } = null!;
        public DbSet<PozycjaKosztu> PozycjeKosztow { get; set; } = null!;
        public DbSet<Platnosc> Platnosci { get; set; } = null!;
        public DbSet<RozliczeniePlatnosci> RozliczeniaPlatnosci { get; set; } = null!;

        // SŁOWNIKI
        public DbSet<Waluta> Waluty => Set<Waluta>();
        public DbSet<Indeksacja> Indeksacje => Set<Indeksacja>();
        public DbSet<JednostkaMiary> JednostkiMiary => Set<JednostkaMiary>();

        // PODMIOTY
        public DbSet<Podmiot> Podmioty => Set<Podmiot>();
        public DbSet<Kontakt> Kontakty { get; set; } = null!;
        public DbSet<Wlasnosc> Wlasnosci { get; set; } = null!;

        // NAJEM
        public DbSet<UmowaNajmu> UmowyNajmu => Set<UmowaNajmu>();
        public DbSet<PrzedmiotNajmu> PrzedmiotyNajmu => Set<PrzedmiotNajmu>();
        public DbSet<SkladnikCzynszu> SkladnikiCzynszu => Set<SkladnikCzynszu>();
        public DbSet<Kaucja> Kaucje => Set<Kaucja>();

        // MEDIA
        public DbSet<RodzajMedium> RodzajeMediow { get; set; } = null!;
        public DbSet<Przylacze> Przylacza { get; set; } = null!;
        public DbSet<Licznik> Liczniki { get; set; } = null!;
        public DbSet<Odczyt> Odczyty { get; set; } = null!;

        /// <summary>
        /// Metoda wywoływana przy tworzeniu modelu EF Core.
        /// - Wykorzystujemy ją do zastosowania konfiguracji encji
        ///   (IEntityTypeConfiguration&lt;T&gt;), które rozbijemy na osobne klasy.
        /// </summary>
        /// <param name="modelBuilder">Obiekt do budowania modelu EF Core.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Automatyczne zastosowanie wszystkich konfiguracji z tego assembly.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CastlePlus2DbContext).Assembly);
        }
    }
}

