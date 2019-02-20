using System;
using Loggifyio.Queries.Processors;
using Microsoft.IdentityModel.Tokens;

namespace Loggifyio.Security
{
    public static class TokenAuthOption
    {
        
            public static string Audience { get; } = "LoggifyioAudience";
            public static string Issuer { get; } = "LoggifyioIssuer";
            public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
            public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

            public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(30);
            public static string TokenType { get; } = "Bearer";
        }
    }

