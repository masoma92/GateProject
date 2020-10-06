using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GateProjectBackend.Common.Startup
{
    public class ApiStartup
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public ApiStartup(
            IServiceCollection services,
            IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void AddSwaggerGen(bool addSecurity = false)
        {
            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = _configuration["Api:Title"],
                    Description = _configuration["Api:Description"],
                    Contact = new OpenApiContact
                    {
                        Name = "Soma Makai",
                        Email = "masoma@stud.uni-obuda.hu"
                    }
                });

                if (addSecurity)
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer" },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });
                }
            });
        }

        public void AddSwaggerUi(IApplicationBuilder app, string url, string name)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url, name);
            });
        }

        public void ConfigureJwtAuthentication(Action<ClaimsIdentity, TokenValidatedContext> onUserAuthenticated = null, bool isAuthenticate = false)
        {
            var jwtSettings = new JwtSettings();
            _configuration.Bind(nameof(jwtSettings), jwtSettings);
            _services.AddSingleton(jwtSettings);

            if (isAuthenticate)
            {
                _services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            onUserAuthenticated?.Invoke(context.Principal.Identity as ClaimsIdentity, context);

                            return Task.CompletedTask;
                        }
                    };
                });
            }
        }
    }
}
