using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IWarrantyReceiptRepository
    {
        ICollection<WarrantyReceipt> GetAllWarrantyReceipt();
        WarrantyReceipt GetWarrantyReceipt(int id);
        bool WarrantyReceiptExists(int id);
        bool CreateWarrantyReceipt(WarrantyReceipt warrantyReceipt);
        bool UpdateWarrantyReceipt(WarrantyReceipt warrantyReceipt);
        bool DeleteWarrantyReceipt(WarrantyReceipt warrantyReceipt);
        bool Save();
    }
}
