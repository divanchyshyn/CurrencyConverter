namespace CurrencyConverter.Authorization;

public static class Scopes
{
    private const string ScopeBase = "currency-converter";
    public static readonly string Read = $"{ScopeBase}:read";
    public static readonly string Write = $"{ScopeBase}:write";
    public static readonly string ClaimType = "scope";
}