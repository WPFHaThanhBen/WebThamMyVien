using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IWarrantyTypeRepository
    {
        ICollection<WarrantyTypeDto> GetAllWarrantyType();
        WarrantyTypeDto GetWarrantyType(int id);
        bool CreateWarrantyType(WarrantyTypeDto WarrantyType);
        bool UpdateWarrantyType(WarrantyTypeDto WarrantyType);
        bool DeleteWarrantyType(WarrantyTypeDto WarrantyType);
 
    }
}
