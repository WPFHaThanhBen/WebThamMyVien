using WebThamMyVien.Models;

namespace WebThamMyVien.Interfaces
{
    public interface IPromotionRepository
    {
        ICollection<PromotionDto> GetAllPromotion();
        PromotionDto GetPromotion(int id);
        bool CreatePromotion(PromotionDto promotion);
        bool UpdatePromotion(PromotionDto promotion);
        bool DeletePromotion(PromotionDto promotion);
 
    }
}
