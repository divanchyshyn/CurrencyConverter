using System.Reflection;
using CurrencyConverter.Application.Behaviours;
using CurrencyConverter.Application.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, CacheSettings cacheSettings) =>
        services
            .AddSingleton(cacheSettings)
            .AddApplicationServices()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

    private static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services;
}
