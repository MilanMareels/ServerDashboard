using ServerDashboardClient.Components;
using ServerDashboardClient.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // Server Side rendering!

builder.Services.AddHttpClient<ITemperatureService, TemperatureService>(client =>
{
    var apiUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5120/api/";
    client.BaseAddress = new Uri(apiUrl);
});

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
