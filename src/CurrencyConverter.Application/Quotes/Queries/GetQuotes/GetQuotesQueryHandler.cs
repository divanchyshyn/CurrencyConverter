using CSharpFunctionalExtensions;
using CurrencyConverter.Application.Model;
using CurrencyConverter.ExchangeRatesApi.Client;
using CurrencyConverter.ExchangeRatesApi.Client.Model;
using MediatR;
using Refit;

namespace CurrencyConverter.Application.Quotes.Queries.GetQuotes;

public class GetQuotesQueryHandler : IRequestHandler<GetQuotesQuery, Result<QuotesDto>>
{
    private readonly IExchangeRatesApiClient _exchangeRatesApiClient;
    private readonly string _convertTo = 
        $"{Currency.UnitedStatesDollar},{Currency.Euro},{Currency.BrazilianReal}," +
        $"{Currency.BritishPound},{Currency.AustralianDollar}";

    public GetQuotesQueryHandler(IExchangeRatesApiClient exchangeRatesApiClient) => 
        _exchangeRatesApiClient = exchangeRatesApiClient;

    public async Task<Result<QuotesDto>> Handle(GetQuotesQuery request, CancellationToken cancellationToken)
    {
        LatestQuotesResponse result;
        try
        {
            result = await _exchangeRatesApiClient
                .GetLatestQuotes(request.Currency, _convertTo, cancellationToken);
        }
        catch (ApiException exception)
        {
            return Result.Failure<QuotesDto>($"Couldn't fetch quotes for {request.Currency}. The reason: {exception.Message}");
        }

        return MapQuotes(result.Rates);
    }

    private static QuotesDto MapQuotes(ExchangeRatesApi.Client.Model.Quotes quotes) =>
        new()
        {
            Quotes = new []
            {
                new QuoteDto { Name =  Currency.UnitedStatesDollar, Price = quotes.UnitedStatesDollar },
                new QuoteDto { Name =  Currency.Euro, Price = quotes.Euro },
                new QuoteDto { Name =  Currency.BrazilianReal, Price = quotes.BrazilianReal },
                new QuoteDto { Name =  Currency.BritishPound, Price = quotes.BritishPound },
                new QuoteDto { Name =  Currency.AustralianDollar, Price = quotes.AustralianDollar }
            }
        };
}