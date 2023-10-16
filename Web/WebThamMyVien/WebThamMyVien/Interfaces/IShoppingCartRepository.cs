using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IShoppingCartRepository
    {
        ICollection<ShoppingCartDto> GetAllShoppingCart();
        ShoppingCartDto GetShoppingCart(int id);
        bool CreateShoppingCart(ShoppingCartDto shoppingCart);
        bool UpdateShoppingCart(ShoppingCartDto shoppingCart);
        bool DeleteShoppingCart(ShoppingCartDto shoppingCart);
 
    }
}
