using CastlePlus2.Client;
using CastlePlus2.Client.Services.Auth.Http;
using CastlePlus2.Client.Services.Auth.Storage;
using CastlePlus2.Web.App.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// 1) Client DI (rejestruje m.in. CustomAuthStateProvider itd.)
builder.Services.AddCastlePlus2Client();

// 2) Storage dla tokenów (musi byæ przed handlerami/HttpClientem)
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<IAccessTokenStore, InMemoryAccessTokenStore>();
// builder.Services.AddScoped<IAccessTokenStore, ProtectedLocalStorageTokenStore>();

// 3) Handlery HTTP
builder.Services.AddTransient<BearerTokenHandler>();
builder.Services.AddTransient<AuthorizationFailureHandler>();

// 4) HttpClient: Bearer -> AuthFailure(401) -> HttpClientHandler
builder.Services.AddScoped<HttpClient>(sp =>
{
    var bearer = sp.GetRequiredService<BearerTokenHandler>();
    var authFailure = sp.GetRequiredService<AuthorizationFailureHandler>();

    authFailure.InnerHandler = new HttpClientHandler();
    bearer.InnerHandler = authFailure;

    return new HttpClient(bearer)
    {
        BaseAddress = new Uri("http://localhost:5072/")
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(CastlePlus2.Client.DependencyInjection).Assembly);

app.Run();