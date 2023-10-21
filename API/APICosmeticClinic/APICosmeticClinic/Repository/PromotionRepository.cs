using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public PromotionRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreatePromotion(Promotion promotion)
        {
            _context.Add(promotion);
            return Save();
        }

        public bool DeletePromotion(Promotion promotion)
        {
            promotion.IsDelete = true;
            promotion.DateDelete = sp.GetCurrentDate();
            _context.Update(promotion);
            return Save();
        }

        public ICollection<Promotion> GetAllPromotion()
        {
            return _context.Promotions.Where(c => c.IsDelete != true).ToList();
        }

        public Promotion GetPromotion(int id)
        {
            return _context.Promotions.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool PromotionExists(int id)
        {
            return _context.Promotions.Any(c => c.Id == id && c.IsDelete != true);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePromotion(Promotion promotion)
        {
            _context.Update(promotion);
            return Save(); ;
        }
    }
}
