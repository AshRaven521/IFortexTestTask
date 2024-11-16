using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Services.Interfaces.DataAccessLayer;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<Order> GetOrder()
        {
            return await orderRepository.GetOrder();    
        }

        public async Task<List<Order>> GetOrders()
        {
            return await orderRepository.GetOrders();
        }
    }
}
