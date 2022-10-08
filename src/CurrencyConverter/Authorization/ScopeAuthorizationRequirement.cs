using System.Collections.Immutable;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CurrencyConverter.Authorization;

public sealed class ScopeAuthorizationRequirement
    : AuthorizationHandler<ScopeAuthorizationRequirement>, IAuthorizationRequirement
{
    private IReadOnlyCollection<string> Scopes { get; }
    public ScopeAuthorizationRequirement(IReadOnlyCollection<string> scopes)
    {
        Scopes = scopes.ToImmutableArray();
        if (!Scopes.Any())
            throw new ArgumentException(
                $"'{nameof(scopes)}' must contain at least one value.", nameof(scopes));
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ScopeAuthorizationRequirement requirement)
    {
        var scopeClaims = GetScopeClaims(context.User.Claims).ToList();
        if (!scopeClaims.Any())
        {
            return Task.CompletedTask;
        }

        var scopes = scopeClaims.SelectMany(c =>
            c.Value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries));

        return HandleRequirementAsync(context, requirement, scopes);
    }

    private Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ScopeAuthorizationRequirement requirement,
        IEnumerable<string> scopes)
    {
        if (requirement.Scopes
            .All(requiredScope => scopes.Contains(requiredScope, StringComparer.Ordinal)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
    private static IEnumerable<Claim> GetScopeClaims(IEnumerable<Claim> claims) =>
        claims.Where(c => string.Equals(
            c.Type,
            Authorization.Scopes.ClaimType,
            StringComparison.OrdinalIgnoreCase));
}