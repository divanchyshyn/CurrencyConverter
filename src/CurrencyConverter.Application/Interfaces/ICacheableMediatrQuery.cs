namespace CurrencyConverter.Application.Interfaces;

public interface ICacheableQuery
{
    bool BypassCache { get; }
    string CacheKey { get; }
}