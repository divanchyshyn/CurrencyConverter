using CSharpFunctionalExtensions;

namespace CurrencyConverter.Application.Model;

public record Currency
{
    public static Currency UnitedStatesDollar => new("USD");
    public static Currency Euro => new("EUR");
    public static Currency BrazilianReal => new("BRL");
    public static Currency BritishPound => new("GBP");
    public static Currency AustralianDollar => new("AUD");

    private string Symbol { get; }

    private Currency(string symbol) => Symbol = symbol;

    public static Result<Currency> Create(string symbol)
    {
        return symbol.Length != 3 
            ? Result.Failure<Currency>($"'{symbol}' is not a 3-letter ISO code") 
            : Result.Success(new Currency(symbol.ToUpperInvariant()));
    }

    public override string ToString() => Symbol;

    public static implicit operator string(Currency currency) => currency.ToString();
}