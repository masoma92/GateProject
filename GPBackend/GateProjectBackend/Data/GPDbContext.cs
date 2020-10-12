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
                new AccountType { Id = 2, Name = "Home" },
                new AccountType { Id = 3, Name = "School" },
                new AccountType { Id = 4, Name = "Accomodation" });

            modelBuilder.Entity<GateType>().HasData(
                new GateType { Id = 1, Name = "Entrance" },
                new GateType { Id = 2, Name = "Garage" });

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Soma", LastName = "Makai", Email = "soma.makai@gmail.com", Birth = DateTime.Parse("1992.03.17"), CreatedAt = DateTime.UtcNow, CreatedBy = "SYSTEM", IsActive = true, RoleId = 2 });

            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Name = "TestOffice", Zip = "1145", Country = "Hungary", City = "Budapest", Street = "Szuglo utca", StreetNo = "53", CreatedAt = DateTime.UtcNow, CreatedBy = "SYSTEM", AccountTypeId = 1, ContactEmail = "asd@gmail.com" },
                new Account { Id = 2, Name = "TestSchool", Zip = "1146", Country = "Hungary", City = "Budapest", Street = "Szuglo utca", StreetNo = "53", CreatedAt = DateTime.UtcNow, CreatedBy = "SYSTEM", AccountTypeId = 3, ContactEmail = "asd@gmail.com" },
                new Account { Id = 3, Name = "TestOffice2", Zip = "1147", Country = "Hungary", City = "Budapest", Street = "Szuglo utca", StreetNo = "53", CreatedAt = DateTime.UtcNow, CreatedBy = "SYSTEM", AccountTypeId = 1, ContactEmail = "asd@gmail.com" },
                new Account { Id = 4, Name = "TestHome", Zip = "1148", Country = "Hungary", City = "Budapest", Street = "Szuglo utca", StreetNo = "53", CreatedAt = DateTime.UtcNow, CreatedBy = "SYSTEM", AccountTypeId = 2, ContactEmail = "asd@gmail.com" },
                new Account { Id = 5, Name = "TestAccomodation", Zip = "1149", Country = "Hungary", City = "Budapest", Street = "Szuglo utca", StreetNo = "53", CreatedAt = DateTime.UtcNow, CreatedBy = "SYSTEM", AccountTypeId = 4, ContactEmail = "asd@gmail.com" },
                new Account { Id = 6, Name = "TestOffice", Zip = "1150", Country = "Hungary", City = "Budapest", Street = "Szuglo utca", StreetNo = "53", CreatedAt = DateTime.UtcNow, CreatedBy = "SYSTEM", AccountTypeId = 1, ContactEmail = "asd@gmail.com" });

            modelBuilder.Entity<AccountAdmin>().HasData(
                new AccountAdmin { AccountId = 1, UserId = 1, CreatedAt = DateTime.UtcNow, CreatedBy = "SYSTEM" });

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
