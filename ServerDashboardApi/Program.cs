using Microsoft.EntityFrameworkCore;
using ServerDashboardApi.Context;
using ServerDashboardApi.Repositories;
using ServerDashboardApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DashBoardContext>(options =>
    options.UseSqlServer(connectionString));

// Services
builder.Services.AddScoped<ITemperatureService, TemperatureService>();

// Repo's
builder.Services.AddScoped<ITemperatureRepo, TemperatureRepo>();

// Background Service for USB
builder.Services.AddHostedService<SerialReaderService>();

// Caching
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<LiveTemperatureCaching>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DashBoardContext>();
    dbContext.Database.EnsureCreated();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
