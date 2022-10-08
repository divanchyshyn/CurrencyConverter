using System.Text.Json.Serialization;

namespace CurrencyConverter.CoinMarketCap.Client.Model;

public class Quotes
{
    [JsonPropertyName("AUD")] 
    public Quote AustralianDollar { get; init; } = new();
    [JsonPropertyName("BRL")]
    public Quote BrazilianReal { get; init; } = new();
    [JsonPropertyName("EUR")]
    public Quote Euro { get; init; } = new();
    [JsonPropertyName("GBP")]
    public Quote BritishPound  { get; init; } = new();
    [JsonPropertyName("USD")]
    public Quote UnitedStatesDollar { get; init; } = new();
}