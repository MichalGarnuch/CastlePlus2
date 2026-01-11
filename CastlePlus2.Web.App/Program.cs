using MudBlazor.Services;
using CastlePlus2.Client.Services.Auth.Http;
using CastlePlus2.Client.Services.Auth.Storage;
using CastlePlus2.Client;
using CastlePlus2.Web.App.Components;
using CastlePlus2.Web.App.Services.Auth;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();


builder.Services.AddHttpClient("ApiClient", client => client.BaseAddress = new Uri("http://localhost:5072/"))
    .AddHttpMessageHandler<BearerTokenHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

builder.Services.AddCastlePlus2Client();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<IAccessTokenStore, ProtectedLocalStorageTokenStore>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(CastlePlus2.Client.DependencyInjection).Assembly);

app.Run();