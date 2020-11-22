using System;
using System.Text;
using homepageBackend.Authorization;
using homepageBackend.Options;
using homepageBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace homepageBackend.Installers
{
    public static class MvcInstaller
    {
        public static void InstallMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddScoped<IIdentityService, IdentityService>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);

            // authenticate = use the given information and attempt to authenticate the user with that information
            //     So this will attempt to create a user identity and make it available for the framework
            // enables to store the access-token in the HTTPContext.User - property
            // helpful to get the claims from that later in the controller
            // introduction: https://docs.microsoft.com/de-de/aspnet/core/security/authentication/?view=aspnetcore-5.0
            services.AddAuthentication(a =>
                {
                    // because of this setting: the [Authorize]-Attribute in the Controller
                    // dont need to set [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] anymore
                    // since we set here the default
                    //
                    // what are these schemes?: https://stackoverflow.com/a/52493428/11244995
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(b =>
                {
                    b.SaveToken = true;
                    b.TokenValidationParameters = tokenValidationParameters;
                });

            // Authorization will be available through
            //    - 1. (cbac) policies with required claims to which the users claims (in a jwt) are evaluate against
            //        - need to be registered here to be used in the Controller with sth. like (Authorize(Policy = "ProjectViewer"))
            //    - 2. (rbac) roles
            //        - implemented through a roleManager
            //        - dont need to be registered here to be used in the controller
            //        - but needs to be registed in the dbinstaller in the addDefaultIdenetity
            //            to get the RoleManager
            //        ( - and needs to create Roles e.g. in the program.cs, i guess its better to to this in the seedData)
            //        
            services.AddAuthorization(options =>
            {
                // options.AddPolicy("ProjectViewer", builder => builder.RequireClaim("project.view", "true"));
                options.AddPolicy("MustWorkForDotCom", policy =>
                {
                    policy.AddRequirements(new WorksForCompanyRequirement(".com"));
                } );
            });

            services.AddSingleton<IAuthorizationHandler, WorksForCompanyHandler>();
        }
    }
}