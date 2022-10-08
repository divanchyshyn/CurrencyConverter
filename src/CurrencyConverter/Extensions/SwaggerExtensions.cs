using System.Security.Claims;
using CurrencyConverter.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CurrencyConverter.Extensions;

public static class SwaggerExtensions
{
    public static SwaggerGenOptions UseLocalTokenProviderOptions(this SwaggerGenOptions options, IEnumerable<Claim> claims)
    {
        options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = LocalJwtToken.GenerateJwtToken(claims)
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                },
                new string[] {}
            }
        });

        return options;
    }

}