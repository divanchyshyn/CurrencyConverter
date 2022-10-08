using System.Net;
using System.Text.Json;
using CurrencyConverter.CoinMarketCap.Client.Model;
using FluentAssertions;
using Refit;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace CurrencyConverter.CoinMarketCap.Client.Tests;

public sealed class CoinMarketCapClientTests : IDisposable
{
    private readonly WireMockServer _server;
    private readonly ICoinMarketCapClient _client;

    public CoinMarketCapClientTests()
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
            .For<ICoinMarketCapClient>($"{_server.Urls[0]}", refitSettings);
    }

    [Fact]
    public async Task Posts_UsedPaymentReferenceRequest()
    {
        MockGet("/v2/cryptocurrency/quotes/latest");

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