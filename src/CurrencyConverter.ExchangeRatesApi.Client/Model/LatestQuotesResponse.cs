namespace CurrencyConverter.ExchangeRatesApi.Client.Model;

public class LatestQuotesResponse
{
    public Quotes Rates { get; init; } = new();
}