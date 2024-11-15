using TestTask.Models;

namespace TestTask.Services.Interfaces.DataAccessLayer
{
    //Use this Interface and it's realization for repository pattern
    public interface IUserRepository
    {
        public Task<User> GetUser();
        public Task<List<User>> GetUsers();
        public Task<List<User>> GetAllUsersFromDB();
    }
}
