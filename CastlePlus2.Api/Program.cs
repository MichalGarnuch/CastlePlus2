using AutoMapper;
using CastlePlus2.Api.Middleware;
using CastlePlus2.Api.Services;
using CastlePlus2.Api.Services.Auth;
using CastlePlus2.Application.Common.Behaviors;
using CastlePlus2.Application.Finanse.ProcesyFaktury.Common;
using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Application.Interfaces.Dashboard;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Application.Interfaces.Exports;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Application.Interfaces.Reports;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Application.Interfaces.Notifications;
using CastlePlus2.Application.Mappings.Rdzen;
using CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc;
using CastlePlus2.Domain.Entities.Auth;
using CastlePlus2.Infrastructure.Persistence;
using CastlePlus2.Infrastructure.Repositories.Auth;
using CastlePlus2.Infrastructure.Repositories.Dokumenty;
using CastlePlus2.Infrastructure.Repositories.Finanse;
using CastlePlus2.Infrastructure.Repositories.Konfiguracja;
using CastlePlus2.Infrastructure.Repositories.Media;
using CastlePlus2.Infrastructure.Repositories.Najem;
using CastlePlus2.Infrastructure.Repositories.Podmioty;
using CastlePlus2.Infrastructure.Repositories.Rdzen;
using CastlePlus2.Infrastructure.Repositories.Slowniki;
using CastlePlus2.Infrastructure.Repositories.Utrzymanie;
using CastlePlus2.Infrastructure.Services.Auth;
using CastlePlus2.Infrastructure.Services.Dashboard;
using CastlePlus2.Infrastructure.Services.Exports;
using CastlePlus2.Infrastructure.Services.Najem;
using CastlePlus2.Infrastructure.Services.Reports;
using CastlePlus2.Infrastructure.Services.Notifications;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using CastlePlus2.Shared.Auth;

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


if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
    "Brak ConnectionStrings:DefaultConnection. Uzupełnij w CastlePlus2.Api/appsettings.json (lub user-secrets).");
}


builder.Services.AddDbContext<CastlePlus2DbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.UseNetTopologySuite();
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5);
        sqlOptions.MigrationsAssembly("CastlePlus2.Infrastructure");
    });


    // tylko DEV – pomaga złapać dokładne SQL i miejsce, gdzie wali 'Sort'
    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
    }
});

// -------------------------------------------------------------------------
// 2. Rejestracja warstwy Application: MediatR + FluentValidation (bez AddApplication())
//    (to usuwa typową przyczynę duplikacji profili AutoMapper)
// -------------------------------------------------------------------------
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateNieruchomoscCommand).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login.LoginCommand).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped<IFakturaCreationService, FakturaCreationService>();

