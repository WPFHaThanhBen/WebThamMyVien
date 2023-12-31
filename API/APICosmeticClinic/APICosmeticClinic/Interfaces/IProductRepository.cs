﻿using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllProduct();
        ICollection<Product> GetAllProductByType(int id);
        ICollection<Product> GetAllProductSkip(int start, int skip);
        Product GetProduct(int id);
        Product GetProductFinal();
        bool ProductExists(int id);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();
    }
}
