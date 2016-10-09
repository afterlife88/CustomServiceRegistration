using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CustomServiceRegistration.Domain.Context;
using CustomServiceRegistration.Domain.Models;
using CustomServiceRegistration.TokenProvider;
using CustomServiceRegistration.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CustomServiceRegistration
{
    public partial class Startup
    {

        private static readonly string secretKey = "tS6rP8Q5yz78Fdlkscg96Gj5TCI0Vsfl";

        private void ConfigureAuth(IApplicationBuilder app, IServiceProvider services)
        {

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/api/token",
                Audience = "ExampleAudience",
                Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            }, services);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "ExampleIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "ExampleAudience",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                // Do not automatically authenticate and challenge
                AutomaticAuthenticate = false,
                AutomaticChallenge = false,
                AuthenticationScheme = "Cookie",
                CookieName = "access_token",
                TicketDataFormat = new CustomJwtDataFormat(
                    SecurityAlgorithms.HmacSha256,
                    tokenValidationParameters)
            });
        }

        private async Task<ClaimsIdentity> GetIdentity(Dictionary<string, string> names, IServiceProvider serviceProvider)
        {
            if (names["appname"] != null)
            {
                var appname = names["appname"];
                var dataDb = serviceProvider.GetService<DataDbContext>();
                var checkingName = await dataDb.Applications.FirstOrDefaultAsync(r => r.ApplicationName == appname);

                if (checkingName != null)
                {
                    return
                        await Task.FromResult(new ClaimsIdentity(new GenericIdentity(appname, "Token"), new[]
                        {
                            new Claim("app", "app")
                        }));
                }
            }
            else
            {
                var login = names["login"];
                var password = names["password"];

                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByNameAsync(login);
                if (user != null && await userManager.CheckPasswordAsync(user, password))
                {
                    return
                        await Task.FromResult(new ClaimsIdentity(new GenericIdentity(login, "Token"), new[]
                        {
                            new Claim("user", "user")
                        }));
                }
            }
            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
