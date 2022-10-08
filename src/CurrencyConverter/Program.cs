using System.Security.Claims;
using CurrencyConverter.Application.Extensions;
using CurrencyConverter.Application.Settings;
using CurrencyConverter.Authorization;
using CurrencyConverter.CoinMarketCap.Client.Extensions;
using CurrencyConverter.CoinMarketCap.Client.Settings;
using CurrencyConverter.Extensions;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication
    .CreateBuilder(args);

builder.Host.UseSerilog((_, config) =>
{
    config
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

builder.Services.AddControllers();
builder.Services
    .AddDistributedMemoryCache()
    .AddLocalAuthorization()
    .AddEndpointsApiExplorer()
    .AddApiVersioning(o =>
    {
        o.DefaultApiVersion = new ApiVersion(1, 0);
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.ReportApiVersions = true;
    })
    .AddCoinMarketCapClient(builder.GetSetting<CoinMarketCapSettings>())
    .AddApplication(builder.GetSetting<CacheSettings>())
    .AddSwaggerGen(opt => 
        opt.UseLocalTokenProviderOptions(new[]  
        {
            new Claim("scope", Scopes.Read),
            new Claim("scope", Scopes.Write)
    }))
    .AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
