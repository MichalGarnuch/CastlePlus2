using AutoMapper;
using CastlePlus2.Api.Middleware;
using CastlePlus2.Api.Services;
using CastlePlus2.Application;
using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Application.Interfaces.Dashboard;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Application.Interfaces.Exports;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Application.Mappings.Rdzen;
using CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc;
using CastlePlus2.Infrastructure.Persistence;
using CastlePlus2.Infrastructure.Repositories.Auth;
using CastlePlus2.Infrastructure.Repositories.Dokumenty;
using CastlePlus2.Infrastructure.Repositories.Finanse;
using CastlePlus2.Infrastructure.Repositories.Media;
using CastlePlus2.Infrastructure.Repositories.Najem;
using CastlePlus2.Infrastructure.Repositories.Podmioty;
using CastlePlus2.Infrastructure.Repositories.Rdzen;
using CastlePlus2.Infrastructure.Repositories.Slowniki;
using CastlePlus2.Infrastructure.Repositories.Utrzymanie;
using CastlePlus2.Infrastructure.Services.Dashboard;
using CastlePlus2.Infrastructure.Services.Exports;
using CastlePlus2.Infrastructure.Services.Najem;
using CastlePlus2.Infrastructure.Services.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models; // <--- WAŻNE: Ten using jest potrzebny do konfiguracji
using FluentValidation;
using MediatR;
using CastlePlus2.Application.Common.Behaviors;
using System.Reflection;
using CastlePlus2.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Domain.Entities.Auth;



var builder = WebApplication.CreateBuilder(args);

var exportStorageSection = builder.Configuration.GetSection("ExportStorage");
var exportStorageOptions = exportStorageSection.Get<ExportStorageOptions>() ?? new ExportStorageOptions();
if (!string.IsNullOrWhiteSpace(exportStorageOptions.RootPath) && !Path.IsPathRooted(exportStorageOptions.RootPath))
{
    exportStorageOptions.RootPath = Path.Combine(builder.Environment.ContentRootPath, exportStorageOptions.RootPath);
}

// -------------------------------------------------------------------------
// 1. Konfiguracja Bazy Danych (EF Core)
// -------------------------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CastlePlus2DbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        // To jest wymagane dla typów geograficznych (np. lokalizacja nieruchomości)
        sqlOptions.UseNetTopologySuite();

        // Odporność na chwilowe błędy sieci
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5);

        // !!! KLUCZOWA POPRAWKA !!!
        // Informujemy EF Core, że pliki migracji mają trafić do projektu Infrastructure
        sqlOptions.MigrationsAssembly("CastlePlus2.Infrastructure");
    }));

// -------------------------------------------------------------------------
// 2. Rejestracja Warstwy Application (CQRS, Mapper)
// -------------------------------------------------------------------------

//builder.Services.AddMediatR(cfg => {
//    cfg.RegisterServicesFromAssembly(typeof(CreateNieruchomoscCommand).Assembly);
//});

builder.Services.AddApplication();

