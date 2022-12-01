using System.Text;
using EyeTrainer.Api.Constants;
using EyeTrainer.Api.Policies;
using EyeTrainer.Api.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace EyeTrainer.Api.Bootstrap
{
    public static class AuthorizationEngine
    {
        public static void SetupAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(configuration.GetSection("Authentication:TokenSecret").Value)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = "EyeTrainer",
                        ValidIssuer = "EyeTrainer"
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.RequireRegularRights, 
                    policy => policy.RequireRole(Roles.Patient, Roles.Doctor, Roles.Administrator));

                options.AddPolicy(Policy.RequireOwner,
                    policy => policy.AddRequirements(new RequireOwnerRequirement()));
            });

            services.AddCors(options =>
            {
                options.AddPolicy(Policy.DevelopmentCors, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((x) => true);
                });
            });

            services.AddTransient<IAuthorizationHandler, RequireOwnerHandler>();
            services.AddTransient<IAuthorizationHandler, AdministratorPrivilegesHandler>();
        }
    }
}
