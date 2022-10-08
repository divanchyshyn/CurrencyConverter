using System.Text.Json;
using CurrencyConverter.CoinMarketCap.Client.Settings;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CurrencyConverter.CoinMarketCap.Client.Extensions;

public static class CoinMarketCapExtensions
{
    private const string ApiKeyHeader = "X-CMC_PRO_API_KEY";

    public static IServiceCollection AddCoinMarketCapClient(
        this IServiceCollection services,
        CoinMarketCapSettings coinMarketCapSettings)
    {
        var refitSettings = new RefitSettings(
            new SystemTextJsonContentSerializer(
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }));


        services.AddRefitClient<ICoinMarketCapClient>(refitSettings)
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(coinMarketCapSettings.BaseUrl);
                client.DefaultRequestHeaders.Add(
                    ApiKeyHeader,
                    new[] { coinMarketCapSettings.ApiKey });
            });

        return services;
    }
}