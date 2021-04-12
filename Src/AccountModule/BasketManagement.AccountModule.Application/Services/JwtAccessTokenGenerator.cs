using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BasketManagement.AccountModule.Domain;
using BasketManagement.AccountModule.Domain.Services;
using BasketManagement.AccountModule.Domain.ValueObjects;

namespace BasketManagement.AccountModule.Application.Services
{
    public class JwtAccessTokenGenerator : IAccessTokenGenerator
    {
        public const string JWT_KEY = "BasketManagementCustomJwtKey";
        public const string JWT_ISSUER = "BasketManagementJwtIssuer";
        public const string JWT_AUDIENCE = "BasketManagementAudience";

        public AccessToken Generate(AccountId accountId, Roles role)
        {
            DateTime expireAt = DateTime.UtcNow.AddDays(1);

            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, accountId.Value.ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            string tokenValue = GenerateJwtToken(expireAt, claims);

            return new AccessToken(accountId, tokenValue, expireAt);
        }

        private static string GenerateJwtToken(DateTime expireTime, IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_KEY));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(JWT_ISSUER,
                                             JWT_AUDIENCE,
                                             claims,
                                             expires: expireTime,
                                             signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}