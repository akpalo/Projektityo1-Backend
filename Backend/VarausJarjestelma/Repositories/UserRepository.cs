using VarausJarjestelma.Models;
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

        

        public async Task<User?> GetUserAsync(string username)
        {
            User? user = _context.Users.Where(x => x.UserName == username).FirstOrDefault();
            return user;
        }

        
    }
}
