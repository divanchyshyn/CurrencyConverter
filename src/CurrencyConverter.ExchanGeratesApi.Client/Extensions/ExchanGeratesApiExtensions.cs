using System.Text.Json;
using CurrencyConverter.ExchanGeratesApi.Client.Settings;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CurrencyConverter.ExchanGeratesApi.Client.Extensions;

public static class ExchanGeratesApiExtensions
{
    private const string ApiKeyHeader = "apikey";

    public static IServiceCollection ExchanGeratesApiClient(
        this IServiceCollection services,
        ExchanGeratesApiSettings exchanGeratesApiSettings)
    {
        var refitSettings = new RefitSettings(
            new SystemTextJsonContentSerializer(
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }));


        services.AddRefitClient<IExchanGeratesApiClient>(refitSettings)
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(exchanGeratesApiSettings.BaseUrl);
                client.DefaultRequestHeaders.Add(
                    ApiKeyHeader,
                    new[] { exchanGeratesApiSettings.ApiKey });
            });

        return services;
    }
}