namespace CurrencyConverter.Extensions;

public static class ConfigurationExtensions
{
    internal static T GetSetting<T>(this WebApplicationBuilder builder)
        where T : class =>
        builder.Configuration.GetSection(typeof(T).Name).Get<T>()!;
}
