﻿using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<ICollection<InvoiceDto>> GetAllInvoice();
        Task<InvoiceDto> GetInvoice(int id);
        Task<bool> CreateInvoice(InvoiceDto Invoice);
        Task<bool> UpdateInvoice(InvoiceDto Invoice);
        Task<bool> DeleteInvoice(InvoiceDto Invoice);

    }
}
