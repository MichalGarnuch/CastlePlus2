using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Application.Mappings.Rdzen;
using CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc;
using CastlePlus2.Infrastructure.Persistence;
using CastlePlus2.Infrastructure.Repositories.Rdzen;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models; // <--- WAŻNE: Ten using jest potrzebny do konfiguracji
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateNieruchomoscCommand).Assembly);
});

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
builder.Services.AddScoped<INieruchomoscRepository, NieruchomoscRepository>();
builder.Services.AddScoped<IAdresRepository, AdresRepository>();
builder.Services.AddScoped<IBudynekRepository, BudynekRepository>();
builder.Services.AddScoped<ILokalRepository, LokalRepository>();
builder.Services.AddScoped<IPomieszczenieRepository, PomieszczenieRepository>();
builder.Services.AddScoped<IPrzypisanieAdresuRepository, PrzypisanieAdresuRepository>();


// -------------------------------------------------------------------------
// 4. Konfiguracja API i Swaggera (TU BYŁ PROBLEM)
// -------------------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// POPRAWKA: Jawna konfiguracja dokumentu Swaggera
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CastlePlus2 API", Version = "v1" });
});

var app = builder.Build();

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();