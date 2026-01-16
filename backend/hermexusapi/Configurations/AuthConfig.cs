using hermexusapi.Auth.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace hermexusapi.Configurations
{
    public static class AuthConfig
    {
        public static IServiceCollection AddAuthConfiguration(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            var tokenConfigurations = new TokenConfig();

            configuration.GetSection("TokenConfigurations").Bind(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme
                    = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme
                    = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenConfigurations.Issuer,
                        ValidAudience = tokenConfigurations.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(tokenConfigurations.Secret)
                        ),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = ctx =>
                        {
                            Console.WriteLine("JWT FAILED: " + ctx.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnChallenge = ctx =>
                        {
                            Console.WriteLine("JWT CHALLENGE: " + ctx.Error + " - " + ctx.ErrorDescription);
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorizationBuilder()
                .AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            return services;
        }
    }
}
