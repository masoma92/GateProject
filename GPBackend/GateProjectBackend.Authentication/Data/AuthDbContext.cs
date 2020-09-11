using GateProjectBackend.Authentication.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.Data
{
    public class AuthDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AuthDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<AuthUser> AuthUsers { get; set; }
    }
}
