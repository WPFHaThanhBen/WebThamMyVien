using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IShoppingCartRepository
    {
        ICollection<ShoppingCart> GetAllShoppingCart();
        ShoppingCart GetShoppingCart(int id);
        ShoppingCart GetShoppingCartByAccountId(int id);
        bool ShoppingCartExists(int id);
        bool CreateShoppingCart(ShoppingCart shoppingCart);
        bool UpdateShoppingCart(ShoppingCart shoppingCart);
        bool DeleteShoppingCart(ShoppingCart shoppingCart);
        bool Save();
    }
}
