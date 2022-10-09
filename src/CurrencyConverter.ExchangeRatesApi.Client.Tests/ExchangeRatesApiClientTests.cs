using System.Net;
using System.Text.Json;
using CurrencyConverter.ExchangeRatesApi.Client.Model;
using FluentAssertions;
using Refit;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace CurrencyConverter.ExchangeRatesApi.Client.Tests;

public sealed class ExchangeRatesApiClientTests : IDisposable
{
    private readonly WireMockServer _server;
    private readonly IExchangeRatesApiClient _client;

    public ExchangeRatesApiClientTests()
    {
        _server = WireMockServer.Start();

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }),
        };

        _client = RestService
            .For<IExchangeRatesApiClient>($"{_server.Urls[0]}", refitSettings);
    }

    [Fact]
    public async Task GetsQuotes()
    {
        MockGet("/exchangerates_data/latest");

        Func<Task> action = async () =>
        {
            await _client.GetLatestQuotes("BTC", "USD", CancellationToken.None);
        };

        await action.Should().NotThrowAsync();
    }

    private void MockGet(string path)
    {
        _server
            .Given(Request
                .Create()
                .WithPath(path)
                .UsingGet())
            .RespondWith(
                Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(JsonSerializer.Serialize(new LatestQuotesResponse())));
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}