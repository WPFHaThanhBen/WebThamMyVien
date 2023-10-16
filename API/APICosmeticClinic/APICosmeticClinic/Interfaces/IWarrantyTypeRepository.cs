using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IWarrantyTypeRepository
    {
        ICollection<WarrantyType> GetAllWarrantyType();
        WarrantyType GetWarrantyType(int id);
        bool WarrantyTypeExists(int id);
        bool CreateWarrantyType(WarrantyType WarrantyType);
        bool UpdateWarrantyType(WarrantyType WarrantyType);
        bool DeleteWarrantyType(WarrantyType WarrantyType);
        bool Save();
    }
}
