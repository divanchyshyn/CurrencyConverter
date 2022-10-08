using CSharpFunctionalExtensions;
using CurrencyConverter.Application.Interfaces;
using CurrencyConverter.Application.Model;
using MediatR;

namespace CurrencyConverter.Application.Quotes.Queries.GetQuotes;

public record GetQuotesQuery(Currency Currency) : IRequest<Result<QuotesDto>>, ICacheableQuery
{
    public bool BypassCache  => false;
    public string CacheKey => Currency;
}