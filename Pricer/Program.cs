/*
Main program of the api.
*/

using Microsoft.EntityFrameworkCore;
using Pagination;
using PricerApi.Data;
using PricerApi.Endpoints;
using PricerApi.Log;
using PricerApi.StockRepository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

// Use Serilog as the logging provider
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStockRepository, SqlStockRepository>();
builder.Services.AddSingleton<PaginationParameters, PaginationParameters>();
builder.Services.AddDbContext<PricerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Exception handler should be FIRST because after logging, we fail.
app.UseMiddleware<ExceptionLoggingMiddleware>();

// Then request logging
app.UseMiddleware<RequestLoggingMiddleware>();

// Register endpoints
RouteGroupBuilder versionGroup = app.MapGroup("api/");
versionGroup.MapStockEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Handle Migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PricerDbContext>();
    await context.Database.MigrateAsync();
}

#region Run application

try
{
    Log.Information("Starting web application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}

#endregion
