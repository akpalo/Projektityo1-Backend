﻿using VarausJarjestelma.Models;
using Microsoft.EntityFrameworkCore;

namespace VarausJarjestelma.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ReservationContext _context;

        public UserRepository(ReservationContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return null;
            }
            return user;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<User> GetUserAsync(long id)
        {
            User user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            return user;
        }

        
    }
}
