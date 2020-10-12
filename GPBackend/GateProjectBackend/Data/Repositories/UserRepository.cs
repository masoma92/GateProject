﻿using GateProjectBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(string firstName, string lastName, string email, DateTime birth);
        Task<IEnumerable<User>> GetAll();
    }
    public class UserRepository : IUserRepository
    {
        private readonly GPDbContext _context;

        public UserRepository(GPDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(string firstName, string lastName, string email, DateTime birth)
        {
            if (_context.Users.Any(x => x.Email == email))
                throw new Exception("User already exist!");

            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Birth = birth,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "SYSTEM",
                RoleId = 1
            };
            var result = _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
