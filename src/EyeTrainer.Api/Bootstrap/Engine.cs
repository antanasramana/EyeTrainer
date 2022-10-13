using EyeTrainer.Api.Handlers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using IAuthenticationHandler = EyeTrainer.Api.Handlers.IAuthenticationHandler;

namespace EyeTrainer.Api.Bootstrap
{
    public static class Engine
    {
        public static void SetupDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthenticationHandler, AuthenticationHandler>();
            serviceCollection.AddTransient<ITokenGenerator, TokenGenerator>();
            serviceCollection.AddTransient<SecurityTokenHandler, JwtSecurityTokenHandler>();
        }
    }
}
