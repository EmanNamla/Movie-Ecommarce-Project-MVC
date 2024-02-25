using eTickets.BL.Service.Abstration;
using eTickets.DAL.Contexts;
using eTickets.DAL.Models.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Service.Implementation
{
    public class OrderServices : IOrderServices
    {
        private readonly AppDbContext dbContext;

        public OrderServices(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Order>> GetOrdersByUserAsync(string userId)
        {
           var orders=await dbContext.Orders.Include(n=>n.OrderItems).ThenInclude(a=>a.Movie).Where(m=>m.UserId== userId).ToListAsync();
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
            };
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await dbContext.OrderItems.AddAsync(orderItem);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
