﻿using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface ICustomerImageRepository
    {
        ICollection<CustomerImage> GetAllCustomerImage();
        ICollection<CustomerImage> GetAllCustomerImageByCustomer(int id);
        CustomerImage GetCustomerImage(int id);
        bool CustomerImageExists(int id);
        bool CreateCustomerImage(CustomerImage customerImage);
        bool UpdateCustomerImage(CustomerImage customerImage);
        bool DeleteCustomerImage(CustomerImage customerImage);
        bool Save();
    }
}
