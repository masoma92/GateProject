using GateProjectBackend.Common;
using GateProjectBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data
{
    public class GPDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public GPDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountAdmin> AccountAdmins { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGate> UserGates { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<GateType> GateTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Admin" });

            modelBuilder.Entity<EventType>().HasData(
                new EventType { Id = 1, Name = "User" },
                new EventType { Id = 2, Name = "Error" },
                new EventType { Id = 3, Name = "Info" });

            modelBuilder.Entity<AccountType>().HasData(
                new AccountType { Id = 1, Name = "Office" },
                new AccountType { Id = 2, Name = "Home" });

            modelBuilder.Entity<GateType>().HasData(
                new GateType { Id = 1, Name = "Entrance" },
                new GateType { Id = 2, Name = "Garage" },
                new GateType { Id = 3, Name = "School" },
                new GateType { Id = 4, Name = "Hotel" });

            // accounts and admins many-to-many join table
            modelBuilder.Entity<AccountAdmin>().HasKey(a => new { a.UserId, a.AccountId });
            modelBuilder.Entity<AccountAdmin>().HasOne(ad => ad.User).WithMany(u => u.AdminAccounts).HasForeignKey(ac => ac.UserId);
            modelBuilder.Entity<AccountAdmin>().HasOne(ad => ad.Account).WithMany(ac => ac.Admins).HasForeignKey(ac => ac.AccountId);

            // accounts and users many-to-many join table
            modelBuilder.Entity<AccountUser>().HasKey(a => new { a.UserId, a.AccountId });
            modelBuilder.Entity<AccountUser>().HasOne(ad => ad.User).WithMany(u => u.UserAccounts).HasForeignKey(ac => ac.UserId);
            modelBuilder.Entity<AccountUser>().HasOne(ad => ad.Account).WithMany(ac => ac.Users).HasForeignKey(ac => ac.AccountId);

            // users and gates many-to-many join table
            modelBuilder.Entity<UserGate>().HasKey(ug => new { ug.UserId, ug.GateId });
            modelBuilder.Entity<UserGate>().HasOne(ug => ug.User).WithMany(u => u.Gates).HasForeignKey(g => g.UserId);
            modelBuilder.Entity<UserGate>().HasOne(ug => ug.Gate).WithMany(g => g.Users).HasForeignKey(ug => ug.GateId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("CONN"));
        }
    }
}
