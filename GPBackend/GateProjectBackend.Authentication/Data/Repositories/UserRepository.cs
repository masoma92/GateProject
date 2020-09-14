using GateProjectBackend.Authentication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.Data.Repositories
{
    public interface IUserRepository
    {
        AuthUser GetUserByEmail(string email);
        Task<AuthUser> CreateUser(string firstName, string lastName, string email, byte[] hashedPassword, byte[] passwordSalt, string activationToken);
    }
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _dbContext;

        public UserRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AuthUser GetUserByEmail(string email)
        {
            var user = _dbContext.AuthUsers.FirstOrDefault(x => x.Email == email);
            return user;
        }

        public async Task<AuthUser> CreateUser(string firstName, string lastName, string email, byte[] hashedPassword, byte[] passwordSalt, string activationToken)
        {
            var newUser = new AuthUser { 
                FirstName = firstName, 
                LastName = lastName, 
                Email = email, 
                PasswordHash = hashedPassword ,
                PasswordSalt = passwordSalt,
                ActivationToken = activationToken,
                IsConfirmed = false
            };
            var result = _dbContext.AuthUsers.Add(newUser);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
