using CurrencyConverter.ExchangeRatesApi.Client.Model;
using Refit;

namespace CurrencyConverter.ExchangeRatesApi.Client;

public interface IExchangeRatesApiClient
{
    [Get("/exchangerates_data/latest?symbols={symbols}&base={base}")]
    Task<LatestQuotesResponse> GetLatestQuotes(
        [Query] string @base,
        [Query] string symbols, 
        CancellationToken cancellationToken);
}