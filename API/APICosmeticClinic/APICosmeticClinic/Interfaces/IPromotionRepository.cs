using APICosmeticClinic.Models;

namespace APICosmeticClinic.Interfaces
{
    public interface IPromotionRepository
    {
        ICollection<Promotion> GetAllPromotion();
        Promotion GetPromotion(int id);
        bool PromotionExists(int id);
        bool CreatePromotion(Promotion promotion);
        bool UpdatePromotion(Promotion promotion);
        bool DeletePromotion(Promotion promotion);
        bool Save();
    }
}
