using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GateProjectBackend.Common.Startup;
using GateProjectBackend.Data;
using GateProjectBackend.Data.Repositories;
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
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
            services.AddScoped<IAccountAdminRepository, AccountAdminRepository>();
            services.AddScoped<IUserGateRepository, UserGateRepository>();
            services.AddScoped<IGateRepository, GateRepository>();
            services.AddScoped<IGateTypeRepository, GateTypeRepository>();

            string conn = Configuration.GetConnectionString("CONN");

            services.AddDbContext<GPDbContext>(opt =>
            {
                opt.UseSqlServer(conn);
            });

            #region COMMON_SWAGGER_JWTAUTH

            _apiStartup = new ApiStartup(services, Configuration);
            _apiStartup.AddSwaggerGen(true);
            _apiStartup.ConfigureJwtAuthentication(OnUserAuthenticated, true);

            #endregion

            #region CORSCONFIG
            //** will be used
            //var corsConfig = Configuration.GetSection("CorsConfiguration").Get<CorsConfiguration>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "GateCorsConfig",
                    builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod());
                //builder =>
                //{
                //    builder.WithOrigins(corsConfig?.AcceptedUrls?.ToArray());
                //});
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            #endregion

            #region MEDIATR

            services.AddMediatR(typeof(Startup));

            #endregion

            #region IGNORE_REFERENCELOOPING
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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

            app.UseCors("GateCorsConfig");

            app.UseMvc();

            //** will be used
            //app.UseCors("GPAuthAllowedOrigins")

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }

        private void OnUserAuthenticated(ClaimsIdentity user, TokenValidatedContext context)
        {
            var userRepository = context.HttpContext.RequestServices.GetService<IUserRepository>();
            var dbUser = userRepository.GetUserByEmail(user.Name).Result;
            
            if (dbUser == null)
            {
                var fname = user.Claims.FirstOrDefault(x => x.Type == "firstname").Value;
                var lname = user.Claims.FirstOrDefault(x => x.Type == "lastname").Value;
                var email = user.Name;
                var birth = DateTime.Parse(user.Claims.FirstOrDefault(x => x.Type == "birth").Value);
                var res = userRepository.CreateUser(fname, lname, email, birth).Result;
            }

            var roleRepository = context.HttpContext.RequestServices.GetService<IRoleRepository>();
            var role = roleRepository.GetRoleByUserEmail(user.Name).Result;
            if (role != null)
                user.AddClaim(new Claim("Role", role.Name));
        }
    }
}
