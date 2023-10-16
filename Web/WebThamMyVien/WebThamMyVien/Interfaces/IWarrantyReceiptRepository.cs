using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IWarrantyReceiptRepository
    {
        ICollection<WarrantyReceiptDto> GetAllWarrantyReceipt();
        WarrantyReceiptDto GetWarrantyReceipt(int id);
        bool CreateWarrantyReceipt(WarrantyReceiptDto warrantyReceipt);
        bool UpdateWarrantyReceipt(WarrantyReceiptDto warrantyReceipt);
        bool DeleteWarrantyReceipt(WarrantyReceiptDto warrantyReceipt);
 
    }
}
