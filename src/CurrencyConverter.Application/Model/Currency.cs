using CSharpFunctionalExtensions;

namespace CurrencyConverter.Application.Model;

public record Currency
{
    public static Currency Bitcoin => new("BTC");
    public static Currency UnitedStatesDollar => new("USD");
    public static Currency Euro => new("EUR");
    public static Currency BrazilianReal => new("BRL");
    public static Currency BritishPound => new("GBP");
    public static Currency AustralianDollar => new("AUD");

    private static readonly string[] Currencies = { "BTC", "USD", "EUR", "BRL", "GBP", "AUD" };
    private string Symbol { get; }

    private Currency(string symbol) => Symbol = symbol;

    public static Result<Currency> Create(string symbol)
    {
        if (!Currencies.Contains(symbol.ToUpperInvariant()))
        {
            return Result.Failure<Currency>($"{symbol} is not supported or not a valid currency");
        }

        return Result.Success(new Currency(symbol.ToUpperInvariant()));
    }

    public override string ToString() => Symbol;

    public static implicit operator string(Currency currency) => currency.ToString();
}