using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace CurrencyConverter.Authorization;

public static class LocalJwtToken
{
    public static string Issuer { get; } = Guid.NewGuid().ToString();
    public static string Audience { get; } = Guid.NewGuid().ToString();
    public static SecurityKey SecurityKey { get; }
    private static SigningCredentials SigningCredentials { get; }

    private static readonly JwtSecurityTokenHandler TokenHandler = new();
    private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();
    private static readonly byte[] Key = new byte[32];

    static LocalJwtToken()
    {
        RandomNumberGenerator.GetBytes(Key);
        SecurityKey = new SymmetricSecurityKey(Key) { KeyId = Guid.NewGuid().ToString() };
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
    }

    public static string GenerateJwtToken(IEnumerable<Claim> claims) =>
        TokenHandler.WriteToken(new JwtSecurityToken(Issuer, Audience, claims, null,
            DateTime.UtcNow.AddMinutes(30), SigningCredentials));
}