builder.Services.AddValidatorsFromAssembly(typeof(CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login.LoginCommand).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// RĘCZNA konfiguracja AutoMapper – bez pakietu DI
var mapperConfig = new MapperConfiguration(cfg =>
{
    // Dodajemy wszystkie profile z assembly Application.Mappings.Rdzen
    cfg.AddMaps(typeof(NieruchomoscProfile).Assembly);

    // Gdybyś wolał jawnie:
    // cfg.AddProfile<NieruchomoscProfile>();
    // cfg.AddProfile<BudynekProfile>();
    // cfg.AddProfile<AdresProfile>();
});

IMapper mapper = mapperConfig.CreateMapper();

// Rejestrujemy jako singleton w DI
builder.Services.AddSingleton(mapper);

// -------------------------------------------------------------------------
// 3. Rejestracja Warstwy Infrastructure (Repozytoria)
// -------------------------------------------------------------------------
//RDZEN
builder.Services.AddScoped<INieruchomoscRepository, NieruchomoscRepository>();
builder.Services.AddScoped<IAdresRepository, AdresRepository>();
builder.Services.AddScoped<IBudynekRepository, BudynekRepository>();
builder.Services.AddScoped<ILokalRepository, LokalRepository>();
builder.Services.AddScoped<IPomieszczenieRepository, PomieszczenieRepository>();
builder.Services.AddScoped<IPrzypisanieAdresuRepository, PrzypisanieAdresuRepository>();
builder.Services.AddScoped<IEncjaRepository, EncjaRepository>();
//AUTH
builder.Services.AddScoped<IUzytkownikAuthRepository, UzytkownikAuthRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IAuthTokenService, AuthTokenService>();
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();


//UTRZYMANIE
builder.Services.AddScoped<IZleceniePracyRepository, ZleceniePracyRepository>();
builder.Services.AddScoped<IPowiazanieZleceniaRepository, PowiazanieZleceniaRepository>();
//DOKUMENTY
builder.Services.AddScoped<IDokumentRepository, DokumentRepository>();
builder.Services.AddScoped<IPowiazanieDokumentuRepository, PowiazanieDokumentuRepository>();
//FINANSE
builder.Services.AddScoped<IAlokacjaKosztuRepository, AlokacjaKosztuRepository>();
builder.Services.AddScoped<IKategoriaKosztuRepository, KategoriaKosztuRepository>();
builder.Services.AddScoped<IFakturaRepository, FakturaRepository>();
builder.Services.AddScoped<IPozycjaKosztuRepository, PozycjaKosztuRepository>();
builder.Services.AddScoped<IPlatnoscRepository, PlatnoscRepository>();
builder.Services.AddScoped<IRozliczeniePlatnosciRepository, RozliczeniePlatnosciRepository>();
//SLOWNIK
builder.Services.AddScoped<IWalutaRepository, WalutaRepository>();
builder.Services.AddScoped<IIndeksacjaRepository, IndeksacjaRepository>();
builder.Services.AddScoped<IJednostkaMiaryRepository, JednostkaMiaryRepository>();
//PODMIOTY
builder.Services.AddScoped<IPodmiotRepository, PodmiotRepository>();
builder.Services.AddScoped<IKontaktRepository, KontaktRepository>();
//NAJEM
builder.Services.AddScoped<IUmowaNajmuRepository, UmowaNajmuRepository>();
builder.Services.AddScoped<IPrzedmiotNajmuRepository, PrzedmiotNajmuRepository>();
builder.Services.AddScoped<ISkladnikCzynszuRepository, SkladnikCzynszuRepository>();
builder.Services.AddScoped<IKaucjaRepository, KaucjaRepository>();
builder.Services.AddScoped<IUmowaNajmuKodGenerator, UmowaNajmuKodGenerator>();
builder.Services.AddScoped<IWlasnoscRepository, WlasnoscRepository>();
builder.Services.AddScoped<INajemDashboardQueryService, NajemDashboardQueryService>();
builder.Services.AddScoped<IDashboardV1NajemQueryService, DashboardV1NajemQueryService>();
//MEDIA
builder.Services.AddScoped<IRodzajMediumRepository, RodzajMediumRepository>();
builder.Services.AddScoped<IPrzylaczeRepository, PrzylaczeRepository>();
builder.Services.AddScoped<ILicznikRepository, LicznikRepository>();
builder.Services.AddScoped<IOdczytRepository, OdczytRepository>();
//EXPORT
builder.Services.AddScoped<IReportExportService, ReportExportService>();
builder.Services.AddScoped<IExportArchiveService, ExportArchiveService>();
builder.Services.AddScoped<IReportsReadService, ReportsReadService>();
builder.Services.AddScoped<
    CastlePlus2.Application.Interfaces.Reports.IReportDefinition,
    CastlePlus2.Infrastructure.Services.Reports.Definitions.PodsumowanieOperacyjneReportDefinition>();
builder.Services.AddScoped<
    CastlePlus2.Application.Interfaces.Reports.IReportDefinition,
    CastlePlus2.Infrastructure.Services.Reports.Definitions.FakturyReportDefinition>();
builder.Services.AddScoped<
    CastlePlus2.Application.Interfaces.Reports.IReportDataPreviewService,
    CastlePlus2.Infrastructure.Services.Reports.ReportDataPreviewService>();
builder.Services.AddScoped<
    CastlePlus2.Application.Interfaces.Reports.IReportRegistry,
    CastlePlus2.Application.Reports.ReportRegistry>();
builder.Services.AddScoped<
    CastlePlus2.Application.Interfaces.Reports.IReportDataPreviewService,
    CastlePlus2.Infrastructure.Services.Reports.ReportDataPreviewService>();
builder.Services.AddScoped<
    CastlePlus2.Application.Interfaces.Reports.IReportDocumentPreviewService,
    CastlePlus2.Infrastructure.Services.Reports.ReportDocumentPreviewService>();

builder.Services.AddScoped<CsvReportExporter>();
builder.Services.AddScoped<XlsxReportExporter>();
builder.Services.AddScoped<PdfReportExporter>();
builder.Services.AddScoped<DocxReportExporter>();
builder.Services.AddHostedService<ExportArchiveRetentionService>();
builder.Services.AddSingleton(exportStorageOptions);
builder.Services.AddMemoryCache();


// -------------------------------------------------------------------------
// 4. Konfiguracja API i Swaggera (TU BYŁ PROBLEM)
// -------------------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// POPRAWKA: Jawna konfiguracja dokumentu Swaggera
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(t => t.FullName ?? t.Name);
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CastlePlus2 API", Version = "v1" });
});