// -------------------------------------------------------------------------
// 2b. AutoMapper – ręczna konfiguracja (jedno źródło prawdy)
// -------------------------------------------------------------------------
var mapperConfig = new MapperConfiguration(cfg =>
{
    // Jeśli AutoMapper w Twojej wersji to wspiera – włączamy additive,
    // żeby nie wywalało appki przy zduplikowanych CreateMap (zostanie „ostatnia” definicja).
    TryEnableAdditiveTypeMapCreation(cfg);

    // Profile z assembly Application (masz tam wszystkie moduły)
    cfg.AddMaps(typeof(NieruchomoscProfile).Assembly);
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// -------------------------------------------------------------------------
// 3. Rejestracja Warstwy Infrastructure (Repozytoria / Serwisy)
// -------------------------------------------------------------------------
// RDZEN
builder.Services.AddScoped<INieruchomoscRepository, NieruchomoscRepository>();
builder.Services.AddScoped<IAdresRepository, AdresRepository>();
builder.Services.AddScoped<IBudynekRepository, BudynekRepository>();
builder.Services.AddScoped<ILokalRepository, LokalRepository>();
builder.Services.AddScoped<IPomieszczenieRepository, PomieszczenieRepository>();
builder.Services.AddScoped<IPrzypisanieAdresuRepository, PrzypisanieAdresuRepository>();
builder.Services.AddScoped<IEncjaRepository, EncjaRepository>();

// AUTH
builder.Services.AddScoped<IUzytkownikAuthRepository, UzytkownikAuthRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IAuthTokenService, AuthTokenService>();
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<IAccessRequestRepository, RequestAccessRepository>();
builder.Services.AddScoped<IActivationTokenRepository, ActivationTokenRepository>();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<IAppUrlProvider, AppUrlProvider>();
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("Email"));

// UTRZYMANIE
builder.Services.AddScoped<IZleceniePracyRepository, ZleceniePracyRepository>();
builder.Services.AddScoped<IPowiazanieZleceniaRepository, PowiazanieZleceniaRepository>();

// DOKUMENTY
builder.Services.AddScoped<IDokumentRepository, DokumentRepository>();
builder.Services.AddScoped<IPowiazanieDokumentuRepository, PowiazanieDokumentuRepository>();

// KONFIGURACJA
builder.Services.AddScoped<IZasobUIRepository, ZasobUIRepository>();
builder.Services.AddScoped<IZasobUITekstRepository, ZasobUITekstRepository>();

// FINANSE
builder.Services.AddScoped<IAlokacjaKosztuRepository, AlokacjaKosztuRepository>();
builder.Services.AddScoped<IKategoriaKosztuRepository, KategoriaKosztuRepository>();
builder.Services.AddScoped<IFakturaRepository, FakturaRepository>();
builder.Services.AddScoped<IPozycjaKosztuRepository, PozycjaKosztuRepository>();
builder.Services.AddScoped<IPlatnoscRepository, PlatnoscRepository>();
builder.Services.AddScoped<IRozliczeniePlatnosciRepository, RozliczeniePlatnosciRepository>();

// SLOWNIKI
builder.Services.AddScoped<IWalutaRepository, WalutaRepository>();
builder.Services.AddScoped<IIndeksacjaRepository, IndeksacjaRepository>();
builder.Services.AddScoped<IJednostkaMiaryRepository, JednostkaMiaryRepository>();

// PODMIOTY
builder.Services.AddScoped<IPodmiotRepository, PodmiotRepository>();
builder.Services.AddScoped<IKontaktRepository, KontaktRepository>();

// NAJEM
builder.Services.AddScoped<IUmowaNajmuRepository, UmowaNajmuRepository>();
builder.Services.AddScoped<IPrzedmiotNajmuRepository, PrzedmiotNajmuRepository>();
builder.Services.AddScoped<ISkladnikCzynszuRepository, SkladnikCzynszuRepository>();
builder.Services.AddScoped<IKaucjaRepository, KaucjaRepository>();
builder.Services.AddScoped<IUmowaNajmuKodGenerator, UmowaNajmuKodGenerator>();
builder.Services.AddScoped<IWlasnoscRepository, WlasnoscRepository>();
builder.Services.AddScoped<INajemDashboardQueryService, NajemDashboardQueryService>();
builder.Services.AddScoped<IDashboardV1NajemQueryService, DashboardV1NajemQueryService>();
builder.Services.AddScoped<INajemPowerDashboardDataService, NajemPowerDashboardDataService>();
builder.Services.AddScoped<INajemAnalitykaQueryService, NajemAnalitykaQueryService>();


// MEDIA
builder.Services.AddScoped<IRodzajMediumRepository, RodzajMediumRepository>();
builder.Services.AddScoped<IPrzylaczeRepository, PrzylaczeRepository>();
builder.Services.AddScoped<ILicznikRepository, LicznikRepository>();
builder.Services.AddScoped<IOdczytRepository, OdczytRepository>();

// EXPORT / REPORTS
builder.Services.AddScoped<IReportExportService, ReportExportService>();
builder.Services.AddScoped<IFakturaDocxTemplateRenderer, FakturaDocxTemplateRenderer>();
builder.Services.AddScoped<IDokumentFileStorage, DokumentFileStorage>();
builder.Services.AddScoped<IExportArchiveService, ExportArchiveService>();
builder.Services.AddScoped<IReportsReadService, ReportsReadService>();

builder.Services.AddScoped<
    CastlePlus2.Application.Interfaces.Reports.IReportDefinition,
    CastlePlus2.Infrastructure.Services.Reports.Definitions.PodsumowanieOperacyjneReportDefinition>();
builder.Services.AddScoped<
    CastlePlus2.Application.Interfaces.Reports.IReportDefinition,
    CastlePlus2.Infrastructure.Services.Reports.Definitions.FakturyReportDefinition>();

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
// 4. Konfiguracja API i Swagger
// -------------------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(t => t.FullName ?? t.Name);
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CastlePlus2 API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// -------------------------------------------------------------------------
// 5. JWT AuthN/AuthZ
// -------------------------------------------------------------------------
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
            ClockSkew = TimeSpan.FromMinutes(1),
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmployerOrAdmin", policy =>
        policy.RequireRole(RoleCodes.Admin, RoleCodes.Employee, RoleCodes.Manager));
});
builder.Services.AddTransient<IClaimsTransformation, RoleClaimsTransformation>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<CastlePlus2DbContext>();


    var conn = db.Database.GetDbConnection();
    app.Logger.LogInformation("EF CONNECTED TO: {DataSource} | DB: {Database}",
    conn.DataSource, conn.Database);


    try
    {
        await db.Database.ExecuteSqlRawAsync("SELECT TOP(0) [Sort] FROM [konfiguracja].[ZasobUI];");
        app.Logger.LogInformation("CHECK OK: [konfiguracja].[ZasobUI] has column [Sort].");


        await db.Database.ExecuteSqlRawAsync("SELECT TOP(0) [Sort] FROM [konfiguracja].[ZasobUITekst];");
        app.Logger.LogInformation("CHECK OK: [konfiguracja].[ZasobUITekst] has column [Sort].");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "CHECK FAILED: column [Sort] not found for current connection.");
    }
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
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
    var roleId = await uzytkownikRepository.GetRoleIdByCodeAsync(roleCode, CancellationToken.None);
    if (!roleId.HasValue)
    {
        var fallbackRoleCode = string.Equals(roleCode, "Admin", StringComparison.OrdinalIgnoreCase)
            ? "ADMIN"
            : "Admin";
        roleId = await uzytkownikRepository.GetRoleIdByCodeAsync(fallbackRoleCode, CancellationToken.None);
        if (roleId.HasValue)
        {
            roleCode = fallbackRoleCode;
        }
    }

    if (!roleId.HasValue && !string.Equals(roleCode, "Admin", StringComparison.OrdinalIgnoreCase))
    {
        roleCode = "Admin";
        roleId = await uzytkownikRepository.GetRoleIdByCodeAsync(roleCode, CancellationToken.None)
            ?? await uzytkownikRepository.GetRoleIdByCodeAsync("ADMIN", CancellationToken.None);
    }

    if (!roleId.HasValue)
        throw new InvalidOperationException($"Brak roli '{roleCode}' w bazie (auth.Rola).");

    var utcNow = DateTime.UtcNow;

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

        var roles = await uzytkownikRepository.GetRoleCodesAsync(existing.IdUzytkownika, CancellationToken.None);
        if (!roles.Contains(roleCode, StringComparer.OrdinalIgnoreCase))
        {
            await uzytkownikRepository.AssignRoleAsync(existing.IdUzytkownika, roleId.Value, CancellationToken.None);
        }
    }
}

app.Run();

static void TryEnableAdditiveTypeMapCreation(IMapperConfigurationExpression cfg)
{
    // AutoMapper: w zależności od wersji, opcja może być w cfg.Advanced.* albo cfg.Internal().*
    try
    {
        var advanced = cfg.GetType().GetProperty("Advanced")?.GetValue(cfg);
        var allow = advanced?.GetType().GetProperty("AllowAdditiveTypeMapCreation");
        if (allow?.CanWrite == true)
        {
            allow.SetValue(advanced, true);
            return;
        }
    }
    catch
    {
        // ignore
    }

    try
    {
        var internalMethod = cfg.GetType().GetMethod(
            "Internal",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            binder: null,
            types: Type.EmptyTypes,
            modifiers: null);

        var internalCfg = internalMethod?.Invoke(cfg, null);
        var allow = internalCfg?.GetType().GetProperty("AllowAdditiveTypeMapCreation");
        if (allow?.CanWrite == true)
        {
            allow.SetValue(internalCfg, true);
        }
    }
    catch
    {
        // ignore
    }
}
