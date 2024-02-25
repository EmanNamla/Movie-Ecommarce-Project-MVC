using AutoMapper.Configuration.Annotations;
using eTickets.BL.Service.Abstration;
using eTickets.BL.UnitOfWork;
using eTickets.DAL.Models;
using eTickets.DAL.Models.Order;
using eTickets.PL.ViewModels.Movies;
using eTickets.PL.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.PL.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShoppingCart shoppingCart;
        private readonly IUnitofWork unitofWork;
        private readonly IOrderServices orderServices;

        public OrdersController(ShoppingCart shoppingCart,IUnitofWork unitofWork,IOrderServices orderServices)
        {
            this.shoppingCart = shoppingCart;
            this.unitofWork = unitofWork;
            this.orderServices = orderServices;
        }
        public async Task<IActionResult> Index()
        {
            string userId = "";
            var Orders = await orderServices.GetOrdersByUserAsync(userId);
            return View(Orders);
        }
        public IActionResult ShoppingCart()
        {
            var Items=shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems=Items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item=await unitofWork.Repository<Movie>().GetByIdAsync(id);
            if(item !=null)
            {
                shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await unitofWork.Repository<Movie>().GetByIdAsync(id);
            if (item != null)
            {
                shoppingCart.RemoveItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
      

        public async Task<IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetShoppingCartItems();
            string UserId = "";
            string UserEmailAddress = ""; 
           await orderServices.StoreOrderAsync(items, UserId, UserEmailAddress);
           await shoppingCart.ClearShoppingCartItemsAsync();
            return View();
        }
    }
}
