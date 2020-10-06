using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateProjectBackend.Common.Startup;
using GateProjectBackend.Data;
using GateProjectBackend.Data.Repositories;
using GateProjectBackend.Resources.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GateProjectBackend
{
    public class Startup
    {
        private ApiStartup _apiStartup;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            string conn = Configuration.GetConnectionString("CONN");

            services.AddDbContext<GPDbContext>(opt =>
            {
                opt.UseSqlServer(conn);
            });

            #region CORSCONFIG
            //** will be used
            //var corsConfig = Configuration.GetSection("CorsConfiguration").Get<CorsConfiguration>();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "GPAuthAllowedOrigins",
            //        builder =>
            //        {
            //            builder.WithOrigins(corsConfig?.AcceptedUrls?.ToArray());
            //        });
            //});

            services.AddCors(options => options.AddDefaultPolicy(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddMvc(options => options.EnableEndpointRouting = false);

            #endregion

            #region JWT_AUTHENTICATION

            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
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
            });

            #endregion

            #region MEDIATR

            services.AddMediatR(typeof(Startup));

            #endregion

            #region SWAGGER

            _apiStartup = new ApiStartup(services, Configuration);
            _apiStartup.AddSwaggerGen(true);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _apiStartup.AddSwaggerUi(app, "/swagger/v1/swagger.json", "GateProjectBackend");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseMvc();

            //** will be used
            //app.UseCors("GPAuthAllowedOrigins")

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
