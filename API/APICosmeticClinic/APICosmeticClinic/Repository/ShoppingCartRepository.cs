using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ShoppingCartRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.Add(shoppingCart);
            return Save();
        }

        public bool DeleteShoppingCart(ShoppingCart shoppingCart)
        {
            shoppingCart.IsDelete = true;
            shoppingCart.DateDelete = sp.GetCurrentDate();
            _context.Update(shoppingCart);
            return Save();
        }

        public ICollection<ShoppingCart> GetAllShoppingCart()
        {
            return _context.ShoppingCarts.Where(c => c.IsDelete != true).ToList();
        }

        public ShoppingCart GetShoppingCart(int id)
        {
            return _context.ShoppingCarts.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public ShoppingCart GetShoppingCartByAccountId(int id)
        {
            return _context.ShoppingCarts.Where(e => e.UserAccountId == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCarts.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.Update(shoppingCart);
            return Save();
        }
    }
}
