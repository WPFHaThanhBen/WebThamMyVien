using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        ICollection<ShoppingCartItem> GetAllShoppingCartItem();
        ShoppingCartItem GetShoppingCartItem(int id);
        bool ShoppingCartItemExists(int id);
        bool CreateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        bool UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        bool DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem);
        bool Save();
    }
}
