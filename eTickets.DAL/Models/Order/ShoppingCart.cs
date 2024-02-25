using eTickets.DAL.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.DAL.Models.Order
{
    public class ShoppingCart
    {
        private readonly AppDbContext dbContext;

        public ShoppingCart(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string ShoppingCartId { get; set; }

        public List< ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession session=service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDbContext>();
            string cartId=session.GetString("CartId")??Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId= cartId };
        }
      



        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = dbContext.ShoppingCartItems
                                                      .Where(n => n.ShoppingCartId == ShoppingCartId)
                                                      .Include(n => n.Movie)
                                                      .ToList());
        }

        public double GetShoppingCartTotal()
        {
            return dbContext.ShoppingCartItems.Where(n=>n.ShoppingCartId==ShoppingCartId)
                .Select(n=>n.Movie.Price* n.Amount).Sum();
        }
        public void AddItemToCart(Movie movie)
        {
            var ShoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (ShoppingCartItem == null)
            {
                ShoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                dbContext.ShoppingCartItems.Add(ShoppingCartItem);
            }
            else
            {
                ShoppingCartItem.Amount++;
            }
            dbContext.SaveChanges();
        }
        public void RemoveItemToCart(Movie movie)
        {
            var ShoppingCartItem = dbContext.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (ShoppingCartItem != null)
            {
               if(ShoppingCartItem.Amount>1)
                {
                    ShoppingCartItem.Amount--;
                }
                else
                {
                    dbContext.ShoppingCartItems.Remove(ShoppingCartItem);
                }
               
            }
                 dbContext.SaveChanges();
        }

        public async Task ClearShoppingCartItemsAsync()
        {
            var items=await dbContext.ShoppingCartItems.Where(s=>s.ShoppingCartId==ShoppingCartId).ToListAsync();
               dbContext.ShoppingCartItems.RemoveRange(items);
            await dbContext.SaveChangesAsync();
        
        }

    }
}
