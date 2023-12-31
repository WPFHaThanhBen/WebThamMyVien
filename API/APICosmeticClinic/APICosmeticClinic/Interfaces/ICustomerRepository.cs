﻿using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface ICustomerRepository
    {
        ICollection<Customer> GetAllCustomer();
        ICollection<Customer> GetAllCustomerSkip(int start, int skip);
        Customer GetCustomer(int id);
		Customer GetCustomerFinal();
		Customer GetCustomerBySDT(string sdt);
        bool CustomerExists(int id);
        bool CustomerExistsBySDT(string sdt);
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        bool Save();
    }
}
