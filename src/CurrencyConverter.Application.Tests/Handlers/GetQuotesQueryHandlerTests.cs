using AutoFixture;
using AutoFixture.AutoMoq;
using CurrencyConverter.Application.Model;
using CurrencyConverter.Application.Quotes.Queries.GetQuotes;
using CurrencyConverter.ExchanGeratesApi.Client;
using CurrencyConverter.ExchanGeratesApi.Client.Model;
using FluentAssertions;
using Moq;

namespace CurrencyConverter.Application.Tests.Handlers;

public class GetQuotesQueryHandlerTests
{
    [Fact]
    public async Task Handles()
    {
        var symbol = "BTC";
        var latestQuotesResponse = GetQuotes();
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization());
        var coinMarketCapClientMock = fixture.Freeze<Mock<IExchanGeratesApiClient>>();
        coinMarketCapClientMock.Setup(c =>
                c.GetLatestQuotes(symbol, It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(latestQuotesResponse);
        var handler = fixture.Create<GetQuotesQueryHandler>();

        var result = await handler.Handle(new GetQuotesQuery(
            Currency.Create("BTC").Value),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Quotes.Should().HaveCount(5);
        var quotes = latestQuotesResponse.Rates;
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.AustralianDollar
                                                  && q.Price == quotes.AustralianDollar);
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.BrazilianReal
                                                  && q.Price == quotes.BrazilianReal);
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.BritishPound
                                                  && q.Price == quotes.BritishPound);
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.Euro
                                                  && q.Price == quotes.Euro);
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.UnitedStatesDollar
                                                  && q.Price == quotes.UnitedStatesDollar);
    }

    private static LatestQuotesResponse GetQuotes() =>
        new()
        {
            Rates = new ExchanGeratesApi.Client.Model.Quotes
            {
                AustralianDollar = 1m,
                UnitedStatesDollar = 2m,
                BrazilianReal = 3m,
                BritishPound = 4m,
                Euro = 5m
            }
        };
}