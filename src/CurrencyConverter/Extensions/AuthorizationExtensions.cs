using CurrencyConverter.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CurrencyConverter.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddLocalAuthorization(
        this IServiceCollection services) =>
        services
            .AddAuthorizationPolicies()
            .SetLocalJwtToken();

    private static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services) =>
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicyKeys.CanRead, policy =>
                policy.AddRequirements(new ScopeAuthorizationRequirement(new[] { Scopes.Read })));

            options.AddPolicy(AuthorizationPolicyKeys.CanWrite, policy =>
                policy.AddRequirements(new ScopeAuthorizationRequirement(new[] { Scopes.Write })));
        });

    private static IServiceCollection SetLocalJwtToken(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var config = new OpenIdConnectConfiguration
                {
                    Issuer = LocalJwtToken.Issuer
                };
                config.SigningKeys.Add(LocalJwtToken.SecurityKey);

                options.Configuration = config;
                options.Audience = LocalJwtToken.Audience;
            });

        return services;
    }
}