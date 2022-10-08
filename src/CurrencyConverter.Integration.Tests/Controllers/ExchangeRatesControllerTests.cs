using System.Net;
using CurrencyConverter.Application.Model;
using CurrencyConverter.Application.Quotes.Queries.GetQuotes;
using CurrencyConverter.Authorization;
using CurrencyConverter.Integration.Tests.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace CurrencyConverter.Integration.Tests.Controllers;

public sealed class ExchangeRatesControllerTests : IClassFixture<WebApplicationFactory<StartupPlaceHolder>>, IDisposable
{
    public void Dispose() => _client.Dispose();

    private readonly HttpClient _client;

    public ExchangeRatesControllerTests(WebApplicationFactory<StartupPlaceHolder> factory) => 
        _client = factory.CreateClient();

    [Fact]
    public async Task ReturnsQuotes()
    {
        using var result = await _client.SetClientScope(Scopes.Read)
            .GetAsync("/v1/ExchangeRates?symbol=BTC");

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await result.Content.ReadAsStringAsync();
        var quotesDto = JsonConvert.DeserializeObject<QuotesDto>(json);

        quotesDto.Should().NotBeNull();
        quotesDto!.Quotes.Should().HaveCount(5);
        quotesDto!.Quotes.Should().Contain(q => q.Name == Currency.AustralianDollar);
        quotesDto!.Quotes.Should().Contain(q => q.Name == Currency.BrazilianReal);
        quotesDto!.Quotes.Should().Contain(q => q.Name == Currency.BritishPound);
        quotesDto!.Quotes.Should().Contain(q => q.Name == Currency.Euro);
        quotesDto!.Quotes.Should().Contain(q => q.Name == Currency.UnitedStatesDollar);
    }

    [Fact]
    public async Task ReturnsBadRequest()
    {
        using var result = await _client.SetClientScope(Scopes.Read)
            .GetAsync("/v1/ExchangeRates?symbol=UnknownCurrency");

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}