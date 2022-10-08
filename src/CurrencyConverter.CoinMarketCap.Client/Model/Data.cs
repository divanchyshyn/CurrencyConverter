using System.Text.Json.Serialization;

namespace CurrencyConverter.CoinMarketCap.Client.Model;

public class Data
{
    [JsonPropertyName("BTC")]
    public CryptoCurrency[] CryptoCurrencies { get; init; } = Array.Empty<CryptoCurrency>();
}