using System.Text.Json.Serialization;

namespace CurrencyConverter.CoinMarketCap.Client.Model;

public class CryptoCurrency
{
    [JsonPropertyName("quote")] 
    public Quotes Quotes { get; init; } = new();
}