using System.Text.Json;
using CurrencyConverter.ExchangeRatesApi.Client.Settings;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CurrencyConverter.ExchangeRatesApi.Client.Extensions;

public static class ExchangeRatesApiExtensions
{
    private const string ApiKeyHeader = "apikey";

    public static IServiceCollection ExchangeRatesApiClient(
        this IServiceCollection services,
        ExchangeRatesApiSettings exchangeRatesApiSettings)
    {
        var refitSettings = new RefitSettings(
            new SystemTextJsonContentSerializer(
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }));


        services.AddRefitClient<IExchangeRatesApiClient>(refitSettings)
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(exchangeRatesApiSettings.BaseUrl);
                client.DefaultRequestHeaders.Add(
                    ApiKeyHeader,
                    new[] { exchangeRatesApiSettings.ApiKey });
            });

        return services;
    }
}