using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces.DataAccessLayer;

namespace TestTask.Services.Implementations.DataAccessLayer
{
    /// <summary>
    /// This class work with data from database. For don't call database anywhere else I would call this realization.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Order> GetOrder()
        {
            var ordersWithMoreThanOneQuantity = await context.Orders.Where(o => o.Quantity > 1).ToListAsync();
            var theEarliestOrder = ordersWithMoreThanOneQuantity.OrderBy(d => d.CreatedAt).First();
            return theEarliestOrder;
        }

        public async Task<List<Order>> GetOrders()
        {
            var ordersFromActiveUsers = new List<Order>();

            var activeUsers = await context.Users.Where(u => u.Status == Enums.UserStatus.Active).ToListAsync();
            foreach (var activeUser in activeUsers)
            {
                var orders = await GetUserOrders(activeUser); 
                if (orders.Any())
                {
                    ordersFromActiveUsers.AddRange(orders);
                }
            }

            var sortedByDaysOrders = ordersFromActiveUsers.OrderBy(o => o.CreatedAt).ToList();
            return sortedByDaysOrders;
        }

        public async Task<List<Order>> GetUserOrders(User user)
        {
            var orders = await context.Orders.Where(o => o.UserId == user.Id).ToListAsync();
            return orders;
        }
    }
}
