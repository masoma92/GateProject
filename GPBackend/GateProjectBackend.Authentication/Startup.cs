using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using GateProjectBackend.Authentication.Data;
using GateProjectBackend.Authentication.Data.Repositories;
using GateProjectBackend.Authentication.Resources;
using GateProjectBackend.Authentication.Resources.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GateProjectBackend.Authentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IUserRepository, UserRepository>();

            string conn = Configuration.GetConnectionString("CONN");

            services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseSqlServer(conn);
            });

            services.Configure<UrlSettings>(Configuration.GetSection("UrlSettings"));

            #region SENDGRID

            services.Configure<SendGridEmailSettings>(Configuration.GetSection("SendGridEmailProperties"));

            services.Configure<SendGridEmailVariables>(Configuration.GetSection("CompanyProperties"));

            services.AddScoped<IEmailSender, EmailSender>();

            #endregion

            #region JWT_AUTHENTICATION

            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            #endregion

            #region SWAGGERCONFIG

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "GateProject Authentication API",
                    Description = "GP Authentication REST API Endpoint",
                    Contact = new OpenApiContact
                    {
                        Name = "Soma Makai",
                        Email = "masoma@stud.uni-obuda.hu"
                    }
                });
            });

            #endregion

            services.AddMediatR(typeof(Startup));
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.

            var swaggerSettings = new SwaggerSettings();
            Configuration.GetSection(nameof(swaggerSettings)).Bind(swaggerSettings);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerSettings.URL, swaggerSettings.Name);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
