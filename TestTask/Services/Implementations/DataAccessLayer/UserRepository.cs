using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces.DataAccessLayer;

namespace TestTask.Services.Implementations.DataAccessLayer
{
    /// <summary>
    /// This class work with data from database. For don't call database anywhere else I would call this realization.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IOrderRepository orderRepository;
        public UserRepository(ApplicationDbContext context, IOrderRepository orderRepository)
        {
            this.context = context;
            this.orderRepository = orderRepository;
        }

        public async Task<List<User>> GetAllUsersFromDB()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetUser()
        {
            var users = await GetAllUsersFromDB();
            // Had to do like this, because I can't change user model to add field for orders price.
            // In this dictionary will be stored (user Id, sum of user's orders price)
            var usersMaxOrdersPrice = new Dictionary<int, int>();

            foreach (var user in users)
            {
                var orders = await orderRepository.GetUserOrders(user);
                int userOrdersSum = 0;
                foreach (var order in orders)
                {
                    if (order.Status == Enums.OrderStatus.Delivered)
                    {
                        if (order.CreatedAt.Year == 2003)
                        {
                            userOrdersSum += order.Price; 
                        }
                    }
                }
                if (userOrdersSum > 0) 
                {
                    usersMaxOrdersPrice.Add(user.Id, userOrdersSum);
                }
            }
            int maxOrdersSum = usersMaxOrdersPrice.Values.Max();
            int userWithMaxOrdersSumId = usersMaxOrdersPrice.FirstOrDefault(x => x.Value == maxOrdersSum).Key;
            var userWithMaxOrdersSum = users.FirstOrDefault(u => u.Id == userWithMaxOrdersSumId);
            return userWithMaxOrdersSum;

        }

        public async Task<List<User>> GetUsers()
        {
            var users = await GetAllUsersFromDB();
            var usersWith2010PaidOrders = new List<User>();

            foreach (var user in users)
            {
                var orders = await orderRepository.GetUserOrders(user);
                foreach (var order in orders)
                {
                    if (order.Status == Enums.OrderStatus.Paid)
                    {
                        if (order.CreatedAt.Year == 2010)
                        {
                            usersWith2010PaidOrders.Add(user);
                        }
                    }
                }
            }
            return usersWith2010PaidOrders;
        }
    }
}
