using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        ICollection<ShoppingCartItemDto> GetAllShoppingCartItem();
        ShoppingCartItemDto GetShoppingCartItem(int id);
        bool CreateShoppingCartItem(ShoppingCartItemDto shoppingCartItem);
        bool UpdateShoppingCartItem(ShoppingCartItemDto shoppingCartItem);
        bool DeleteShoppingCartItem(ShoppingCartItemDto shoppingCartItem);
 
    }
}
