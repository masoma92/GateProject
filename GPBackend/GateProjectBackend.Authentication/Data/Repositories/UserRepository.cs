using GateProjectBackend.Authentication.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.Data.Repositories
{
    public interface IUserRepository
    {
        Task<AuthUser> GetUserByEmail(string email);
        Task<AuthUser> CreateUser(string firstName, string lastName, string email, byte[] hashedPassword, byte[] passwordSalt);
        Task<int> Update(AuthUser user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _dbContext;

        public UserRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthUser> GetUserByEmail(string email)
        {
            var user = await _dbContext.AuthUsers.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<AuthUser> CreateUser(string firstName, string lastName, string email, byte[] hashedPassword, byte[] passwordSalt)
        {
            var newUser = new AuthUser { 
                FirstName = firstName, 
                LastName = lastName, 
                Email = email, 
                PasswordHash = hashedPassword ,
                PasswordSalt = passwordSalt,
                IsConfirmed = false
            };
            var result = _dbContext.AuthUsers.Add(newUser);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> Update(AuthUser user)
        {
            var result = _dbContext.AuthUsers.Update(user);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }
    }
}
