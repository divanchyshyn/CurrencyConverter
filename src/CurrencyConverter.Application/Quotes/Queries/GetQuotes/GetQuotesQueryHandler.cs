using CSharpFunctionalExtensions;
using CurrencyConverter.Application.Model;
using CurrencyConverter.CoinMarketCap.Client;
using MediatR;

namespace CurrencyConverter.Application.Quotes.Queries.GetQuotes;

public class GetQuotesQueryHandler : IRequestHandler<GetQuotesQuery, Result<QuotesDto>>
{
    private readonly ICoinMarketCapClient _coinMarketCapClient;
    private readonly string _convertTo = 
        $"{Currency.UnitedStatesDollar},{Currency.Euro},{Currency.BrazilianReal}," +
        $"{Currency.BritishPound},{Currency.AustralianDollar}";

    public GetQuotesQueryHandler(ICoinMarketCapClient coinMarketCapClient) => 
        _coinMarketCapClient = coinMarketCapClient;

    public async Task<Result<QuotesDto>> Handle(GetQuotesQuery request, CancellationToken cancellationToken)
    {
        var result = await _coinMarketCapClient
            .GetLatestQuotes(request.Currency, _convertTo, cancellationToken);

        if (result.Data.CryptoCurrencies.Length == 0)
            return Result.Failure<QuotesDto>($"Couldn't fetch quotes for {request.Currency}");

        return MapQuotes(result.Data.CryptoCurrencies.First().Quotes);
    }

    private static QuotesDto MapQuotes(CoinMarketCap.Client.Model.Quotes quotes) =>
        new()
        {
            Quotes = new []
            {
                new QuoteDto { Name =  Currency.UnitedStatesDollar, Price = quotes.UnitedStatesDollar.Price },
                new QuoteDto { Name =  Currency.Euro, Price = quotes.Euro.Price },
                new QuoteDto { Name =  Currency.BrazilianReal, Price = quotes.BrazilianReal.Price },
                new QuoteDto { Name =  Currency.BritishPound, Price = quotes.BritishPound.Price },
                new QuoteDto { Name =  Currency.AustralianDollar, Price = quotes.AustralianDollar.Price }
            }
        };
}