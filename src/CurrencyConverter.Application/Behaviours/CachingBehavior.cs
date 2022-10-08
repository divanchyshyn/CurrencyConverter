using System.Text;
using CurrencyConverter.Application.Interfaces;
using CurrencyConverter.Application.Settings;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CurrencyConverter.Application.Behaviours;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableQuery, IRequest<TResponse>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger _logger;
    private readonly CacheSettings _settings;
    public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger, CacheSettings settings)
    {
        _cache = cache;
        _logger = logger;
        _settings = settings;
    }
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (request.BypassCache) return await next();

        TResponse response;
        var cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
        if (cachedResponse != null)
        {
            response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse))!;
            _logger.LogInformation($"Fetched from Cache -> '{request.CacheKey}'.");
        }
        else
        {
            response = await GetResponseAndAddToCache(request, next, cancellationToken);
            _logger.LogInformation($"Added to Cache -> '{request.CacheKey}'.");
        }
        return response;
    }

    async Task<TResponse> GetResponseAndAddToCache(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var response = await next();
        var slidingExpiration = TimeSpan.FromMinutes(_settings.SlidingExpirationInMinutes);
        var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
        var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));

        await _cache.SetAsync(request.CacheKey, serializedData, options, cancellationToken);

        return response;
    }
}