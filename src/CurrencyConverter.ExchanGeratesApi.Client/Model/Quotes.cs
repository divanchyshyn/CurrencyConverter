using System.Text.Json.Serialization;

namespace CurrencyConverter.ExchanGeratesApi.Client.Model;

public class Quotes
{
    [JsonPropertyName("AUD")] 
    public decimal AustralianDollar { get; init; }
    [JsonPropertyName("BRL")]
    public decimal BrazilianReal { get; init; }
    [JsonPropertyName("EUR")]
    public decimal Euro { get; init; }
    [JsonPropertyName("GBP")]
    public decimal BritishPound  { get; init; }
    [JsonPropertyName("USD")]
    public decimal UnitedStatesDollar { get; init; }
}