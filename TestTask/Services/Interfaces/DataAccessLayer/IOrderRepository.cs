using TestTask.Models;

namespace TestTask.Services.Interfaces.DataAccessLayer
{
    //Use this Interface and it's realization for repository pattern
    public interface IOrderRepository
    {
        public Task<Order> GetOrder();
        public Task<List<Order>> GetOrders();
        public Task<List<Order>> GetUserOrders(User user);
    }
}
