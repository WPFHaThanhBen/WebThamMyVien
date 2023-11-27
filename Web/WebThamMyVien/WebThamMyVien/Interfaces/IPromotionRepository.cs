using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPromotionRepository
    {
        Task<ICollection<PromotionDto>> GetAllPromotion();
        Task<PromotionDto> GetPromotion(int id);
        Task<bool> CreatePromotion(PromotionDto promotion);
        Task<bool> UpdatePromotion(PromotionDto promotion);
        Task<bool> DeletePromotion(PromotionDto promotion);
    }
}
