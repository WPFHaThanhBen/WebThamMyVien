using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public ShoppingCartItemRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _context.Add(shoppingCartItem);
            return Save();
        }

        public bool DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            shoppingCartItem.IsDelete = true;
            shoppingCartItem.DateDelete = sp.GetCurrentDate();
            _context.Update(shoppingCartItem);
            return Save();
        }

        public ICollection<ShoppingCartItem> GetAllShoppingCartItem()
        {
            return _context.ShoppingCartItems.Where(c => c.IsDelete != true).ToList();
        }

        public ICollection<ShoppingCartItem> GetAllShoppingCartItemByShoppingCartId(int id)
        {
            return _context.ShoppingCartItems.Where(c => c.IsDelete != true && c.ShoppingCartId == id).ToList();
        }

        public ShoppingCartItem GetShoppingCartItem(int id)
        {
            return _context.ShoppingCartItems.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ShoppingCartItemExists(int id)
        {
            return _context.ShoppingCartItems.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _context.Update(shoppingCartItem);
            return Save();
        }
    }
}
