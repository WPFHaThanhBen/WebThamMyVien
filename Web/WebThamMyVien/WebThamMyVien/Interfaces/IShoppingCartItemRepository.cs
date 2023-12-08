﻿using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        Task<ICollection<ShoppingCartItemDto>> GetAllShoppingCartItem();
        Task<ICollection<ShoppingCartItemDto>> GetAllShoppingCartItemByShoppingCartId(int id);
        Task<ShoppingCartItemDto> GetShoppingCartItem(int id);
        Task<bool> CreateShoppingCartItem(ShoppingCartItemDto ShoppingCartItem);
        Task<bool> UpdateShoppingCartItem(ShoppingCartItemDto ShoppingCartItem);
        Task<bool> DeleteShoppingCartItem(ShoppingCartItemDto ShoppingCartItem);

    }
}
