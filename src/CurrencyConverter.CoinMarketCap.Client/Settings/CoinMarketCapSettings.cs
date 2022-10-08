namespace CurrencyConverter.CoinMarketCap.Client.Settings;

public sealed class CoinMarketCapSettings
{
    public string BaseUrl { get; init; } = string.Empty;
    public string ApiKey { get; init; } = string.Empty;
}