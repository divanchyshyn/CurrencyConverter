using AutoFixture;
using AutoFixture.AutoMoq;
using CurrencyConverter.Application.Model;
using CurrencyConverter.Application.Quotes.Queries.GetQuotes;
using CurrencyConverter.CoinMarketCap.Client;
using CurrencyConverter.CoinMarketCap.Client.Model;
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
        var coinMarketCapClientMock = fixture.Freeze<Mock<ICoinMarketCapClient>>();
        coinMarketCapClientMock.Setup(c => 
                c.GetLatestQuotes(symbol, It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(latestQuotesResponse);
        var handler = fixture.Create<GetQuotesQueryHandler>();

        var result = await handler.Handle(new GetQuotesQuery(Currency.Bitcoin), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Quotes.Should().HaveCount(5);
        var quotes = latestQuotesResponse.Data.CryptoCurrencies.First().Quotes;
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.AustralianDollar 
                                                  && q.Price == quotes.AustralianDollar.Price);
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.BrazilianReal 
                                                  && q.Price == quotes.BrazilianReal.Price);
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.BritishPound 
                                                  && q.Price == quotes.BritishPound.Price);
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.Euro 
                                                  && q.Price == quotes.Euro.Price);
        result.Value.Quotes.Should().Contain(q => q.Name == Currency.UnitedStatesDollar 
                                                  && q.Price == quotes.UnitedStatesDollar.Price);
    }

    private static LatestQuotesResponse GetQuotes() =>
        new()
        {
            Data = new Data
            {
                CryptoCurrencies = new[]
                {
                    new CryptoCurrency
                    {
                        Quotes = new CoinMarketCap.Client.Model.Quotes
                        {
                            AustralianDollar = new Quote { Price = 1m },
                            UnitedStatesDollar = new Quote { Price = 2m },
                            BrazilianReal = new Quote { Price = 3m },
                            BritishPound = new Quote { Price = 4m },
                            Euro = new Quote { Price = 5m },
                        }
                    }
                }
            }
        };
}