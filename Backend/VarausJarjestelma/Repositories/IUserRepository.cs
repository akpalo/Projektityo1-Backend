using VarausJarjestelma.Models;

namespace VarausJarjestelma.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserAsync (string username);
        public Task<User> AddUserAsync(User user);
    }
}
