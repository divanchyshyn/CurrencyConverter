using CurrencyConverter.CoinMarketCap.Client.Model;
using Refit;

namespace CurrencyConverter.CoinMarketCap.Client;

public interface ICoinMarketCapClient
{
    [Get("/v2/cryptocurrency/quotes/latest?symbol={cryptoCurrencyCode}&convert={convert}")]
    Task<LatestQuotesResponse> GetLatestQuotes(
        [Query] string cryptoCurrencyCode, 
        [Query] string convert, 
        CancellationToken cancellationToken);
}