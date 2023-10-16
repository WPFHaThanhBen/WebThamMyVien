using APICosmeticClinic.Helper;
using APICosmeticClinic.Interfaces;
using APICosmeticClinic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Collections.Specialized.BitVector32;

namespace APICosmeticClinic.Repository
{
    public class FollowUpDetailRepository : IFollowUpDetailRepository
    {
        private QL_CosmeticClinic_V2Context _context;
        private Support sp = new Support();
        public FollowUpDetailRepository(QL_CosmeticClinic_V2Context context)
        {
            _context = context;
        }
        public bool CreateFollowUpDetail(FollowUpDetail followUpDetail)
        {
            _context.Add(followUpDetail);
            return Save();
        }

        public bool DeleteFollowUpDetail(FollowUpDetail followUpDetail)
        {
            followUpDetail.IsDelete = true;
            followUpDetail.DateDelete = sp.GetCurrentDate();
            _context.Update(followUpDetail);
            return Save();
        }

        public bool FollowUpDetailExists(int id)
        {
            return _context.FollowUpDetails.Any(c => c.Id == id && c.IsDelete != true);
        }

        public ICollection<FollowUpDetail> GetAllFollowUpDetail()
        {
            return _context.FollowUpDetails.Where(c => c.IsDelete != true).ToList();
        }

        public FollowUpDetail GetFollowUpDetail(int id)
        {
            return _context.FollowUpDetails.Where(e => e.Id == id && e.IsDelete != true).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFollowUpDetail(FollowUpDetail followUpDetail)
        {
            _context.Update(followUpDetail);
            return Save();
        }
    }
}
