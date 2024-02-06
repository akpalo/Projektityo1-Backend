using VarausJarjestelma.Models;

namespace VarausJarjestelma.Services
{
    public interface IUserService
    {
        public Task<UserDTO> CreateUserAsync(User user);
    }
}
