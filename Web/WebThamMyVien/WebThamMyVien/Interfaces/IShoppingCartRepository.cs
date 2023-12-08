using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ICollection<ShoppingCartDto>> GetAllShoppingCart();
        Task<ShoppingCartDto> GetShoppingCart(int id);
        Task<ShoppingCartDto> GetShoppingCartByAccountId(int id);
        Task<bool> CreateShoppingCart(ShoppingCartDto ShoppingCart);
        Task<bool> UpdateShoppingCart(ShoppingCartDto ShoppingCart);
        Task<bool> DeleteShoppingCart(ShoppingCartDto ShoppingCart);

    }
}
