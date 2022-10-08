using System.Net.Http.Headers;
using System.Security.Claims;
using CurrencyConverter.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CurrencyConverter.Integration.Tests.Extensions;

internal static class HttpClientExtensions
{
    public static HttpClient SetClientScope(this HttpClient client, string scope)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                LocalJwtToken.GenerateJwtToken(new[]
                {
                    new Claim("scope", scope)
                }));

        return client;
    }
}