var jwtSection = builder.Configuration.GetSection("Jwt");
var signingKey = jwtSection["SigningKey"];
if (string.IsNullOrWhiteSpace(signingKey))
    throw new InvalidOperationException("Brak konfiguracji Jwt:SigningKey.");

var issuer = jwtSection["Issuer"];
var audience = jwtSection["Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
            ValidateIssuer = !string.IsNullOrWhiteSpace(issuer),
            ValidIssuer = issuer,
            ValidateAudience = !string.IsNullOrWhiteSpace(audience),
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();


// -------------------------------------------------------------------------
// 5. Pipeline HTTP
// -------------------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // POPRAWKA: Wskazanie konkretnego pliku JSON
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CastlePlus2 API V1");
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// --- AUTH: Ensure Admin (DEV only) ---
if (app.Environment.IsDevelopment() &&
    builder.Configuration.GetValue<bool>("Auth:SeedAdmin:Enabled"))
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<CastlePlus2DbContext>();
    var uzytkownikRepository = scope.ServiceProvider.GetRequiredService<IUzytkownikAuthRepository>();
    var passwordHashService = scope.ServiceProvider.GetRequiredService<IPasswordHashService>();

    var seedSection = builder.Configuration.GetSection("Auth:SeedAdmin");

    var login = seedSection["Login"] ?? string.Empty;
    var email = seedSection["Email"];
    var password = seedSection["Password"] ?? string.Empty;
    var roleCode = seedSection["RoleCode"] ?? "Admin";
    var resetPassword = seedSection.GetValue<bool>("ResetPasswordOnStartup");

    if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        throw new InvalidOperationException("Auth:SeedAdmin:Login i Auth:SeedAdmin:Password muszą być ustawione.");

    // Rola
    var roleId = await uzytkownikRepository.GetRoleIdByCodeAsync(roleCode, CancellationToken.None)
        ?? await uzytkownikRepository.GetRoleIdByCodeAsync("Admin", CancellationToken.None);

    if (!roleId.HasValue)
        throw new InvalidOperationException($"Brak roli '{roleCode}' w bazie (auth.Rola).");

    var utcNow = DateTime.UtcNow;

    // Szukamy po loginie (pewniejsze niż 'AnyUsers')
    var existing = await db.Uzytkownicy
        .FirstOrDefaultAsync(x => x.Login == login, CancellationToken.None);

    if (existing is null)
    {
        var user = new Uzytkownik
        {
            Login = login,
            Email = string.IsNullOrWhiteSpace(email) ? null : email,
            HasloHash = passwordHashService.Hash(password),
            CzyAktywny = true,
            DataUtworzeniaUtc = utcNow,
            DataModyfikacjiUtc = utcNow
        };

        db.Uzytkownicy.Add(user);
        await db.SaveChangesAsync(CancellationToken.None);

        await uzytkownikRepository.AssignRoleAsync(user.IdUzytkownika, roleId.Value, CancellationToken.None);
    }
    else
    {
        // opcjonalnie reset hasła w DEV
        if (resetPassword)
        {
            existing.HasloHash = passwordHashService.Hash(password);
            existing.DataModyfikacjiUtc = utcNow;
        }

        if (!existing.CzyAktywny)
        {
            existing.CzyAktywny = true;
            existing.DataModyfikacjiUtc = utcNow;
        }

        await db.SaveChangesAsync(CancellationToken.None);

        // Ensure roli
        var roles = await uzytkownikRepository.GetRoleCodesAsync(existing.IdUzytkownika, CancellationToken.None);
        if (!roles.Contains(roleCode, StringComparer.OrdinalIgnoreCase))
        {
            await uzytkownikRepository.AssignRoleAsync(existing.IdUzytkownika, roleId.Value, CancellationToken.None);
        }
    }
}




app.Run();