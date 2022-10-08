using CurrencyConverter.ExchanGeratesApi.Client.Model;
using Refit;

namespace CurrencyConverter.ExchanGeratesApi.Client;

public interface IExchanGeratesApiClient
{
    [Get("/exchangerates_data/latest?symbols={symbols}&base={base}")]
    Task<LatestQuotesResponse> GetLatestQuotes(
        [Query] string @base,
        [Query] string symbols, 
        CancellationToken cancellationToken);
}