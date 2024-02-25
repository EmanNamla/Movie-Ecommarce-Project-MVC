using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.DAL.Models.Order
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public Movie Movie { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }

    
    }
}
