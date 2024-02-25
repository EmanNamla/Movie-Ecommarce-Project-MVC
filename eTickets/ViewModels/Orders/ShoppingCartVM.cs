using eTickets.DAL.Models.Order;

namespace eTickets.PL.ViewModels.Orders
{
    public class ShoppingCartVM
    {
        public ShoppingCart? ShoppingCart { get; set; }

        public double ShoppingCartTotal { get; set; }
    }
}
