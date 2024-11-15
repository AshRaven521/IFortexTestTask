using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Services.Interfaces.DataAccessLayer;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetUser()
        {
            return await userRepository.GetUser();
        }

        public async Task<List<User>> GetUsers()
        {
            return await userRepository.GetUsers();
        }
    }
}
