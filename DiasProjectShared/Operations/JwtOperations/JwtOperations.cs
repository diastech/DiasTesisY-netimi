using DiasShared.Operations.JwtOperations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiasShared.Operations.JwtOperation
{
    public static class JwtOperations
    {
        /// <summary>
        /// Referans olması için tutuluyor, kullanılmayacak
        /// </summary>
        private static void ConfigureService(IServiceCollection service, string Issuer, string Audience, string Key)
        {
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = Issuer,
                        ValidAudience = Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key))
                    };
                });
        }

        public static string ProduceNewJwtToken(string tokenIdentifier, IList<Claim> companyRoleClaims = null)
        {
            JwtSecurityTokenHandler tokenHandler = new ();

            byte[] key = StaticJwtData.GetSecretKeyByBytes();

            List<Claim> addedClaims = new();

            addedClaims.Add(new Claim(ClaimTypes.Name, tokenIdentifier));

            if(companyRoleClaims != null)
            {
                foreach (Claim item in companyRoleClaims)
                {
                    addedClaims.Add(item);
                }
            }

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(addedClaims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
