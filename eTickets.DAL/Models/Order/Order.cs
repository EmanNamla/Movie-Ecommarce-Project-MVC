using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.DAL.Models.Order
{
    public class Order:BaseEntity
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
