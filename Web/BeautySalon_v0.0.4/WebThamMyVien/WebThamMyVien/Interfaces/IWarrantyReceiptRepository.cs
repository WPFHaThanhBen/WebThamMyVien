using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IWarrantyReceiptRepository
    {
        Task<ICollection<WarrantyReceiptDto>> GetAllWarrantyReceipt();
        Task<WarrantyReceiptDto> GetWarrantyReceipt(int id);
        Task<bool> CreateWarrantyReceipt(WarrantyReceiptDto WarrantyReceipt);
        Task<bool> UpdateWarrantyReceipt(WarrantyReceiptDto WarrantyReceipt);
        Task<bool> DeleteWarrantyReceipt(WarrantyReceiptDto WarrantyReceipt);

    }
}
