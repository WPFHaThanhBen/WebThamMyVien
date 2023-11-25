using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IWarrantyTypeRepository
    {
        Task<ICollection<WarrantyTypeDto>> GetAllWarrantyType();
        Task<WarrantyTypeDto> GetWarrantyType(int id);
        Task<bool> CreateWarrantyType(WarrantyTypeDto WarrantyType);
        Task<bool> UpdateWarrantyType(WarrantyTypeDto WarrantyType);
        Task<bool> DeleteWarrantyType(WarrantyTypeDto WarrantyType);

    }
}
