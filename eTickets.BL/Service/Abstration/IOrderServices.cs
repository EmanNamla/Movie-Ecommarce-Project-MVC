using eTickets.DAL.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Service.Abstration
{
    public interface IOrderServices
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items,string userId,string userEmailAddress);
        Task <List<Order>> GetOrdersByUserAsync(string userId);
    }
}
